namespace Online_Learning_Platform.Extension
{
    public static class RedisExtensions
    {
        public static void AddCustomRedis(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379"; // Replace with your Redis server connection string
                options.InstanceName = "SampleInstance"; // Optional: If you have multiple apps sharing the same Redis instance
            });
        }
    }
}
