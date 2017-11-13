using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Models
{
    public class Wish
    {
        public int WishID { get; set; }
        [ForeignKey("WishList")]
        public int WishListID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WishCategorie Categorie { get; set; }
        public bool Checked { get; set; }
        public string ImageURL { get; set; }
        public User Buyer { get; set; }
    }
}
