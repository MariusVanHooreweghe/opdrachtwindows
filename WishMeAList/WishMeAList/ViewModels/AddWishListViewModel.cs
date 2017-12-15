using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        }

        public void ConfirmWishList()
        {
            WishList = new WishList
            {
                Title = this.Title,
                DateOfEvent = this._dateOfEvent,
                Wishes = new Collection<Wish>(),
                Accessors = SelectedUsers
            };
            WishLists.Add(this.WishList);

            this._parent.WishListsOwning = WishLists;
            this._parent.CurrentData = new WishListsViewModel(this._parent);
        
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
