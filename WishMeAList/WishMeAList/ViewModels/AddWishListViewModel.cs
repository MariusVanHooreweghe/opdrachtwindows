using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class AddWishListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ObservableCollection<WishList> WishLists { get; set; }
        public WishList WishList { get; set; }
        public String _title;

        public DateTime _dateOfEvent { get; set; }
        public DateTimeOffset DateOfEvent {
            get { return DateTime.SpecifyKind(_dateOfEvent, DateTimeKind.Utc); }
            set { _dateOfEvent = value.DateTime; RaisePropertyChanged("DateOfEvent"); }
        }

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
            ConfirmWishListCommand = new RelayCommand(_ => ConfirmWishList());
        }

        public void ConfirmWishList()
        {
            WishList = new WishList
            {
                Title = this.Title,
                DateOfEvent = this._dateOfEvent
            };
            WishLists.Add(this.WishList);

            this._parent.CurrentData = new WishListsViewModel(WishLists.ToList(), this._parent);
        
        }

 

    }
}
