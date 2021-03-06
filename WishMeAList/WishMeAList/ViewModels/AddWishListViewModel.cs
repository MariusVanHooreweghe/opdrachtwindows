﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class AddWishListViewModel : ViewModelBase
    {
        public ObservableCollection<WishList> WishLists { get; set; }
        public WishList WishList { get; set; }
        public User CurrentUser { get { return UserManager.CurrentUser; } }
        public Collection<User> SelectedUsers { get; set; }
        public Collection<User> Friends { get; set; }
        public DateTime _dateOfEvent { get; set; }
        public DateTimeOffset DateOfEvent {
            get { return DateTime.SpecifyKind(_dateOfEvent, DateTimeKind.Utc); }
            set { _dateOfEvent = value.DateTime; RaisePropertyChanged("DateOfEvent"); }
        }

        private String _title;
        public String Title {
            get { return _title; }
            set { _title = value; RaisePropertyChanged("Title"); }
        }

        public RelayCommand ConfirmWishListCommand { get; set; }
        private NavigatorViewModel _parent { get; set; }


        public AddWishListViewModel(ObservableCollection<WishList> WishLists, NavigatorViewModel parent)
        {
            this._parent = parent;
            this.WishLists = WishLists;
            _dateOfEvent = DateTime.Today;
            ConfirmWishListCommand = new RelayCommand(_ => ConfirmWishList());
            SelectedUsers = new Collection<User>();
            InitFriends();
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

        public void ConfirmWishList()
        {
            WishList = new WishList
            {
                Title = this.Title,
                DateOfEvent = this._dateOfEvent,
                Wishes = new Collection<Wish>(),
                Accessors = SelectedUsers,
                OwnerID = UserManager.CurrentUser.UserID
            };
            UserManager.CurrentUser.WishListsOwning = WishLists;
            PostWishList();   
        }
        private async void PostWishList()
        {
            if (WishList.Title== null || WishList.Title.Trim().Equals(""))
            {
          
                DisplayDialog("Invalid Wishlist name", "Please enter a Wishlist name");
                return;
                
            } else if (WishList.DateOfEvent < DateTime.Now)
            {
                DisplayDialog("Invalid Date", "Please enter a Date in the future");
                return;
            }
            string wishListJson = JsonConvert.SerializeObject(this.WishList);
            HttpClient client = new HttpClient();
            var res = await client.PostAsync("http://localhost:65172/api/wishlists/", new StringContent(wishListJson, System.Text.Encoding.UTF8, "application/json"));
            Debug.Write(res);
            if (res.Content != null)
            {
                string newWishListJson = await res.Content.ReadAsStringAsync();
                WishLists.Add(JsonConvert.DeserializeObject<WishList>(newWishListJson));
            }
            this._parent.CurrentData = new WishListsViewModel(this._parent);
        }

        private async void DisplayDialog(String title, String content)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await dialog.ShowAsync();
        }
        public void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            User user = (User)e.ClickedItem;
            if (SelectedUsers.Contains(user))
            {
                SelectedUsers.Remove(user);
            }
            else
            {
                SelectedUsers.Add(user);
            }
        }
    }
}
