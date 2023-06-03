using Eduker.Areas.Admin.ViewModels.RolesVm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Eduker.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                var roles = _roleManager.Roles.ToList();

                var roleVm = roles.Select(x => new RoleVm
                {
                    Id = x.Id,
                    Name = x.Name
                });

                return View(roleVm);
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] AddRoleVm addroleVm)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                if (ModelState.IsValid)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(addroleVm.Name));

                    if (!roleResult.Succeeded)
                    {
                        Log.Information($"Role {addroleVm.Name} has added ");
                        return RedirectToAction("Index", "roles");
                    }
                }

                return await Index();
            }

            return Forbid();
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] string id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role != null)
                {
                    
                    return View(new EditRoleVm
                    {
                        Name = role.Name
                    });
                }

                return RedirectToAction("index", "roles");
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditRoleVm roleVm, [FromQuery] string id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                if (ModelState.IsValid)
                {
                    var roleExist = await _roleManager.FindByNameAsync(roleVm.Name);

                    if (roleExist != null)
                    {
                        return await Edit(id);
                    }

                    var newrole = await _roleManager.FindByIdAsync(id);
                    newrole.Name = roleVm.Name;

                    var result = await _roleManager.UpdateAsync(newrole);
                    if (result.Succeeded)
                    {
                        Log.Information($"Role with id {newrole.Id} has changed ");
                        return RedirectToAction("index", "roles");
                    }
                }

                return await Edit(id);
            }

            return Forbid();
        }

        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                var role = await _roleManager.FindByIdAsync(id);

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    Log.Information($"Role with id {id} has deleted ");
                    return RedirectToAction("index", "roles");
                }

                return RedirectToAction("index", "roles");
            }

            return Forbid();
        }
    }
}