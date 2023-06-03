using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Eduker.Areas.Admin.ViewModels.UserVm;
using Services.Abstraction;
using Contract.ClientDto.UserDto;
using Contract.AdminDto.UserInfoDto;

namespace Eduker.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _serviceManager = serviceManager;
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
                var users = _userManager.Users.ToList();

                List<UserVm> userVm = new List<UserVm>();

                foreach (var x in users)
                {
                    var listOfRoles = await _userManager.GetRolesAsync(x);
                    userVm.Add(new UserVm
                    {
                        Id = x.Id,
                        Name = x.UserName,
                        Role = listOfRoles.First()
                    });
                }

                return View(userVm);
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] AddUserVm userVm)
        {
            if (ModelState.IsValid)
            {
                var userName = Request.Cookies["Username"];
                if (userName == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByNameAsync(userName);

                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    var newUser = new IdentityUser
                    {
                        UserName = userVm.Name,
                        Email = userVm.Email,
                    };
                    var result = await _userManager.CreateAsync(newUser, userVm.Password);

                    if (result.Succeeded)
                    {
                        var createdUser = await _userManager.FindByNameAsync(userVm.Name);
                        var role = await _roleManager.FindByNameAsync(userVm.Role);
                        if (role == null)
                        {
                            role = new IdentityRole("simple");
                        }


                        await _serviceManager.UserInfoService.AddUserInfo(new AddUserInfoDto
                        {
                            Address = userVm.Address,
                            RealName = userVm.RealName,
                            Email = userVm.Email,
                            IdentityId = createdUser.Id
                        });

                        var isRoleAdded = await _userManager.AddToRoleAsync(createdUser, role.Name);
                        if (isRoleAdded.Succeeded)
                        {
                            await _signInManager.SignInAsync(createdUser, isPersistent: false);
                            return RedirectToAction("index", "user");
                        }
                    }

                    return await Index();
                }

                return Forbid();
            }

            return await Index();
        }

        [HttpGet]
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
                var deletedUser = await _userManager.FindByIdAsync(id);
                if (deletedUser != null)
                {
                    await _serviceManager.UserInfoService.DeleteUserInfo(deletedUser.Id);
                    await _userManager.DeleteAsync(deletedUser);
                    return RedirectToAction("index", "user");
                }
            }

            return Forbid();
        }

        [HttpGet]
        public async Task<IActionResult> EditRole([FromQuery] string id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);


            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                var userVm = await _userManager.FindByIdAsync(id);
                var userRole = await _userManager.GetRolesAsync(userVm);

                return View(new EditUserRoleVm
                {
                    Name = userRole.FirstOrDefault()
                });
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> EditRole([FromForm] EditUserRoleVm editUserRoleVm, [FromQuery] string id)
        {
            if (ModelState.IsValid)
            {
                var userName = Request.Cookies["Username"];
                if (userName == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByNameAsync(userName);

                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    var editUserRole = await _userManager.FindByIdAsync(id);
                    var userRole = await _userManager.GetRolesAsync(editUserRole);

                    var newUserRole = await _roleManager.FindByNameAsync(editUserRoleVm.Name);
                    if (newUserRole == null)
                    {
                        newUserRole = new IdentityRole("simple");
                    }

                    var resultRemove = await _userManager.RemoveFromRoleAsync(editUserRole, userRole.FirstOrDefault());
                    if (resultRemove.Succeeded)
                    {
                        var resultAdd = await _userManager.AddToRoleAsync(editUserRole, newUserRole.Name);
                        if (resultAdd.Succeeded)
                        {
                            return RedirectToAction("index", "user");
                        }
                    }
                }
            }

            return await EditRole(id);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser([FromQuery] string id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                var currentUser = await _userManager.FindByIdAsync(id);
                return View(new EditUserVm
                {
                    UserName = currentUser.UserName,
                    Email = currentUser.Email
                });
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm] EditUserVm editUserVm, [FromQuery] string id)
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
                    var newUser = await _userManager.FindByIdAsync(id);
                    if (newUser.Email != editUserVm.Email | newUser.UserName != editUserVm.UserName)
                    {
                        newUser.UserName = editUserVm.UserName;
                        newUser.Email = editUserVm.Email;

                        var result = await _userManager.UpdateAsync(newUser);
                        var userInfo = await _serviceManager.UserInfoService.GetUserById(id);
                        await _serviceManager.UserInfoService.EditUserInfo(new EditUserInfoDto
                        {
                            Id = userInfo.Id,
                            RealName = editUserVm.RealName,
                            Email = newUser.Email,
                            Address = editUserVm.Address,
                            UserId = newUser.Id
                        });
                        if (result.Succeeded)
                        {
                            return RedirectToAction("index", "user");
                        }
                    }
                }

                return await EditUser(id);
            }

            return Forbid();
        }
    }
}