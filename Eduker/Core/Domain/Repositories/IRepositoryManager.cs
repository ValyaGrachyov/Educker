namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        ICourseRepository CourseRepository { get; }
        IEventsRepository EventsRepository { get; }
        IPurchasedCourseRepository PurchasedCourseRepository { get; }
        IUserInfoRepository UserInfoRepository { get; }
        IInstructorRepository InstructorRepository { get; }
        ICommentsRepository CommentsRepository { get; }
    }
}