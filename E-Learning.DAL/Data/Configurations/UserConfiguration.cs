using E_Learning.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.DAL.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.Property(u => u.FName).IsRequired().HasMaxLength(20);
            //builder.Property(u => u.LName).IsRequired().HasMaxLength(20);
            //builder.Property(u => u.Email).IsRequired().HasMaxLength(50);

            ////Data Seeding
            //builder.HasData(
            //    new User { Id = 1, FName = "Abdallah", LName = "Shatta", Email = "AbdallahShatta@gmail.com", ProfilePicture = ""},
            //    new User { Id = 2, FName = "Mohamed", LName = "Ebrahim", Email = "MohamedErbahim@gmail.com", ProfilePicture = "" },
            //    new User { Id = 3, FName = "Mohsen", LName = "Tayseer", Email = "MohsemTayseer@gmail.com", ProfilePicture = ""},
            //    new User { Id = 4, FName = "Marwa", LName = "Elkasaby", Email = "MarwaElkasaby@gmail.com", ProfilePicture = "" }
            //    );
        }
    }
}
