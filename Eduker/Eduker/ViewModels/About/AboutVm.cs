using Contract.ClientDto;

namespace Eduker.ViewModels.About;

public class AboutVm
{
    public List<InstructorDto> Instructors { get; set; }
    public List<CommentsDto> Comments { get; set; }
    public List<CourseDto> Courses { get; set; }
}