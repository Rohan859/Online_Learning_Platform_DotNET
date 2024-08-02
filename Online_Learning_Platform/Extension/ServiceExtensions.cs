using Online_Learning_Platform.Filter;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Service;
using Online_Learning_Platform.ServiceInterfaces;

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

            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IAdminService, AdminService>();


            services.AddSingleton<ISingleton,Singleton>();
            services.AddScoped<IScoped,Scope>();
            services.AddTransient<ITransient,Transient>();


           
        }
    }
}
