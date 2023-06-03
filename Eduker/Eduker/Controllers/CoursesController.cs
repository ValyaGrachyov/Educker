using Contract.ClientDto;
using Eduker.ViewModels.CourseVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;

namespace Eduker.Controllers;

[Route("/courses")]
public class CoursesController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly UserManager<IdentityUser> _userManager;

    // private int? categoryId = null;

    public CoursesController(UserManager<IdentityUser> userManager, IServiceManager serviceManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    public async Task<IActionResult> Index(int? page, int? categoryId)
    {
        var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        var pageSize = 6;
        try
        {
            var categories = await _serviceManager.CourseService.GetCategoriesAsync();

            var coursesList = await _serviceManager.CourseService.GetAllAsync();
            var relatedCourses = await _serviceManager.CourseService.GetRelatedAsync(1);

            var courseDtos = coursesList.ToList();

            // var data = generatePaginatedData(page, courseDtos);
            // if (categoryId != null)
            // {
            //     data = data.Where(x => x.Category.Id == categoryId.Value).ToList();
            //     ViewBag.CategoryId = categoryId;
            // }
            // var model = new CoursesVm()
            // {
            //     Courses = data ,
            //     RelatedCourses = relatedCourses.ToList(),
            //     Categories = categories.ToList()
            // };
            // return View(model);
            if (page == null && categoryId != null)
            {
                courseDtos = courseDtos.Where(x => x.Category.Id == categoryId.Value).ToList();
                ViewBag.CategoryId = categoryId;
                ViewBag.CurrentPage = 1;
                
                return View(new CoursesVm()
                {
                    Courses = courseDtos,
                    RelatedCourses = relatedCourses.ToList(),
                    Categories = categories.ToList()
                });
            }
            else
            {
                ViewBag.CategoryId = null;
                
                return View(new CoursesVm()
                {
                    Courses = generatePaginatedData(page,courseDtos),
                    RelatedCourses = relatedCourses.ToList(),
                    Categories = categories.ToList()
                });
            }
        }
        catch (Exception error)
        {
            return NotFound();
        }
    }

    private List<CourseDto> generatePaginatedData(int? page, List<CourseDto> courses)
    {
        var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        var pageSize = 6;

        var data = courses.OrderBy(d => d.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        if (!data.IsNullOrEmpty())
        {
            ViewBag.CurrentPage = pageNumber;
            return data;
        }

        pageNumber--;
        data = courses.OrderBy(d => d.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        ViewBag.CurrentPage = pageNumber;
        return data;
    }
}