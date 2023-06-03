using Contract.AdminDto.UserInfoDto;
using Contract.ClientDto;
using Contract.ClientDto.UserDto;

namespace Services.Abstraction;

public interface IPurchasedCourseService
{
    public Task AddAsync(PurchasedCourseDto purchasedCourseDto);
    public Task<PurchasedCourseDto> FindAsync(PurchasedCourseDto purchasedCourseDto);

    public Task<IEnumerable<PurchasedCourseDto>> GetAllPurchasedByCourseIdAsync(int courseId);
}