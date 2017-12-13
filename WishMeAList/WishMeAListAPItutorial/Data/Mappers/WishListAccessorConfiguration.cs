using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Data.Mappers
{
    internal class WishListAccessorConfiguration : IEntityTypeConfiguration<WishListAccessor>
    {
        public void Configure(EntityTypeBuilder<WishListAccessor> builder)
        {
            builder.ToTable("TTWishListAccessor");
            builder.HasKey(t => new { t.WishListID, t.UserID});
            builder.HasOne(tt => tt.WishList).WithMany(tt => tt.Accessors).HasForeignKey(tt => tt.WishListID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(tt => tt.User).WithMany(tt => tt.WishListsAccessing).HasForeignKey(tt => tt.UserID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
