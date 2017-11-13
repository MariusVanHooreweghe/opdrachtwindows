using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Models
{
    public class WishList
    {
        public int WishListID { get; set; }
        [ForeignKey("User")]
        public int OwnerID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public virtual Collection<Wish> Wishes { get; set; }
        public virtual Collection<User> Accessors { get; set; }
    }
}
