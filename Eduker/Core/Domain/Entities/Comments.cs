using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Comments
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    [ForeignKey(nameof(CourseId))]
    public int? CourseId { get; set; }
    public virtual Course Course { get; set; }
    public int Rating { get; set; }
}