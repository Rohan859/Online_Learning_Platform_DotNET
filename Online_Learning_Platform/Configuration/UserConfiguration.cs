using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Learning_Platform.Model;
using System.Reflection.Emit;

namespace Online_Learning_Platform.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);  //primary key for user table


            builder
               .HasMany(e => e.Enrollments)  //one user can make many enrollments
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId);



            builder
                .HasMany(e => e.Reviews) //one user can give many reviews
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
    }
}
