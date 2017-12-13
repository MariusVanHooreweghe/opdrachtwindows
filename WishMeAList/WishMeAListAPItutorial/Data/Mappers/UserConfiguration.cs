using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Data.Mappers
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.UserID);
            builder.HasMany(t => t.WishesBuying).WithOne().IsRequired().HasForeignKey(t => t.BuyerID).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.WishListsOwning).WithOne().IsRequired().HasForeignKey(t => t.OwnerID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
