using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class WishListsViewModel : ViewModelBase
    {
        public WishList _wishList;
        public WishList WishList {
            get {return _wishList; }
            set {_wishList = value; RaisePropertyChanged("WishList"); }
        }
        public ObservableCollection<WishList> WishLists { get; set; }
        public RelayCommand AddWishListCommand { get; set; }
        public RelayCommand OpenWishListCommand { get; set; }
        private NavigatorViewModel _parent { get; set; }


        public WishListsViewModel(List<WishList> wishLists, NavigatorViewModel parent)
        {
            this._parent = parent;
            WishLists = new ObservableCollection<WishList>(wishLists);
            AddWishListCommand = new RelayCommand(_ => ShowAddWishList());
        }

        private void ShowAddWishList()
        {
           this._parent.CurrentData = new AddWishListViewModel(WishLists, this._parent);
        }
  

        public void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            _wishList = (WishList) e.ClickedItem;
            this._parent.CurrentData = new WishListViewModel(WishLists.Where(val => 
                    val.WishListID == _wishList.WishListID).SingleOrDefault(), this._parent);
        }

    }
}
