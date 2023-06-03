using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using Contract.ClientDto;
using Eduker.ViewModels.CourseVM;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories;
using Persistence.services.EmailService;
using Domain.Repositories;
using Eduker.ViewModels.PurchaseVm;
using Serilog;

namespace Eduker.Controllers
{
    [Route("/course")]
    public class CourseController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public CourseController(UserManager<IdentityUser> userManager, IEmailService emailService,
            IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _userManager = userManager;
            _emailService = emailService;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var model = await GetModel(id);
                return View(model);
            }
            catch (Exception error)
            {
                return Redirect("/login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] PurchaseVm purchaseVm, int id)
        {
                var result = await _userManager.FindByNameAsync(Request.Cookies["username"]);
                if (result == null)
                {
                    return Redirect("/login");
                }


                var model = await GetModel(id);

                try
                {
                    var isPurchaseVm = purchaseVm.IsNormal();

                    if (isPurchaseVm != null)
                    {
                        model.ErrorMessage = isPurchaseVm;
                        return View(model);
                    }


                    var purchasedCourse = new PurchasedCourseDto
                    {
                        CourseId = Convert.ToInt32(id),
                        UserId = result.Id
                    };


                    var course = await _serviceManager.CourseService.GetCourseByIdAsync(id);
                    await _emailService.SendMessageAsync(purchaseVm.Email,
                        "Thanks for purchasing our course", $"You purchased course: {course.Name}");
                    await _serviceManager.PurchasedCourseService.AddAsync(purchasedCourse);
                    model = await GetModel(id);
                    return View(model);
                }
                catch (Exception e)
                {
                    model.ErrorMessage = "Something went wrong, try again later";
                    return View(model);
                }
        }

        private async Task<CourseDetailVm?> GetModel(int id)
        {
            var relatedCourses = await _serviceManager.CourseService.GetRelatedAsync(id);
            var relatedCoursesList = relatedCourses.ToList();
            var purchasedCourseDtos = await _serviceManager.PurchasedCourseService.GetAllPurchasedByCourseIdAsync(id);
            var members = new List<IdentityUser>();
            foreach (var purchasedCourseDto in purchasedCourseDtos)
            {
                members.Add(await _userManager.FindByIdAsync(purchasedCourseDto.UserId));
            }
            var reviews = await _serviceManager.CourseService.GetCourseReviewsAsync(id);
            
            var reviewsList = reviews?.ToList();
            var course = await _serviceManager.CourseService.GetCourseByIdAsync(id);
            
            if (course == null)
                return null;

            var model = new CourseDetailVm
            {
                Course = course,
                RelatedCourses = relatedCoursesList,
                Reviews = reviewsList,
                Members = members
            };

            var user = await _userManager.FindByNameAsync(Request.Cookies["username"]);
            
            if (await _serviceManager.PurchasedCourseService.FindAsync(new PurchasedCourseDto{CourseId = id,UserId = user.Id}) != null)
            {
                model = new CourseDetailVm
                {
                    Course = course,
                    RelatedCourses = relatedCoursesList,
                    Members = members, 
                    Reviews = reviewsList,
                    UserName =  user.UserName
                };
            }
             Log.Information($" Purchased a Course {user.UserName}");
            return model;

        }
    }
}