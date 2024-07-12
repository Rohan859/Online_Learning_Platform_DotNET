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

//builder.Services.AddDbContext<AllTheDbContext>(options => 
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AllTheDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//added scoped for services
//builder.Services.AddScoped<IUserService,UserService>();
//builder.Services.AddScoped<IInstructorService,InstructorService>();
//builder.Services.AddScoped<ICourseService,CourseService>();
//builder.Services.AddScoped<IEnrollmentService,EnrollmentService>();
//builder.Services.AddScoped<ICourseAnalyticsService,CourseAnalyticsService>();
//builder.Services.AddScoped<IReviewService,ReviewService>();

builder.Services.AddAllTheExtensions();


//added scoped for repositories
//builder.Services.AddScoped<IEnrollmentRepository,EnrollmentRepository>();
//builder.Services.AddScoped<IUserRepository,UserRepository>();
//builder.Services.AddScoped<ICourseRepository,CourseRepository>();
//builder.Services.AddScoped<IInstructorRepository,InstructorRepository>();
//builder.Services.AddScoped<IReviewRepository,ReviewRepository>();


//added profiles
//builder.Services.AddAutoMapper(typeof(UserProfile));
//builder.Services.AddAutoMapper(typeof(ReviewProfile));
//builder.Services.AddAutoMapper(typeof(InstructorProfile));
//builder.Services.AddAutoMapper(typeof(CourseProfile));

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
