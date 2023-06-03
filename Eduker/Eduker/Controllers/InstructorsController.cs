using Eduker.ViewModels.InstructorsVm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Eduker.Controllers;

[Route("/instructors")]
public class InstructorsController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly UserManager<IdentityUser> _userManager;

    public InstructorsController(UserManager<IdentityUser> userManager, IServiceManager serviceManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var instructorsList = await _serviceManager.InstructorService.GetAllAsync();
            var commentsList = await _serviceManager.CommentsService.GetAllAsync();
            
            var model = new InstructorsVm()
            {
                Instructors = instructorsList.ToList(),
                Comments = commentsList.ToList()
            };
            return View(model);
        }
        catch (Exception error)
        {
            return NotFound();
        }
    }
}
