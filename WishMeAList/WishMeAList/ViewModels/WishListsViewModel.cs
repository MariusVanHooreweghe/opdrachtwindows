using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class WishListsViewModel : ViewModelBase
    {
        public WishList wishList { get; set; }
        public ObservableCollection<WishList> WishLists { get; set; }
        public RelayCommand AddWishListCommand { get; set; }
        public RelayCommand OpenWishListCommand { get; set; }
        private NavigatorViewModel _parent { get; set; }



        public WishListsViewModel(List<WishList> wishLists, NavigatorViewModel parent)
        {
            this._parent = parent;
            WishLists = new ObservableCollection<WishList>(wishLists);
            AddWishListCommand = new RelayCommand(_ => ShowAddWishList());
            OpenWishListCommand = new RelayCommand((param) => OpenWishList(param));
        }

        private void ShowAddWishList()
        {
           this._parent.CurrentData = new AddWishListViewModel(WishLists, this._parent);
        }

        private void OpenWishList(object wishListID)
        {
            Debug.WriteLine(wishListID.ToString());
        }
    }
}
