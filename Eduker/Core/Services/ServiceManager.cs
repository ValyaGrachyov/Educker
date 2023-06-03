using Domain.Repositories;
using Services.Abstraction;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICourseService> _lazyCourseService;
        private readonly Lazy<IEventsService> _lazyEventsService;
        private readonly Lazy<IUserInfoService> _lazyUserInfoService;
        private readonly Lazy<IInstructorService> _lazyInstructorService;
        private readonly Lazy<ICommentsService> _lazyCommentsService;
        private readonly Lazy<IPurchasedCourseService> _lazyPurchasedService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyCourseService = new Lazy<ICourseService>(() => new CourseService(repositoryManager));
            _lazyEventsService = new Lazy<IEventsService>(() => new EventsService(repositoryManager));
            _lazyUserInfoService = new Lazy<IUserInfoService>(() => new UserInfoService(repositoryManager));
            _lazyInstructorService = new Lazy<IInstructorService>(() => new InstructorService(repositoryManager));
            _lazyCommentsService = new Lazy<ICommentsService>(() => new CommentsService(repositoryManager));
            _lazyPurchasedService = new Lazy<IPurchasedCourseService>(() => new PurchasedCourseService(repositoryManager));
        }

        public ICourseService CourseService => _lazyCourseService.Value;
        public IEventsService EventsService => _lazyEventsService.Value;
        public IUserInfoService UserInfoService => _lazyUserInfoService.Value;
        public IInstructorService InstructorService => _lazyInstructorService.Value; 
        public ICommentsService CommentsService => _lazyCommentsService.Value; 
        public IPurchasedCourseService PurchasedCourseService => _lazyPurchasedService.Value;
    }
}