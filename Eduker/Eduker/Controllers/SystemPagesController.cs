using Microsoft.AspNetCore.Mvc;

namespace Eduker.Controllers
{
    public class SystemPagesController : Controller
    {
        [Route("/404")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/gpass")]
        public IActionResult PasswordChangedCorrect()
        {
            return View();
        }

        [Route("/bpass")]
        public IActionResult PasswordChangedIncorrect()
        {
            return View();
        }
    }
}