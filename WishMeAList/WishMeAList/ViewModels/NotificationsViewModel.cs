using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
            get; set; 
        }


        public NotificationsViewModel()
        {
            InitNotifications();
            
        }

        private async Task InitNotifications()
        {
            try
            {
                HttpClient client = new HttpClient();
                    var json = await client.GetStringAsync(new Uri("http://localhost:65172/api/Notifications/user/" + UserManager.CurrentUser.UserID));
                Notifications = JsonConvert.DeserializeObject<ObservableCollection<Notification>>(json);
                foreach (Notification not in Notifications)
                {
                    not.HasBeenRead = true;
                    string notJson = JsonConvert.SerializeObject(not);
                    await client.PutAsync("http://localhost:65172/api/Notifications/"+not.NotificationID, new StringContent(notJson, System.Text.Encoding.UTF8, "application/json"));
                }
                RaisePropertyChanged("Notifications");
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                Notifications = new ObservableCollection<Notification>();
            }
            
        }

        public void AcceptNotification(Notification notification)
        {
            notification.Accept();
            Notifications.Remove(notification);
            RaisePropertyChanged("Notifications");
        }

        public void DenyNotification(Notification notification)
        {
            notification.Deny();
            Deny(notification);
            Notifications.Remove(notification);

            RaisePropertyChanged("Notifications");
        }

  
        private async Task Deny(Notification notification)
        {
            HttpClient client = new HttpClient();
            var res = await client.DeleteAsync("http://localhost:65172/api/Notifications/" + notification.NotificationID);
            RaisePropertyChanged("Notifications");

        }
    }
}
