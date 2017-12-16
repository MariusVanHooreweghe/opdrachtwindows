using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WishMeAListAPItutorial.Models
{
    public class WishListAccessor
    {
        [ForeignKey("WishList")]
        public int WishListID { get; set; }
        public WishList WishList { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
