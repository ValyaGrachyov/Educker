using Contract;
using Contract.ClientDto;
using Contract.ClientDto.CourseDtoFold;

namespace Services.Abstraction
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseDto>> GetAllAsync();

        public Task<CourseDto?> GetCourseByIdAsync(int id);

        public Task<IEnumerable<CourseDto>> GetRelatedAsync(int id);
        public Task<IEnumerable<CourseReviewDto>> GetCourseReviewsAsync(int id);
        
        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}