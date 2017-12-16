using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Data.Mappers
{
    internal class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendship");
            builder.HasKey(f => new { f.BefrienderID, f.BefriendedID});
            builder.HasOne(f => f.Befriender).WithMany().IsRequired().HasForeignKey(f => f.BefrienderID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(f => f.Befriended).WithMany().IsRequired().HasForeignKey(f => f.BefriendedID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
