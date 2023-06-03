using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class PurchasedCourseRepository : IPurchasedCourseRepository
{
    private readonly EduckerDbContext _dbContext;

    public PurchasedCourseRepository(EduckerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(PurchasedCourse purchasedCourse)
    {
        try
        {
            await _dbContext.PurchasedCourses.AddAsync(purchasedCourse);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<PurchasedCourse> FindAsync(PurchasedCourse purchasedCourse)
    {
        return await _dbContext.PurchasedCourses.FirstOrDefaultAsync(x =>
            x.CourseId == purchasedCourse.CourseId && x.UserId == purchasedCourse.UserId);
    }

    public async Task<IEnumerable<PurchasedCourse>> GetAllPurchasedByCourseIdAsync(int courseId)
    {
        // var temp = _dbContext.PurchasedCourses
        //     .Where(x => x.CourseId == courseId)
        //     .Include(c => c.UserInfo);

        return await _dbContext.PurchasedCourses
            .Where(x => x.CourseId == courseId).ToListAsync();
        //.Include(c => c.UserInfo)
    }
}