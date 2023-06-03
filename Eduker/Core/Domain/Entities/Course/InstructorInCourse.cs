using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

//Другие учителя курса
public class InstructorInCourse
{
    public int Id { get; set; }

    [ForeignKey(nameof(InstructorId))] public int? InstructorId { get; set; }
    public virtual Instructor Instructor { get; set; }

    [ForeignKey(nameof(CourseId))] public int? CourseId { get; set; }
    public virtual Course Course { get; set; }
}