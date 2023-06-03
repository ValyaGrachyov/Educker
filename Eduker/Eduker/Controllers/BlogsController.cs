using Microsoft.AspNetCore.Mvc;

namespace Eduker.Controllers;

[Route("/blogs")]
public class BlogsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}