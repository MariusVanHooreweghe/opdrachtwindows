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
    public class FriendViewModel : ViewModelBase
    {
        public User Friend { get; set; }
        public WishList SelectedWishList { get; set; }
        public RelayCommand RequestAccessCommand { get; set; }

        private ObservableCollection<WishList> _wishLists;
        public ObservableCollection<WishList> WishLists {
            get { return _wishLists; }
            set { _wishLists = value; RaisePropertyChanged("WishLists"); }
        }

        public FriendViewModel(User friend)
        {
            Friend = friend;
            GetUser();
        
            RequestAccessCommand = new RelayCommand(_ => RequestAccess());
        }

        private void RequestAccess()
        {
            NotifyFriend();
        }

        private async Task GetUser()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:65172/api/WishLists/user/" + Friend.UserID));
            ObservableCollection<WishList> WishLists = JsonConvert.DeserializeObject<ObservableCollection<WishList>>(json);

            Friend.WishListsOwning = WishLists;
            this.WishLists = new ObservableCollection<WishList>(Friend.WishListsOwning);
            
            RaisePropertyChanged("WishLists");
        }

        private async Task NotifyFriend()
        {
            HttpClient client = new HttpClient();

            Notification notification = new Notification(UserManager.CurrentUser, Friend, NotificationType.REQUEST_FOR_ACCESS, DateTime.Now, SelectedWishList);
            try
            {
                var notificationJson = JsonConvert.SerializeObject(notification,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    });
                Debug.WriteLine(notificationJson);
                var resNotification =
             await client.PostAsync("http://localhost:65172/api/Notifications/", new StringContent(notificationJson, System.Text.Encoding.UTF8, "application/json"));
                Debug.WriteLine(resNotification);
                if (resNotification.Content != null)
                {
                    string newNotificationJson = await resNotification.Content.ReadAsStringAsync();
                    if (Friend.Notifications == null)
                    {
                        Friend.Notifications = new Collection<Notification>();
                    }
                    var addedNotification = JsonConvert.DeserializeObject<Notification>(newNotificationJson);
                    addedNotification.Sender = UserManager.CurrentUser;
                    addedNotification.Reciever = Friend;
                    Friend.Notifications.Add(addedNotification);
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

        }
    }
}
