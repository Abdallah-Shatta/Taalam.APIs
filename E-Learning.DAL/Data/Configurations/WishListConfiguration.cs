using E_Learning.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.DAL.Data.Configurations
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasKey(c => new { c.UserId, c.CourseId });
            builder.HasOne(c => c.Course).WithMany(c => c.WishLists).HasForeignKey(c => c.CourseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.User).WithMany(c => c.WishListItems).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);

            //Data Seeding
            builder.HasData([
                new WishList { CourseId = 3, UserId = 1 },
              new WishList { CourseId = 4, UserId = 2 }
                ]);

        }
    }
    }