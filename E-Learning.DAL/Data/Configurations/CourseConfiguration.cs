using E_Learning.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.DAL.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {

            builder.HasOne(c => c.User).WithMany(i => i.OwnedCourses).HasForeignKey(c => c.UserId);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(50);
            //builder.Property(c => c.Description).IsRequired();
            //builder.Property(c => c.CoverPicture).IsRequired();
            builder.Property(c => c.Price).IsRequired();
            //builder.Property(c => c.CreationDate).HasDefaultValueSql();

            ////Data Seeding
            //builder.HasData(
            //    new Course { Id = 1, Title = "C# From Zero To SuperHero", CategoryId = 1, InstructorId = 1 },
            //    new Course { Id = 2, Title = "Data Strcutre", CategoryId = 1, InstructorId = 1 },
            //    new Course { Id = 3, Title = "Diet", CategoryId = 2, InstructorId = 2 },
            //    new Course { Id = 4, Title = "GYM", CategoryId = 2, InstructorId = 2 }
            //    );
        }
    }
}
