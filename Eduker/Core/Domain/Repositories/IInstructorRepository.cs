using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IInstructorRepository
    {
        public Task<IEnumerable<Instructor>> GetAllAsync();
        public Task<IEnumerable<Instructor>> GetCourseInstructorsAsync(int id);
        public Task<Instructor> FindById(int id);
        public  Task<List<InstructorInCourse>> GetInsturctorCourses(int id);


    }
}