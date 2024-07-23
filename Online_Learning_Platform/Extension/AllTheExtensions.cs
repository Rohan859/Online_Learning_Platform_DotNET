namespace Online_Learning_Platform.Extension
{
    public static class AllTheExtensions
    {
        public static void AddAllTheExtensions(this IServiceCollection services)
        {
            services.AddCustomServices();
            services.AddCustomRepositories();
            services.AddCustomProfiles();
            services.AddCustomSwagger();
            services.AddCustomJsonOptions();
            services.AddCustomCacheMemory();
            services.AddCustomCors();
        }
    }
}
