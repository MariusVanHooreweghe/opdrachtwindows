using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WishMeAListAPItutorial.Data.Mappers;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Data
{
    public class WishListContext : IdentityDbContext
    {
        public WishListContext(DbContextOptions<WishListContext> options)
            : base(options)
        {
        }

        public DbSet<WishList> WishLists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WishListConfiguration());
            modelBuilder.ApplyConfiguration(new WishConfiguration());
            modelBuilder.ApplyConfiguration(new WishListAccessorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
