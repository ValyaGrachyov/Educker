using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("Id")]
public class PurchasedCourse
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int CourseId { get; set; }
    
    
}