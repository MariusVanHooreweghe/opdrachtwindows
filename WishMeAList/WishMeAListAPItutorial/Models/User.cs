using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishMeAListAPItutorial.Models
{
    public class User
    {
        public int UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public virtual Collection<WishList> WishListsOwning { get; set; }
        public virtual Collection<WishListAccessor> WishListsAccessing { get; set; }
        public virtual Collection<Wish> WishesBuying { get; set; }
        public virtual Collection<Notification> Notifications { get; set; }

    }
}
