using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Utils;

namespace WishMeAList.Models
{
    public class Notification : INotifyPropertyChanged
    {
        public int NoitifcationID { get; set; }
        public User Sender { get; set; }
        public User Reciever { get; set; }
        public NotificationType Type { get; set; }
        public WishList WishList { get; set; }
        public String Message { get; set; }
        public bool HasBeenRead { get; set; }
        public DateTime Date { get; set; }

        public Notification(User sender, User reciever, NotificationType type, WishList wishList = null)
        {
            Sender = sender;
            Reciever = reciever;
            Type = type;
            WishList = wishList;
            Date = DateTime.Now;
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
                    Sender.Notifications.Add(new Notification(Reciever, Sender, NotificationType.REQUEST_FOR_ACCESS_CONFIRMATION_MESSAGE, WishList));
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
                    Sender.Notifications.Add(new Notification(Reciever, Sender, NotificationType.FRIEND_CONFIRMATION_MESSAGE));
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
                    Sender.Notifications.Add(new Notification(Reciever, Sender, NotificationType.INVITE_FOR_ACCESS_CONFIRMATION_MESSAGE, WishList));
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
