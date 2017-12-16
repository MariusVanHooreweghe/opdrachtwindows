using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Data.Mappers
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification");
            builder.HasKey(t => t.NotificationID);
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.HasBeenRead).IsRequired();
            builder.Property(t => t.Message).IsRequired();
            builder.Property(t => t.Type).IsRequired();
            builder.HasOne(t => t.WishList).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Reciever).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Sender).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
