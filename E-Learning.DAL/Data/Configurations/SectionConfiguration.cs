using E_Learning.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.DAL.Data.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.Property(s => s.Title).IsRequired().HasMaxLength(100);
            builder.HasOne(s => s.Course).WithMany(c => c.Sections).HasForeignKey(s => s.CourseId);

            ////Data Seeding
            //builder.HasData(new Section { Id = 1, CourseId = 1, Title = "intro", LessonsNo = 3 },
            //                new Section { Id = 2, CourseId = 1, Title = "OOP", LessonsNo = 5 },
            //                new Section { Id = 3, CourseId = 2, Title = "Binary search", LessonsNo = 3 },
            //                new Section { Id = 4, CourseId = 3, Title = "Nutrition", LessonsNo = 4 },
            //                new Section { Id = 5, CourseId = 4, Title = "General", LessonsNo = 3 }
            //    );

        }
    }
}