using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<ICourseRepository> _lazyCourseRepository;
        private readonly Lazy<IEventsRepository> _lazyEventsRepository;
        private readonly Lazy<IPurchasedCourseRepository> _lazyPurchasedCourseRepository;
        private readonly Lazy<IUserInfoRepository> _lazyUserInfoRepository;
        private readonly Lazy<IInstructorRepository> _lazyInstructorRepository;
        private readonly Lazy<ICommentsRepository> _lazyCommentsRepository;

        public RepositoryManager(EduckerDbContext dbContext)
        {
            _lazyCourseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(dbContext));
            _lazyPurchasedCourseRepository =
                new Lazy<IPurchasedCourseRepository>(() => new PurchasedCourseRepository(dbContext));
            _lazyUserInfoRepository = new Lazy<IUserInfoRepository>(() => new UserInfoRepository(dbContext));
            _lazyInstructorRepository = new Lazy<IInstructorRepository>(() => new InstructorRepository(dbContext));
            _lazyEventsRepository = new Lazy<IEventsRepository>(() => new EventsRepository(dbContext)); 
            _lazyCommentsRepository = new Lazy<ICommentsRepository>(() => new CommentsRepository(dbContext));
        }

        public IEventsRepository EventsRepository => _lazyEventsRepository.Value;
        public ICourseRepository CourseRepository => _lazyCourseRepository.Value;
        public IPurchasedCourseRepository PurchasedCourseRepository => _lazyPurchasedCourseRepository.Value;
        public IUserInfoRepository UserInfoRepository => _lazyUserInfoRepository.Value;
        public IInstructorRepository InstructorRepository => _lazyInstructorRepository.Value;
        public ICommentsRepository CommentsRepository => _lazyCommentsRepository.Value;
    }
}