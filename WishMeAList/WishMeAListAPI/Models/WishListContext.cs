using Microsoft.EntityFrameworkCore;
using WishMeAList.Models;

namespace WishMeAListAPI.Models
{
    public class WishListContext : DbContext
    {
        public WishListContext(DbContextOptions<WishListContext> options)
            : base(options)
        {
        }

        public DbSet<WishList> WishLists { get; set; }
    }
}
