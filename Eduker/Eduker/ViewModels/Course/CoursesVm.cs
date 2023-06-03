using Contract.ClientDto;
using Contract.ClientDto.CourseDtoFold;

namespace Eduker.ViewModels.CourseVM;

public class CoursesVm
{

    public List<CourseDto> Courses { get; set; }

    public List<CourseDto> RelatedCourses { get; set; }
    
    public List<CategoryDto> Categories { get; set; }

}