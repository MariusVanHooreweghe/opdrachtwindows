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
            builder.Property(t => t.Username).IsRequired();
            builder.Property(t => t.Password).IsRequired();
            builder.Property(t => t.FirstName).IsRequired();
            builder.Property(t => t.LastName).IsRequired();
            builder.HasMany(t => t.WishesBuying).WithOne().IsRequired(false).HasForeignKey(t => t.BuyerID).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.WishListsOwning).WithOne().IsRequired().HasForeignKey(t => t.OwnerID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.Notifications).WithOne().IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.WishListsAccessing).WithOne(t => t.User).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
