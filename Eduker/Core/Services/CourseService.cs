using Azure.Core;
using Contract.ClientDto;
using Contract.ClientDto.CourseDtoFold;
using Contract.ClientDto.UserDto;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.VisualBasic;
using Services.Abstraction;

namespace Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CourseService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _repositoryManager.CourseRepository.GetAllAsync();

            return courses.Select(x => new CourseDto
            {
                Id = x.Id,
                Name = x.Name,
                Category = new CategoryDto
                {
                    Id = x.Category.Id,
                    CategoryName = x.Category.CategoryName
                },
                //todo
                Tags = null,
                MainInstructor = new InstructorDto
                {
                    Id = x.MainInstructor.Id,
                    Name = x.MainInstructor.Name,
                    Surname = x.MainInstructor.Surname,
                    Specialization = x.MainInstructor.Specialization,
                    ImgUrl = x.MainInstructor.ImgUrl,
                    Description = x.MainInstructor.Description
                },
                OtherInstructors = null,
                Description = x.Description,
                DateCreation = x.DateCreation,
                Duration = x.Duration,
                Lectures = x.Lectures,
                Language = x.Language,
                Price = x.Price,
                ImgUrl = x.ImgUrl,
                Rating = x.Rating,
                Views = x.Views,
                Subscribers = x.Subscribers
            });
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _repositoryManager.CourseRepository.GetByIdAsync(id);

            if (course == null)
            {
                return null;
            }

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Category = new CategoryDto
                {
                    Id = course.Category.Id,
                    CategoryName = course.Category.CategoryName
                },
                Tags = await GetCourseTagsAsync(course.Id),
                MainInstructor = new InstructorDto
                {
                    Id = course.MainInstructor.Id,
                    Name = course.MainInstructor.Name,
                    Surname = course.MainInstructor.Surname,
                    Specialization = course.MainInstructor.Specialization,
                    ImgUrl = course.MainInstructor.ImgUrl,
                    Description = course.MainInstructor.Description
                },
                OtherInstructors = await GetCourseInstructorsAsync(course.Id),
                Description = course.Description,
                DateCreation = course.DateCreation,
                Duration = course.Duration,
                Lectures = course.Lectures,
                Language = course.Language,
                Price = course.Price,
                ImgUrl = course.ImgUrl,
                Rating = course.Rating,
                Views = course.Views,
                Subscribers = course.Subscribers
            };
        }

        public async Task<IEnumerable<CourseDto>> GetRelatedAsync(int id)
        {
            var list = await GetAllAsync();
            return list
                .Where(x => x.Id != id)
                .OrderBy(x => x.DateCreation);
        }

        public async Task<IEnumerable<CourseReviewDto>?> GetCourseReviewsAsync(int id)
        {
            var reviews = await _repositoryManager.CourseRepository.GetCourseReviewsAsync(id);
            if (reviews == null)
                return null;
            
            return reviews.Select(x => new CourseReviewDto
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                Description = x.Description,
                Raiting = x.Raiting,
                UserInfoDto = new UserInfoDto
                {
                    Id = x.UserInfo.Id,
                    Address = x.UserInfo.Address,
                    RealName = x.UserInfo.RealName,
                    UserIdentityId = x.UserInfo.IdentityUser.Id
                },

            });
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _repositoryManager.CourseRepository.GetCategoriesAsync();
            return categories.Select(x =>
                new CategoryDto()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName
                }
            );
        }

        private async Task<IEnumerable<TagDto>> GetCourseTagsAsync(int id)
        {
            var tags = await _repositoryManager.CourseRepository.GetCourseTagsAsync(id);
            return tags.Select(x =>
                new TagDto()
                {
                    Id = x.Id,
                    TagName = x.TagName
                });
        }

        private async Task<IEnumerable<InstructorDto>> GetCourseInstructorsAsync(int id)
        {
            var instructors = await _repositoryManager.InstructorRepository.GetCourseInstructorsAsync(id);

            return instructors.Select(x =>
                new InstructorDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Specialization = x.Specialization,
                    ImgUrl = x.ImgUrl,
                    Description = x.ImgUrl
                });
        }
    }
}