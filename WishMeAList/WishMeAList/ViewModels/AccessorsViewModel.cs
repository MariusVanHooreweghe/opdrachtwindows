using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class AccessorsViewModel : ViewModelBase
    {
        public String Title { get; set; }
        public User SelectedAccessor { get; set; }
        private Collection<User> _selectedFriends { get; set; }
        public RelayCommand AddAccessorsCommand { get; set; }
        public RelayCommand SubductAccessCommand { get; set; }
        private WishListViewModel _parent { get; set; }

        public String FriendsVisibility { get { return (Accessors == null || Accessors.Count == 0) ? "Collapsed" : "Visible"; } }
        public String InviteMoreFriendsVisibility { get { return (OtherFriends == null || OtherFriends.Count == 0) ? "Collapsed" : "Visible"; } }

        public ObservableCollection<User> Accessors
        {
            get; set;
        }

        public ObservableCollection<User> OtherFriends
        {
            get;set;
            //get { return new ObservableCollection<User>(UserManager.CurrentUser.Friends
            //    .Where(user => !Accessors.Contains(user)
            //        && user.Notifications.SingleOrDefault(not => not.WishList == _parent.WishList && not.Type == NotificationType.INVITE_FOR_ACCESS) == null)
            //    .OrderBy(val => val.FirstName).ThenBy(val => val.LastName)); }
        }

        public AccessorsViewModel(WishListViewModel parent)
        {
            _parent = parent;
            initAccessors();
            initOtherFriends();
             Title =  _parent.WishList.Title + " - Accessors";
             _selectedFriends = new Collection<User>();
            AddAccessorsCommand = new RelayCommand(_ => AddAccessors());
            SubductAccessCommand = new RelayCommand(_ => SubductAccess());

        }



        private async Task initAccessors()
        {
            var accessorsJson = "{}";
            Accessors = new ObservableCollection<User>();
            try
            {
                HttpClient client = new HttpClient();
                accessorsJson = await client.GetStringAsync(new Uri("http://localhost:65172/api/wishlists/accessors/" + _parent.WishList.WishListID));
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            Accessors = JsonConvert.DeserializeObject<ObservableCollection<User>>(accessorsJson);
            RaisePropertyChanged("FriendsVisibility");
            RaisePropertyChanged("Accessors");
        }
        private async Task initOtherFriends()
        {
            var accessorsJson = "{}";
            OtherFriends = new ObservableCollection<User>();
            try
            {
                HttpClient client = new HttpClient();
                accessorsJson = await client.GetStringAsync(new Uri("http://localhost:65172/api/users/friendships/"+UserManager.CurrentUser.UserID+"/wishlist/" + _parent.WishList.WishListID));
                OtherFriends = JsonConvert.DeserializeObject<ObservableCollection<User>>(accessorsJson);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            RaisePropertyChanged("OtherFriends");
            RaisePropertyChanged("InviteMoreFriendsVisibility");
        }
        public void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            User user = (User) e.ClickedItem;
            if ( _selectedFriends.Contains(user))
            {
                 _selectedFriends.Remove(user);
            }
            else
            {
                 _selectedFriends.Add(user);
            }
        }

        private void AddAccessors()
        {
            Notification notification;
            foreach (User friend in _selectedFriends)
            {
                notification = new Notification(friend, UserManager.CurrentUser, NotificationType.INVITE_FOR_ACCESS_CONFIRMATION_MESSAGE, DateTime.Now, _parent.WishList);
                if (friend.Notifications == null) friend.Notifications = new ObservableCollection<Notification>();
                friend.Notifications.Add(notification);
                GrantAccess(notification);
                CreateNotification(notification);
            }
            initAccessors();
            initOtherFriends();
            RaisePropertyChanged("FriendsVisibility");
            RaisePropertyChanged("InviteMoreFriendsVisibility");
        }

        private async void CreateNotification(Notification notification)
        {
            try
            {
                HttpClient client = new HttpClient();
                var notificationJson = JsonConvert.SerializeObject(notification,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    });
                 var resNotification =
                 await client.PostAsync("http://localhost:65172/api/Notifications/", new StringContent(notificationJson, System.Text.Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
        }
        private async void GrantAccess(Notification notification)
        {
            string wishListJson = "{}";
            Debug.Write(wishListJson);
            HttpClient client = new HttpClient();
            var res = await client.PostAsync("http://localhost:65172/api/WishLists/accessor/" + notification.SenderID + "/create/" + notification.WishListID, new StringContent(wishListJson, System.Text.Encoding.UTF8, "application/json"));

            RaisePropertyChanged("FriendsVisibility");
            RaisePropertyChanged("Accessors");
            RaisePropertyChanged("OtherFriends");
            RaisePropertyChanged("InviteMoreFriendsVisibility");
        }
        private async Task SubductAccess()
        {
            List<Wish> wishesBuying = _parent.WishList.Wishes.Where(wish => wish.Buyer == SelectedAccessor).ToList();
            //SelectedAccessor.WishListsAccessing.Remove(_parent.WishList);
            if (_parent.WishList.Accessors != null)_parent.WishList.Accessors.Remove(SelectedAccessor);
            //Accessors.Remove(SelectedAccessor);
            //OtherFriends.Add(SelectedAccessor);
            //foreach (Wish wish in wishesBuying)
            //{
            //    SelectedAccessor.WishesBuying.Remove(wish);
            //    wish.IsChecked = false;
            //}
            //SelectedAccessor.Notifications.Add(new Notification(UserManager.CurrentUser, SelectedAccessor, NotificationType.ACCESS_SUBDUCTED, _parent.WishList));
            HttpClient client = new HttpClient();
            var res = await client.DeleteAsync("http://localhost:65172/api/wishlists/" + _parent.WishList.WishListID + "/accessors/" + SelectedAccessor.UserID);
            //if (SelectedAccessor.Notifications == null)
            //    SelectedAccessor.Notifications = new Collection<Notification>();
            Notification notification = new Notification(UserManager.CurrentUser, SelectedAccessor, NotificationType.ACCESS_SUBDUCTED, DateTime.Now,_parent.WishList);
            try {
                var notificationJson = JsonConvert.SerializeObject(notification,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                    });
                Debug.WriteLine(notificationJson);
                //var resNotification = 
                    await client.PostAsync("http://localhost:65172/api/notifications/", new StringContent(notificationJson, System.Text.Encoding.UTF8, "application/json"));
                //if (resNotification.Content != null)
                //{
                //    string newNotificationJson = await resNotification.Content.ReadAsStringAsync();
                //    SelectedAccessor.Notifications.Add(JsonConvert.DeserializeObject<Notification>(newNotificationJson));
                //}
            } catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            initAccessors();
            initOtherFriends();
            RaisePropertyChanged("FriendsVisibility");
            RaisePropertyChanged("Accessors");
            RaisePropertyChanged("OtherFriends");
            RaisePropertyChanged("InviteMoreFriendsVisibility");
        }
    }
}
