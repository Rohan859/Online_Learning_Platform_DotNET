using Online_Learning_Platform.Profiles;

namespace Online_Learning_Platform.Extension
{
    public static class ProfileExtensions
    {
        public static void AddCustomProfiles(this IServiceCollection services)
        {
            //added profiles
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(ReviewProfile));
            services.AddAutoMapper(typeof(InstructorProfile));
            services.AddAutoMapper(typeof(CourseProfile));
        }
    }
}
