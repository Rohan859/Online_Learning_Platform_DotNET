using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Extension;

using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Profiles;
using Online_Learning_Platform.Reflection;
using Online_Learning_Platform.Repository;
using Online_Learning_Platform.RepositoryInterface;
using Online_Learning_Platform.Service;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();


builder.Services.AddDbContext<AllTheDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);

//added distributed cache
builder.Services.AddDistributedMemoryCache();


//builder.Services.AddDbContext<AllTheDbContext>(options =>
//    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
//    ServiceLifetime.Scoped);



builder.Services.AddAllTheExtensions();
builder.Services.AddScoped<DemoClass>(serviceProvider =>
{
    // Resolve the actual service instance needed by DemoClass
    var serviceInstance = serviceProvider.GetService<ICourseService>(); // Adjust as per your actual service type

    // Create an instance of DemoClass with the resolved service instance
    return new DemoClass(serviceInstance);
});
builder.Services.AddScoped<LearningReflection>();






var app = builder.Build();


app.UseCors("AllowSpecificOrigin"); //cors

// Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Register GlobalExceptionMiddleware as middleware


// Add routing middleware
app.UseRouting();


//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.MapControllers();
app.Run();
