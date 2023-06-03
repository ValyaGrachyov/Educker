using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
        ICourseService CourseService { get; }
        IEventsService EventsService { get; }
        IUserInfoService UserInfoService { get; }
        IInstructorService InstructorService { get; }
        ICommentsService CommentsService { get; }
        IPurchasedCourseService PurchasedCourseService { get; }
    }
}