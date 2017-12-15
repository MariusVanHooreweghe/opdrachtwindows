using System;
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
    public class WishListsAccessingViewModel : ViewModelBase
    {
        private WishList _wishList;
        public WishList WishList
        {
            get { return _wishList; }
            set { _wishList = value; RaisePropertyChanged("WishList"); }
        }
        public ObservableCollection<WishList> WishLists { get; set; }
        private NavigatorViewModel _parent { get; set; }

        public WishListsAccessingViewModel(List<WishList> wishLists, NavigatorViewModel parent)
        {
            this._parent = parent;
            WishLists = new ObservableCollection<WishList>(wishLists);
        }

        public void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            _wishList = (WishList)e.ClickedItem;
            this._parent.CurrentData = new WishListAccessingViewModel(WishLists.Where(val =>
                    val.WishListID == _wishList.WishListID).SingleOrDefault(), this._parent);
        }

    }
}
