using Online_Learning_Platform.Repository;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Extension
{
    public static class RepositoryExtensions
    {
        public static void AddCustomRepositories(this IServiceCollection services)
        {
            //added scoped for repositories
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}
