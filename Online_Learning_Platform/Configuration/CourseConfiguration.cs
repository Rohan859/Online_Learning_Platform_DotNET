using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Platform.Model;
using System.Reflection.Emit;

namespace Online_Learning_Platform.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseId); //primary key for Course table


            builder
                .HasMany(x => x.Users) // many to many mapping with user
                .WithMany(x => x.Courses);


            builder
                .HasMany(e => e.Reviews) // one course can have many reviews
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);


            builder
               .HasMany(e => e.Enrollments) //in one course there can be many enrollments
               .WithOne(e => e.Course)
               .HasForeignKey(e => e.CourseId);


            builder     //one course can have many instructors
               .HasMany(e => e.Instructors)
               .WithOne(e => e.Course)
               .HasForeignKey(e => e.CourseId);


            builder  //DifficultyLevel enum would be string 
            .Property(ex => ex.DifficultyLevel)
            .HasConversion<string>();


            builder  //Category enum would be string
                .Property(ex => ex.Category)
                .HasConversion<string>();


        }
    }
}
