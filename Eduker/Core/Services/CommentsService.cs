using Contract.ClientDto;
using Contract.ClientDto.CourseDtoFold;
using Domain.Repositories;
using Services.Abstraction;

namespace Services;

public class CommentsService : ICommentsService
{
    private readonly IRepositoryManager _repositoryManager;

    public CommentsService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<IEnumerable<CommentsDto>> GetAllAsync()
    {
        var comments = await _repositoryManager.CommentsRepository.GetAllAsync();

        return comments.Select(i => new CommentsDto
        {
            Id = i.Id,
            Name = i.Name,
            Title = i.Title,
            Description = i.Description,
            Rating = i.Rating,
            Course = new CourseDto
            {
                Id = i.Course.Id,
                Name = i.Name,
                Category = new CategoryDto
                {
                    Id = i.Course.Category.Id,
                    CategoryName = i.Course.Category.CategoryName
                },
                //todo
                Tags = null,
                MainInstructor = new InstructorDto
                {
                    Id = i.Course.MainInstructor.Id,
                    Name = i.Course.MainInstructor.Name,
                    Surname = i.Course.MainInstructor.Surname,
                    Specialization = i.Course.MainInstructor.Specialization,
                    ImgUrl = i.Course.MainInstructor.ImgUrl,
                    Description = i.Course.MainInstructor.Description
                },
                OtherInstructors = null,
                Description = i.Course.Description,
                DateCreation = i.Course.DateCreation,
                Duration = i.Course.Duration,
                Lectures = i.Course.Lectures,
                Language = i.Course.Language,
                Price = i.Course.Price,
                ImgUrl = i.Course.ImgUrl,
                Rating = i.Course.Rating,
                Views = i.Course.Views,
                Subscribers = i.Course.Subscribers
            }

    });
    }
}