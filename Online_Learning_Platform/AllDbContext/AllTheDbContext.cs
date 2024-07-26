using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.Configuration;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Online_Learning_Platform.AllDbContext
{
    public class AllTheDbContext : DbContext
    {
        public AllTheDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        //public DbSet<StudentCourse> StudentCourses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //apply configuration
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());


        }
    }
}
