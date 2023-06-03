using Microsoft.AspNetCore.Mvc;

namespace Eduker.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}