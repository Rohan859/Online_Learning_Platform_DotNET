using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Extension
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            //added scoped for services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ICourseAnalyticsService, CourseAnalyticsService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
