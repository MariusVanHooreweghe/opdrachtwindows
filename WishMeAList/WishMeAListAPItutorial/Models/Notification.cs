using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishMeAListAPItutorial.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int SenderID { get; set; }
        public User Sender { get; set; }
        public int RecieverID { get; set; }
        public User Reciever { get; set; }
        public NotificationType Type { get; set; }
        public int? WishListID { get; set; }
        public WishList WishList { get; set; }
        public String Message { get; set; }
        public bool HasBeenRead { get; set; }
        public DateTime Date { get; set; }

    }
}
