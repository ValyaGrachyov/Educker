using Eduker.Areas.Admin.ViewModels.UserVm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Eduker.Areas.Admin.ViewModels.EventsVM;
using Domain.Entities;
using Eduker.ViewModels.InstructorsVm;
using Contract.AdminDto.UserInfoDto;
using Contract.AdminDto.EventsInfoDto;
using Contract.ClientDto.UserDto;

namespace Eduker.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceManager _serviceManager;

        public EventsController(IServiceManager serviceManager, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cookieVal = Request.Cookies["Username"];

            if (cookieVal == null)
            {
                return Redirect("/login");
            }

            var user = await _userManager.FindByNameAsync(cookieVal);
            if (user == null)
            {
                return Redirect("/login");
            }
            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                var events = await _serviceManager.EventsService.GetAllAsync();

                return View(events.Select(x => new EventsVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    TimeStart = x.TimeStart,
                    TimeEnd = x.TimeEnd,
                    Address = x.Address,
                    EducatorName = x.EducatorName,
                    ImgPath = x.ImgPath,
                }
                ).ToList());
            }
            return Forbid();
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                await _serviceManager.EventsService.DeleteEventsInfo(id);
                return RedirectToAction("Index", "Events", new { area = "Admin" });
            }

            return Forbid();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] AddEventsVM eventsVm)
        {
            if (ModelState.IsValid)
            {
                var userName = Request.Cookies["Username"];
                if (userName == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByNameAsync(userName);

                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    var newEvents = new AddEventsInfoDto
                    {
                        Name = eventsVm.Name,
                        Address = eventsVm.Address,
                        Description = eventsVm.Description,
                        TimeStart = eventsVm.TimeStart.Value,
                        TimeEnd = eventsVm.TimeEnd.Value,
                        EducatorName = eventsVm.EducatorName,
                        Language = eventsVm.Language,
                        Price = eventsVm.Price,
                        ImgPath = eventsVm.ImgPath,
                        Enrolled=1
                    };
                    await _serviceManager.EventsService.AddEventsInfo(newEvents);
                    return await Index();
                }

                return Forbid();
            }

            return await Index();
        }
        [HttpGet]
        public async Task<IActionResult> EditEvent([FromQuery] int id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                var currentEvent=await _serviceManager.EventsService.GetInfoAsync(id);
                return View(new EditEventsVM
                {
                    Name = currentEvent.Name,
                    Address = currentEvent.Address,
                    Description = currentEvent.Description,
                    TimeStart = currentEvent.TimeStart,
                    TimeEnd = currentEvent.TimeEnd,
                    EducatorName = currentEvent.EducatorName,
                    Language = currentEvent.Language,
                    Price = currentEvent.Price,
                    ImgPath = currentEvent.ImgPath
                });
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent([FromForm] EditEventsVM editEventsVm, [FromQuery] int id)
        {
            var userName = Request.Cookies["Username"];
            if (userName == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                if (ModelState.IsValid)
                {
                   
                    var eventInfo = await _serviceManager.EventsService.GetInfoAsync(id);
                    await _serviceManager.EventsService.EditEventsInfo(new EditEventsInfoDto
                    {
                        Id = eventInfo.Id,
                        Name = editEventsVm.Name,
                        Address = editEventsVm.Address,
                        Description = editEventsVm.Description,
                        TimeStart = editEventsVm.TimeStart.Value,
                        TimeEnd = editEventsVm.TimeEnd.Value,
                        EducatorName = editEventsVm.EducatorName,
                        Language = editEventsVm.Language,
                        Price = editEventsVm.Price,
                        ImgPath = editEventsVm.ImgPath
                    });
                    return RedirectToAction("Index", "Events", new { area = "Admin" });
                }

                return await EditEvent(id);
            }

            return Forbid();
        }
    }
}
