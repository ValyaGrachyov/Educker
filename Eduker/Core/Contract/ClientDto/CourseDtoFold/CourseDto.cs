using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.ClientDto.CourseDtoFold;
using Contract.ClientDto.UserDto;

namespace Contract.ClientDto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public CategoryDto Category { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }
        public InstructorDto MainInstructor { get; set; }
        public IEnumerable<InstructorDto> OtherInstructors { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreation { get; set; }
        public int Duration { get; set; }
        public int Lectures { get; set; }
        public string? Language { get; set; }
        public int Price { get; set; }
        public string? ImgUrl { get; set; }
        public double Rating { get; set; }
        public int Views { get; set; }
        public int Subscribers { get; set; }
    }
}