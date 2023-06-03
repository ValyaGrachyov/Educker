using Domain.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EduckerDbContext _dbContext;

        public CourseRepository(EduckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .Include(c => c.MainInstructor)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            //выкидвает ArgumentNullException если null
            return await _dbContext.Courses
                .AsNoTracking()
                .Include(c => c.MainInstructor)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Tag>> GetCourseTagsAsync(int id)
        {
            return await _dbContext.TagsInCourses
                .Where(tic => tic.CourseId == id)
                .Select(tic => tic.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseReview>> GetCourseReviewsAsync(int id)
        {
            try
            {
                var temp = _dbContext.CourseReviews;
                    // .Include(c => c.UserInfo)
                    // .Include(c => c.Course)
                    //   .Where(tic => tic.CourseId == id);
                    // .Include(c => c.UserInfo)
                    // .ToListAsync();
                return await _dbContext.CourseReviews
                    .Where(tic => tic.CourseId == id)
                    .Include(c => c.UserInfo)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Category
                .AsNoTracking()
                .ToListAsync();
        }
    }
}