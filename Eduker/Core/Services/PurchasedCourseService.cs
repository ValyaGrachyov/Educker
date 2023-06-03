using Contract.AdminDto.UserInfoDto;
using Contract.ClientDto;
using Contract.ClientDto.UserDto;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction;

namespace Services;

public class PurchasedCourseService: IPurchasedCourseService
{
    private readonly IRepositoryManager _repositoryManager;

    public PurchasedCourseService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task AddAsync(PurchasedCourseDto purchasedCourseDto)
    {
        
        await _repositoryManager.PurchasedCourseRepository.AddAsync(new PurchasedCourse
        {
            CourseId = purchasedCourseDto.CourseId,
            UserId = purchasedCourseDto.UserId
        });
    }

    public async Task<PurchasedCourseDto> FindAsync(PurchasedCourseDto purchasedCourseDto)
    {
        var purchasedCourse = await _repositoryManager.PurchasedCourseRepository.FindAsync(new PurchasedCourse
        {
            CourseId = purchasedCourseDto.CourseId,
            UserId = purchasedCourseDto.UserId
        });
        if (purchasedCourse == null) return null;
            return new PurchasedCourseDto
        {
            CourseId = purchasedCourse.CourseId,
            UserId = purchasedCourseDto.UserId
        };
    }

    public async Task<IEnumerable<PurchasedCourseDto>> GetAllPurchasedByCourseIdAsync(int courseId)
    {
        var userInfos = await _repositoryManager.PurchasedCourseRepository.GetAllPurchasedByCourseIdAsync(courseId);
            

        return userInfos.Select(x => new PurchasedCourseDto()
        {
            CourseId = x.CourseId,
            UserId = x.UserId
        }).ToList();
    }
}