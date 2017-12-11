using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishMeAList.Models
{
    public class User
    {
        public int UserID { get; set; }
        [NotMapped]
        public virtual Collection<WishList> WishListsOwning { get; set; }

        [NotMapped]
        public virtual Collection<WishList> WishListsAccessing { get; set; }
        public virtual Collection<Wish> WishesBuying { get; set; }
    }
}
