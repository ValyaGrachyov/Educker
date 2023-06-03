using Microsoft.AspNetCore.Mvc;

namespace Eduker.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}