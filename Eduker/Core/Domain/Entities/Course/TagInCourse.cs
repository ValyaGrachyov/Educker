using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class TagInCourse
{
    public int Id { get; set; }

    [ForeignKey(nameof(TagId))]
    public int? TagId { get; set; }
    public virtual Tag Tag { get; set; }

    [ForeignKey(nameof(CourseId))]
    public int? CourseId { get; set; }
    public virtual Course Course { get; set; }
}