using Eduker.ViewModels.InstructorsVm;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;

namespace Eduker.Controllers
{
    [Route("/instructor")]
    public class CurrentInstructorController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public CurrentInstructorController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index([FromQuery] int id)
        {
            var instructor = await _serviceManager.InstructorService.FindById(id);
            var courses = await _serviceManager.InstructorService.GetInsturctorCourses(id);
            
            return View(new CurrentInstructorVm()
            {
                Name = instructor.Name,
                Description = instructor.Description,
                Surname = instructor.Surname,
                Specialization = instructor.Specialization,
                ImgUrl = instructor.ImgUrl,
                CourseDtos = courses
            });
        }
    }
}