using Contract.ClientDto;

namespace Eduker.ViewModels.InstructorsVm;

public class CurrentInstructorVm
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Specialization { get; set; }
    public string ImgUrl { get; set; }
    public string Description { get; set; }
    public List<CourseDto> CourseDtos { get; set; }
}