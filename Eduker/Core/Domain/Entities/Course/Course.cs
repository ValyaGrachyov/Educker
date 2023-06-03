using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreation { get; set; }
        public int Duration { get; set; }
        public int Lectures { get; set; }
        public string? Language { get; set; }
        public int Price { get; set; }
        public string ImgUrl { get; set; }
        public double Rating { get; set; }

        public int Views { get; set; }

        // Подписчики которые купили курс
        public int Subscribers { get; set; }


        [ForeignKey(nameof(CategoryId))] public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(MainInstructorId))] public int? MainInstructorId { get; set; }
        public virtual Instructor MainInstructor { get; set; }
    }
}