namespace Online_Learning_Platform.Extension
{
    public static class CacheMemoryExtensions
    {
        public static void AddCustomCacheMemory(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddOutputCache();
            services.AddDistributedMemoryCache();
        }
    }
}
