﻿namespace Online_Learning_Platform.Extension
{
    public static class CorsExtensions
    {
        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:3000/") // Replace with your frontend URL
                               .WithOrigins("http://127.0.0.1:5500/")
                                .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }
    }
}