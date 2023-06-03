using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class EduckerDbContext : IdentityDbContext
    {
        public DbSet<Events> Events { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<PurchasedCourse> PurchasedCourses { get; set; }
        public DbSet<UserInfo> UsersInfo { get; set; }

        // region course
        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagInCourse> TagsInCourses { get; set; }

        public DbSet<CourseReview> CourseReviews { get; set; }

        public DbSet<InstructorInCourse> InstructorInCourses { get; set; }

        // endregion

        public EduckerDbContext(DbContextOptions<EduckerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}