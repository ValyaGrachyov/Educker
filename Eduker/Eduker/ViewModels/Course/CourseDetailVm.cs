using Contract.ClientDto;
using Contract.ClientDto.CourseDtoFold;
using Contract.ClientDto.UserDto;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Eduker.ViewModels.CourseVM
{
    public class CourseDetailVm
    {
        public CourseDto Course { get; set; }

        public List<CourseDto> RelatedCourses { get; set; }
        
        public List<IdentityUser> Members { get; set; }

        public List<CourseReviewDto> Reviews { get; set; }

        public PurchaseVm.PurchaseVm PurchasedCourse { get; }
        
        public string UserName { get; set; }
        
        public string ErrorMessage { get; set; }
        
    }
}