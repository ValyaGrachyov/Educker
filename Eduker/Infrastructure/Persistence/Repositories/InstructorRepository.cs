using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly EduckerDbContext _dbContext;

        public InstructorRepository(EduckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _dbContext.Instructors.ToListAsync();
        }

        public async Task<IEnumerable<Instructor>> GetCourseInstructorsAsync(int id)
        {
            return await _dbContext.InstructorInCourses
                .Where(x => x.CourseId == id)
                .Select(x => x.Instructor)
                .ToListAsync();
        }

        public async Task<Instructor> FindById(int id)
        {
            return await _dbContext.Instructors.FindAsync(id);
        }

        public async Task<List<InstructorInCourse>> GetInsturctorCourses(int id)
        {
            var subject = _dbContext.InstructorInCourses
                .AsNoTracking()
                .Include(c => c.Course)
                .Include(i => i.Course.MainInstructor)
                .Include(c => c.Course.Category)
                .Select(x => x)
                .Where(x => x.InstructorId == id)
                .ToList();
            
            return subject;
        }
    }
}