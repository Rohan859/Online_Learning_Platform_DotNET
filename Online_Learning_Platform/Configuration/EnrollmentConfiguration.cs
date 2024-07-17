using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Platform.Model;
using System.Reflection.Emit;

namespace Online_Learning_Platform.Configuration
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(x => x.EnrollmentId); //primary key for Enrollment table


            builder
            .Property(ex => ex.Progress) // Progress enum would be string
            .HasConversion<string>();
        }
    }
}
