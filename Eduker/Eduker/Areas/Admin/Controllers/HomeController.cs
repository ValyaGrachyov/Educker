using Eduker.ViewModels.LoginVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eduker.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
               return Redirect("/404");
            }

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                return View();
            }

            return Forbid();
        }
    }
}