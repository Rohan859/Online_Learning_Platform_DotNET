using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Extension;

using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Profiles;
using Online_Learning_Platform.Repository;
using Online_Learning_Platform.RepositoryInterface;
using Online_Learning_Platform.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();


builder.Services.AddDbContext<AllTheDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAllTheExtensions();




var app = builder.Build();




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
