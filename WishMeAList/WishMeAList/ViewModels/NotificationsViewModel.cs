using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        public ObservableCollection<Notification> Notifications
        {
            get { return new ObservableCollection<Notification>(UserManager.CurrentUser.Notifications.OrderBy(not => not.Date)); }
        }

        public NotificationsViewModel()
        {
            foreach (Notification not in Notifications)
            {
                not.HasBeenRead = true;
            }
        }


        public void AcceptNotification(Notification notification)
        {
            notification.Accept();
            RaisePropertyChanged("Notifications");
        }

        public void DenyNotification(Notification notification)
        {
            notification.Deny();
            RaisePropertyChanged("Notifications");
        }
    }
}
