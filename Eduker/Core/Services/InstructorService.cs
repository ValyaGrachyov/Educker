using Domain.Repositories;
using Services.Abstraction;
using Contract.ClientDto;
using Contract.ClientDto.CourseDtoFold;

namespace Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IRepositoryManager _repositoryManager;

        public InstructorService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<InstructorDto>> GetAllAsync()
        {
            var instructors = await _repositoryManager.InstructorRepository.GetAllAsync();

            return instructors.Select(i => new InstructorDto
            {
                Id = i.Id,
                Name = i.Name,
                Surname = i.Surname,
                Description = i.Description,
                ImgUrl = i.ImgUrl,
                Specialization = i.Specialization
            });
        }

        public async Task<InstructorDto> FindById(int id)
        {
            var instructor = await _repositoryManager.InstructorRepository.FindById(id);
            
            return new InstructorDto()
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Surname = instructor.Surname,
                Description = instructor.Description,
                ImgUrl = instructor.ImgUrl,
                Specialization = instructor.Specialization
            };
        }

        public async Task<List<CourseDto>> GetInsturctorCourses(int id)
        {
            var courses = await _repositoryManager.InstructorRepository.GetInsturctorCourses(id);
            

            return courses.Select(x => new CourseDto()
            {
                Id = x.Id,
                Name = x.Course.Name,
                Category = new CategoryDto
                {
                    Id = x.Course.Category.Id,
                    CategoryName = x.Course.Category.CategoryName
                },
                //todo
                Tags = null,
                MainInstructor = new InstructorDto
                {
                    Id = x.Course.MainInstructor.Id,
                    Name = x.Course.MainInstructor.Name,
                    Surname = x.Course.MainInstructor.Surname,
                    Specialization = x.Course.MainInstructor.Specialization,
                    ImgUrl = x.Course.MainInstructor.ImgUrl,
                    Description = x.Course.MainInstructor.Description
                },
                OtherInstructors = null,
                Description = x.Course.Description,
                DateCreation = x.Course.DateCreation,
                Duration = x.Course.Duration,
                Lectures = x.Course.Lectures,
                Language = x.Course.Language,
                Price = x.Course.Price,
                ImgUrl = x.Course.ImgUrl,
                Rating = x.Course.Rating,
                Views = x.Course.Views,
                Subscribers = x.Course.Subscribers
            }).ToList();
        }
    }
}