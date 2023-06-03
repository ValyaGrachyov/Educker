using Eduker.ViewModels.EventsVm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;

namespace Eduker.Controllers
{
    [Route("/eventDetails")]
    public class EventDetailsController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly UserManager<IdentityUser> _userManager;

        public EventDetailsController(UserManager<IdentityUser> userManager, IServiceManager serviceManager)
        {
            _userManager = userManager;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            //var result = await _userManager.FindByNameAsync(Request.Cookies["username"]);


            //if (result == null)
            //{
            //    return NotFound();
            //}
            var eventInfo = await _serviceManager.EventsService.GetInfoAsync(id);

            return View(new EventDetailsVM
            {
                Id = eventInfo.Id,
                Name = eventInfo.Name,
                Description = eventInfo.Description,
                TimeStart = eventInfo.TimeStart,
                TimeEnd = eventInfo.TimeEnd,
                EducatorName = eventInfo.EducatorName,
                Address = eventInfo.Address,
                Price = eventInfo.Price,
                ImgPath = eventInfo.ImgPath
            });

            //1. Починить вёрстку, пусть будет статика но та которая тебе нужна(+)
            //2. Добавить в контроллер получение RepositoryManager 
            //3. Добавить в EventRepository метод для получения информации о событии по его Id 
            //4. Добавил бы конвертацию в DTO полученного event
            //5. Пробрасывал DTO в качестве модели во View
            //6. Заменил бы статику на поля модели
        }
    }
}