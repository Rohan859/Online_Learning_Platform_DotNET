using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Platform.Model;
using System.Reflection.Emit;

namespace Online_Learning_Platform.Configuration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(x => x.InstructorId); //primary key for Instructor table


            builder
            .Property(ex => ex.Expertise) // Expertise enum would be string 
            .HasConversion<string>();

        }
    }
}
