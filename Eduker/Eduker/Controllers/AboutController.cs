using Eduker.ViewModels.About;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstraction;

namespace Eduker.Controllers;

[Route("/about")]
public class AboutController : Controller
{
    private readonly IServiceManager _serviceManager;

    public AboutController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt")
            .CreateLogger();
    }
    public async Task<IActionResult> Index()
    {
        try
        {
            var instructorsList = await _serviceManager.InstructorService.GetAllAsync();
            var coursesList = await _serviceManager.CourseService.GetAllAsync();
            var commentsList = await _serviceManager.CommentsService.GetAllAsync();

            var model = new AboutVm()
            {
                Instructors = instructorsList.ToList(),
                Courses = coursesList.ToList(),
                Comments = commentsList.ToList()
            };
            Log.Information("Hi from About");
            return View(model);
        }
        catch (Exception error)
        {
            return NotFound();
        }
    }
}