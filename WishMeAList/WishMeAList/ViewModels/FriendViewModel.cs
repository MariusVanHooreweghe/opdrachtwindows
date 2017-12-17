using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class FriendViewModel : ViewModelBase
    {
        public User Friend { get; set; }
        public WishList SelectedWishList { get; set; }
        public RelayCommand RequestAccessCommand { get; set; }

        public FriendViewModel(User friend)
        {
            Friend = friend;
            RequestAccessCommand = new RelayCommand(_ => RequestAccess());
        }

        private void RequestAccess()
        {
            Friend.Notifications.Add(new Notification(UserManager.CurrentUser, Friend, NotificationType.REQUEST_FOR_ACCESS, DateTime.Now, SelectedWishList));
        }
    }
}
