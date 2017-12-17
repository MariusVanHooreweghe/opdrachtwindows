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

        public String FriendsVisibility { get { return Accessors.Count == 0 ? "Collapsed" : "Visible"; } }
        public String InviteMoreFriendsVisibility { get { return OtherFriends.Count == 0 ? "Collapsed" : "Visible"; } }

        public ObservableCollection<User> Accessors
        {
            get {
                if (_parent.WishList.Accessors == null)
                {
                    return new ObservableCollection<User>();
                }
                return new ObservableCollection<User>(_parent.WishList.Accessors?.OrderBy(val => val.FirstName).ThenBy(val => val.LastName)); 
            }
        }

        public ObservableCollection<User> OtherFriends
        {
            get { return new ObservableCollection<User>(UserManager.CurrentUser.Friends
                .Where(user => !Accessors.Contains(user)
                    && user.Notifications.SingleOrDefault(not => not.WishList == _parent.WishList && not.Type == NotificationType.INVITE_FOR_ACCESS) == null)
                .OrderBy(val => val.FirstName).ThenBy(val => val.LastName)); }
        }

        public AccessorsViewModel(WishListViewModel parent)
        {
            _parent = parent;
            initAccessors();
             Title =  _parent.WishList.Title + " - Accessors";
             _selectedFriends = new Collection<User>();
            AddAccessorsCommand = new RelayCommand(_ => AddAccessors());
            SubductAccessCommand = new RelayCommand(_ => SubductAccess());

        }

        private async Task initAccessors()
        {
            var accessorsJson = "{}";
            try
            {
                HttpClient client = new HttpClient();
                accessorsJson = await client.GetStringAsync(new Uri("http://localhost:65172/api/wishlists/accessors/" + _parent.WishList.WishListID));
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            _parent.WishList.Accessors = JsonConvert.DeserializeObject<ObservableCollection<User>>(accessorsJson);
            RaisePropertyChanged("FriendsVisibility");
            RaisePropertyChanged("Accessors");
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
            foreach (User friend in _selectedFriends)
            {
                friend.Notifications.Add(new Notification(UserManager.CurrentUser, friend, NotificationType.INVITE_FOR_ACCESS, _parent.WishList));
            }
            RaisePropertyChanged("FriendsVisibility");
            RaisePropertyChanged("Accessors");
            RaisePropertyChanged("OtherFriends");
            RaisePropertyChanged("InviteMoreFriendsVisibility");
        }

        private async Task SubductAccess()
        {
            //List<Wish> wishesBuying = _parent.WishList.Wishes.Where(wish => wish.Buyer == SelectedAccessor).ToList();
            //SelectedAccessor.WishListsAccessing.Remove(_parent.WishList);
            //_parent.WishList.Accessors.Remove(SelectedAccessor);
            //foreach (Wish wish in wishesBuying)
            //{
            //    SelectedAccessor.WishesBuying.Remove(wish);
            //    wish.IsChecked = false;
            //}
            //SelectedAccessor.Notifications.Add(new Notification(UserManager.CurrentUser, SelectedAccessor, NotificationType.ACCESS_SUBDUCTED, _parent.WishList));
            HttpClient client = new HttpClient();
            var res = await client.DeleteAsync("http://localhost:65172/api/wishlists/" + _parent.WishList.WishListID + "/accessors/" + SelectedAccessor.UserID);
            if (SelectedAccessor.Notifications == null)
                SelectedAccessor.Notifications = new Collection<Notification>();
            Notification notification = new Notification(UserManager.CurrentUser, SelectedAccessor, NotificationType.ACCESS_SUBDUCTED, _parent.WishList);
            try {
                var notificationJson = JsonConvert.SerializeObject(notification,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                    });
                Debug.WriteLine(notificationJson);
                var resNotification = await client.PostAsync("http://localhost:65172/api/notifications/", new StringContent(notificationJson, System.Text.Encoding.UTF8, "application/json"));
                if (resNotification.Content != null)
                {
                    string newNotificationJson = await resNotification.Content.ReadAsStringAsync();
                    SelectedAccessor.Notifications.Add(JsonConvert.DeserializeObject<Notification>(newNotificationJson));
                }
            } catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            
            RaisePropertyChanged("FriendsVisibility");
            RaisePropertyChanged("Accessors");
            RaisePropertyChanged("OtherFriends");
            RaisePropertyChanged("InviteMoreFriendsVisibility");
        }
    }
}
