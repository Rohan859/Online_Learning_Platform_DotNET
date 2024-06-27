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
        }
    }
}
