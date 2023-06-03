using Contract.ClientDto;

namespace Eduker.ViewModels.InstructorsVm;

public class InstructorsVm
{
    public List<InstructorDto> Instructors { get; set; }
    public List<CommentsDto> Comments { get; set; }
}