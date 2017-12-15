﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private Collection<User> _selectedFriends { get; set; }
        public RelayCommand AddAccessorsCommand { get; set; }
        private WishListViewModel _parent { get; set; }
        public String InviteMoreFriendsVisibility { get { return OtherFriends.Count == 0 ? "Collapsed" : "Visible"; } }

        public ObservableCollection<User> Accessors
        {
            get { return new ObservableCollection<User>(_parent.WishList.Accessors.OrderBy(val => val.FirstName).ThenBy(val => val.LastName)); }
        }

        public ObservableCollection<User> OtherFriends
        {
            get { return new ObservableCollection<User>(UserManager.CurrentUser.Friends.Where(user => !Accessors.Contains(user))
                .OrderBy(val => val.FirstName).ThenBy(val => val.LastName)); }
        }

        public AccessorsViewModel(WishListViewModel parent)
        {
            _parent = parent;
             Title =  _parent.WishList.Title + " - Accessors";
             _selectedFriends = new Collection<User>();

            AddAccessorsCommand = new RelayCommand(_ => AddAccessors());

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
                if (!friend.WishListsAccessing.Contains(_parent.WishList))
                {
                    friend.WishListsAccessing.Add(_parent.WishList);
                }
                if (!_parent.WishList.Accessors.Contains(friend))
                {
                    _parent.WishList.Accessors.Add(friend);
                    Accessors.Add(friend);
                    RaisePropertyChanged("Accessors");
                    RaisePropertyChanged("OtherFriends");
                    RaisePropertyChanged("InviteMoreFriendsVisibility");
                }
            }
        }
    }
}