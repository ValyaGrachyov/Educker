namespace Domain.Entities;

public class Instructor
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Specialization { get; set; }
    public string ImgUrl { get; set; }
    public string Description { get; set; }
}