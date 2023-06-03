using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Eduker.ViewModels.EventsVm;
using Eduker.ViewModels.InstructorsVm;

namespace Eduker.Controllers;

[Route("/events")]
public class EventsController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly UserManager<IdentityUser> _userManager;

    public EventsController(UserManager<IdentityUser> userManager, IServiceManager serviceManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //var result = await _userManager.FindByNameAsync(Request.Cookies["username"]);


        //if (result == null)
        //{
        //    return NotFound();
        //}
        var events = await _serviceManager.EventsService.GetAllAsync();
        var instructorsList = await _serviceManager.InstructorService.GetAllAsync();

        return View(events.Select(x => new EventsVM
            {
                Id = x.Id, Name = x.Name, Description = x.Description, TimeStart = x.TimeStart, TimeEnd = x.TimeEnd,
                Address = x.Address, EducatorName = x.EducatorName, ImgPath= x.ImgPath, InstructorsVm = new InstructorsVm()
                {
                    Instructors = instructorsList.ToList()
                }
            }
        ));
    }
}