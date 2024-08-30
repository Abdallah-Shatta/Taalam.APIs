using E_Learning.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.DAL.Data.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(l => l.Title).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Content).IsRequired();
            builder.HasOne(l => l.Section).WithMany(s => s.Lessons).HasForeignKey(l => l.SectionId);

            ////Data Seeding
            //builder.HasData(new Lesson { Id = 1, SectionId = 1, Title = "orientation", Content = "https://" },
            //                new Lesson { Id = 2, SectionId = 1, Title = "introduction", Content = "https://" },
            //                new Lesson { Id = 3, SectionId = 2, Title = "Encpsulation", Content = "https://" },
            //                new Lesson { Id = 4, SectionId = 2, Title = "inheritance", Content = "https://" },
            //                new Lesson { Id = 5, SectionId = 2, Title = "abstraction", Content = "https://" },
            //                new Lesson { Id = 6, SectionId = 2, Title = "polymorphism", Content = "https://" },
                                          
            //                new Lesson { Id = 7, SectionId = 3, Title = "intoduction", Content = "https://" },
            //                new Lesson { Id = 8, SectionId = 3, Title = "Binary tree", Content = "https://" },
                                          
            //                new Lesson { Id = 9, SectionId = 4, Title = "introduction", Content = "https://" },
            //                new Lesson { Id = 10, SectionId = 4, Title = "hum ya gamal", Content = "https://" },
            //                new Lesson { Id = 11, SectionId = 4, Title = "calories", Content = "https://" },
                                          
            //                new Lesson { Id = 12, SectionId = 5, Title = "3ash ya wa7sh", Content = "https://" },
            //                new Lesson { Id = 13, SectionId = 5, Title = "el3ab sa7", Content = "https://" }
            //    );
        }
    }
}
