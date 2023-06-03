using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICourseRepository
    {
        public Task<IEnumerable<Course>> GetAllAsync();
        public Task<Course> GetByIdAsync(int id);
        public Task<IEnumerable<Tag>> GetCourseTagsAsync(int id);
        public Task<IEnumerable<CourseReview>> GetCourseReviewsAsync(int id);
        public Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}