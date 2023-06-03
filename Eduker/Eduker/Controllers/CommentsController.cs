using Eduker.ViewModels.Comments;
using Eduker.ViewModels.InstructorsVm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Eduker.Controllers;

public class CommentsController : Controller
{
    private readonly IServiceManager _serviceManager;

    public CommentsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // var comments = await _serviceManager.CommentsService.GetAllAsync();
        //
        // try
        // {
        //     var commentsList = await _serviceManager.CommentsService.GetAllAsync();
        //
        //     var model = new CommentsVm()
        //     {
        //         Comments = commentsList.ToList(),
        //     };
        //     return View(model);
        // }
        // catch (Exception error)
        // {
        //     return NotFound();
        // }
        return View();
    }
}