using Contract.ClientDto;

namespace Eduker.ViewModels.HomeVm;

public class HomeVm
{
    public List<InstructorDto> Instructors { get; set; }
    public List<CommentsDto> Comments { get; set; }
    public List<CourseDto> Courses { get; set; }
    public List<EventsDto> Events { get; set; }
}