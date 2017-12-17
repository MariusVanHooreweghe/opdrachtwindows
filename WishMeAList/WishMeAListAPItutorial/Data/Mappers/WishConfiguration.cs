using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Data.Mappers
{
    internal class WishConfiguration : IEntityTypeConfiguration<Wish>
    {
        public void Configure(EntityTypeBuilder<Wish> builder)
        {
            builder.ToTable("Wish");
            builder.HasKey(t => t.WishID);
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.ImageURL).IsRequired(false);
            builder.Property(t => t.IsChecked);
            builder.Property(t => t.Categorie).IsRequired();
            builder.HasOne(t => t.Buyer).WithMany().IsRequired(false).HasForeignKey(t => t.BuyerID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
