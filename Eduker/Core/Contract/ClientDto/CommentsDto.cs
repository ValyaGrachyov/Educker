namespace Contract.ClientDto;

public class CommentsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CourseDto Course { get; set; }
    public int Rating { get; set; }
}