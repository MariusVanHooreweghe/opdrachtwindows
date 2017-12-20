using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Data.Mappers
{
    internal class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("WishList");
            builder.HasKey(t => t.WishListID);
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.DateOfEvent).IsRequired();
            builder.HasMany(t => t.Wishes).WithOne().IsRequired().HasForeignKey(t => t.WishListID).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Accessors).WithOne(acc => acc.WishList).IsRequired().HasForeignKey(acc => acc.WishListID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
