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
    public class FriendsViewModel : ViewModelBase
    {
        public ObservableCollection<User> Friends { get; set; }
        private NavigatorViewModel _parent { get; set; }
        public User SelectedFriend { get; set; }
        public RelayCommand ShowFriendCommand { get; set; }

        public FriendsViewModel(NavigatorViewModel parent)
        {
            this._parent = parent;
            InitFriends();
            ShowFriendCommand = new RelayCommand(_ => ShowFriend());
        }

        private async Task InitFriends()
        {
            try
            {
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(new Uri("http://localhost:65172/api/users/friendships/" + UserManager.CurrentUser.UserID));
                Friends = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                Friends = new ObservableCollection<User>();
            }
            RaisePropertyChanged("Friends");
        }

        private void ShowFriend()
        {
            if (SelectedFriend == null)
            {
                DisplayDialog();
            }
            else
            {
                _parent.CurrentData = new FriendViewModel(SelectedFriend);
            }
           
        }

        private async void DisplayDialog()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "No Friend selected",
                Content = "Please select a Friend and try again.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
