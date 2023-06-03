using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Eduker.ViewModels.RegistrationVm;
using System.Text;
using Contract.AdminDto.UserInfoDto;
using Contract.ClientDto.UserDto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Serilog;
using Persistence.services.EmailService;
using Persistence.services.GoogleService;

using Services.Abstraction;

namespace Eduker.Controllers
{
    [Route("/registration")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceManager _serviceManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IGoogleService _googleService;

        public RegistrationController(IServiceManager serviceManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IEmailService emailService, IGoogleService googleService)
        {
            _serviceManager = serviceManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
            _googleService = googleService;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();

        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] RegistrationVm registrationVm)
        {
            if (!ModelState.IsValid) return Index();
            
            if(await Registration(registrationVm)) return Redirect("/login");

            return View();

        }

        [HttpGet]
        [Route("signin-google")]
        public async Task<IActionResult> Google([FromQuery]string code, [FromQuery] string scope)
        {
            var getTokenResult = await _googleService.GetTokenAsync(code,
                _configuration["Authentication:Google:ClientId"] ?? "",
                _configuration["Authentication:Google:ClientSecret"] ?? "",
                "https://localhost:44395/registration/signin-google"
            );
            
            if (getTokenResult is null)
                return BadRequest();
            var getUserResult = await _googleService.GetUserAsync(tokenId: getTokenResult.id_token, accessToken: getTokenResult.access_token);

            if (getUserResult is null)
                return BadRequest();

            var password = GeneratePassword();
            
            var name = getUserResult.email.Split('@')[0];

            var email = getUserResult.email;
            
            var registrationVm = new RegistrationVm()
            {
                Email = email,
                Password = password,
                ConfirmPassword = password,
                Name = name
            };
            await _emailService.SendMessageAsync(email, "Thanks for registration",
                $"Your name:{name}\nYour password:{password}");
            if (await Registration(registrationVm)) return Redirect("/login");

            return Redirect("/registration");
        }

        private async Task<bool> Registration(RegistrationVm registrationVm)
        {
            var user = new IdentityUser
            {
                UserName = registrationVm.Name,
                Email = registrationVm.Email
            };

            var result = await _userManager.CreateAsync(user, registrationVm.Password);

            if (!result.Succeeded) return false;
            
            var createdUser = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(createdUser, "simple");
            await _serviceManager.UserInfoService.AddUserInfo(new AddUserInfoDto()
            {
                Email = createdUser.Email,
                IdentityId = createdUser.Id
            });
            await _signInManager.SignInAsync(createdUser, isPersistent: false);
            return true;
        }
        
        public string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            var length = options.RequiredLength;

            var nonAlphanumeric = options.RequireNonAlphanumeric;
            var digit = options.RequireDigit;
            var lowercase = options.RequireLowercase;
            var uppercase = options.RequireUppercase;

            var password = new StringBuilder();
            var random = new Random();

            while (password.Length < length)
            {

                var c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;

            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
    }
}