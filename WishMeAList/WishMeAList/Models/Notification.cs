using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Utils;

namespace WishMeAList.Models
{
    public class Notification : INotifyPropertyChanged
    {
        [JsonProperty]
        public int NotificationID { get; set; }
        [ForeignKey("User")]
        [JsonProperty]
        public int SenderID { get; set; }
        [JsonIgnore]
        public User Sender { get; set; }
        [ForeignKey("User")]
        [JsonProperty]
        public int RecieverID { get; set; }
        [JsonIgnore]
        public User Reciever { get; set; }
        [JsonProperty]
        public NotificationType Type { get; set; }
        [ForeignKey("WishList")]
        [JsonProperty]
        public int WishListID { get; set; }
        [JsonIgnore]
        public WishList WishList { get; set; }
        [JsonProperty]
        public String Message { get; set; }
        [JsonProperty]
        public bool HasBeenRead { get; set; }
        [JsonProperty]
        public DateTime Date { get; set; }

        public Notification(User sender, User reciever, NotificationType type, DateTime date, WishList wishList = null)
        {
            Sender = sender;
            SenderID = sender.UserID;
            Reciever = reciever;
            RecieverID = Reciever == null? 0 : Reciever.UserID;
            Type = type;
            WishList = wishList;
            WishListID = wishList.WishListID;
            Date = date == null ? DateTime.Now : date;
            HasBeenRead = false;
            // Init message
            switch (Type)
            {
                case NotificationType.REQUEST_FOR_ACCESS: Message = $"{Sender.Name} would like to get access to your wishlist '{WishList?.Title}'";  break;
                case NotificationType.REQUEST_FOR_ACCESS_CONFIRMATION_MESSAGE: Message = $"{Sender.Name} accepted your request for access to '{WishList?.Title}'"; break;
                case NotificationType.INVITE_FOR_ACCESS: Message = $"{ Sender.Name} has invited you to access the wishlist '{WishList?.Title}'"; break;
                case NotificationType.INVITE_FOR_ACCESS_CONFIRMATION_MESSAGE: Message = $"{Sender.Name} accepted your request to access your wishlist '{WishList?.Title}'"; break;
                case NotificationType.FRIEND_REQUEST: Message = $"{Sender.Name} has sent you a friendrequest"; break;
                case NotificationType.FRIEND_CONFIRMATION_MESSAGE: Message = $"{Sender.Name} has accepted you as a friend"; break;
                case NotificationType.ACCESS_SUBDUCTED: Message = $"{Sender.Name} has subducted your access to '{WishList.Title}'." +
                        $"Wishes you were planning to buy from that wishlist were removed from your list"; break;
            }
        }

        public void Accept()
        {
            switch (Type)
            {
                case NotificationType.REQUEST_FOR_ACCESS:
                    if (!Sender.WishListsAccessing.Contains(WishList))
                    {
                        Sender.WishListsAccessing.Add(WishList);
                    }
                    if (!WishList.Accessors.Contains(Sender))
                    {
                        WishList.Accessors.Add(Sender);
                    }
                    Sender.Notifications.Add(new Notification(Reciever, Sender, NotificationType.REQUEST_FOR_ACCESS_CONFIRMATION_MESSAGE, DateTime.Now, WishList));
                    Reciever.Notifications.Remove(this);
                    break;
                case NotificationType.FRIEND_REQUEST:
                    if (!Sender.Friends.Contains(Reciever))
                    {
                        Sender.Friends.Add(Reciever);
                    }
                    if(!Reciever.Friends.Contains(Sender))
                    {
                        Reciever.Friends.Add(Sender);
                    }
                    Sender.Notifications.Add(new Notification(Reciever, Sender, NotificationType.FRIEND_CONFIRMATION_MESSAGE, DateTime.Now));
                    Reciever.Notifications.Remove(this);
                    break;
                case NotificationType.INVITE_FOR_ACCESS:
                    if (!Reciever.WishListsAccessing.Contains(WishList))
                    {
                        Reciever.WishListsAccessing.Add(WishList);
                    }
                    if (!WishList.Accessors.Contains(Reciever))
                    {
                        WishList.Accessors.Add(Reciever);
                    }
                    Sender.Notifications.Add(new Notification(Reciever, Sender, NotificationType.INVITE_FOR_ACCESS_CONFIRMATION_MESSAGE, DateTime.Now, WishList));
                    Reciever.Notifications.Remove(this);
                    break;
                default: Reciever.Notifications.Remove(this); break;
            }
        }

        public void Deny()
        {
            Reciever.Notifications.Remove(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
