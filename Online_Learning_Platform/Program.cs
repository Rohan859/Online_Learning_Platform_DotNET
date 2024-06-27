using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AllTheDbContext>(options => 
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AllTheDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<InstructorService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<EnrollmentService>();



var app = builder.Build();


// Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Add routing middleware
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
