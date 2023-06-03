using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Eduker.ViewModels.UserInfoVm;
using Contract.ClientDto.UserDto;
using Microsoft.Identity.Client;
using Serilog;

namespace Eduker.Controllers
{
    [Route("/user")]
    public class UserInfoController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserInfoController(IServiceManager serviceManager, UserManager<IdentityUser> userManager)
        {
            _serviceManager = serviceManager;
            _userManager = userManager;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cookieVal = Request.Cookies["Username"];

            if (cookieVal == null)
            {
                return Redirect("/login");
            }

            var user = await _userManager.FindByNameAsync(cookieVal);
            if (user == null)
            {
                return Redirect("/login");
            }
            var userInfo = await _serviceManager.UserInfoService.GetUserById(user.Id);


            return View(new UserInfoVm
            {
                Id = userInfo.Id,
                IdentityId = user.Id,
                UserName = user.UserName,
                RealName = userInfo.RealName,
                Address = userInfo.Address,
                Phone = user.PhoneNumber,
                Email = user.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] EditUserInfoVm userInfoVm, [FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return await Index();
            }

            var user = await _userManager.FindByNameAsync(Request.Cookies["Username"]);
            user.PhoneNumber = userInfoVm.Phone;

            var editUserInfoDto = new EditUserInfoDto
            {
                Id = id,
                RealName = userInfoVm.RealName,
                Address = userInfoVm.Address,
                Email = userInfoVm.Email,
                UserId = user.Id
            };

            var result = await _serviceManager.UserInfoService.EditUserInfo(editUserInfoDto);
            Log.Information($"UserInfo with id {user.Id} is changed");
            return RedirectToAction("index", "userinfo");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditPassword([FromForm] EditPasswordVm editpassVm, [FromQuery] string id)
        {
            if (editpassVm.ConfirmPassword != null & editpassVm.OldPassword != null & editpassVm.NewPassword != null)
            {
                var user = await _userManager.FindByIdAsync(id);

                var result =
                    await _userManager.ChangePasswordAsync(user, editpassVm.OldPassword, editpassVm.NewPassword);

                if (result.Succeeded)
                {
                    Log.Information($"UserPassword with id {user.Id} is changed");
                    return Redirect("/gpass");
                }
            }
            Log.Information($"Bad try of change Password");
            return Redirect("/bpass");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout([FromQuery] string id)
        {
            Response.Cookies.Delete("Username");

            return Redirect("/");
        }
    }
}