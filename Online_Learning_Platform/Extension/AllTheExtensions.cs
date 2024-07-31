namespace Online_Learning_Platform.Extension
{
    public static class AllTheExtensions
    {
        public static void AddAllTheExtensions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCustomServices();
            services.AddCustomRepositories();
            services.AddCustomProfiles();
            services.AddCustomSwagger();
            services.AddCustomJsonOptions();
            services.AddCustomCors();
            services.AddCustomCacheMemory();
            services.AddJwtBearer(configuration);
            services.AddController();
        }
    }
}
