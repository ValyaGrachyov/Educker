using Domain.Entities;

namespace Domain.Repositories;

public interface IPurchasedCourseRepository
{
    public Task AddAsync(PurchasedCourse purchasedCourse);
    public Task<PurchasedCourse> FindAsync(PurchasedCourse purchasedCourse);
    public Task<IEnumerable<PurchasedCourse>> GetAllPurchasedByCourseIdAsync(int courseId);
}