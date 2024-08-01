using Online_Learning_Platform.Filter;

namespace Online_Learning_Platform.Extension
{
    public static class ControllerExtensions
    {
        public static void AddController(this IServiceCollection services)
        {
            services.AddControllers
                (
                    option =>
                    {
                        option.Filters.Add<ExecutionTimeFilter>(); 
                        option.Filters.Add<AuthorizationFilter>();
                    }
                );
        }
    }
}
