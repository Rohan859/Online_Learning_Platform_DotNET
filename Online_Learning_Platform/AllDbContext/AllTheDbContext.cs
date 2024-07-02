using Microsoft.EntityFrameworkCore;
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
        public DbSet<StudentCourse> StudentCourses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              modelBuilder.Entity<Instructor>()
            .Property(ex => ex.Expertise)
             .HasConversion<string>();


            modelBuilder.Entity<Course>()
           .Property(ex => ex.DifficultyLevel)
            .HasConversion<string>();

                modelBuilder.Entity<Course>()
           .Property(ex => ex.Category)
            .HasConversion<string>();


                modelBuilder.Entity<Enrollment>()
           .Property(ex => ex.Progress)
            .HasConversion<string>();

                modelBuilder.Entity<Instructor>()
           .Property(ex => ex.Expertise)
            .HasConversion<string>();




            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Courses) //one user can have many courses
            //    .WithOne(e => e.User)
            //    .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<StudentCourse>()
                .HasKey(e => new {e.UserId, e.CourseId});


            modelBuilder.Entity<StudentCourse>()
                .HasOne(e => e.User)
                .WithMany(e => e.StudentCourses)
                .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<StudentCourse>()
                .HasOne(e => e.Course)
                .WithMany(e => e.StudentCourses)
                .HasForeignKey(e => e.CourseId);


            modelBuilder.Entity<Course>() //one course can have many instructors
                .HasMany(e => e.Instructors)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);



            modelBuilder.Entity<Course>()
                .HasMany(e => e.Enrollments) //in one course there can be many enrollments
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);


            modelBuilder.Entity<User>()
                .HasMany(e => e.Reviews) //one user can give many reviews
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<Course>()
                .HasMany(e => e.Reviews) // one course can have many reviews
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);




        }
    }
}
