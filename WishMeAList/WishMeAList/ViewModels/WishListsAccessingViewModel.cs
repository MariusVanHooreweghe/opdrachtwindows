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
        private Collection<WishList> _wishLists { get; set; }
        public ObservableCollection<WishList> WishLists {
            get { return new ObservableCollection<WishList>(_wishLists); }
            set { _wishLists = value; RaisePropertyChanged("WishLists"); }
        }
        private NavigatorViewModel _parent { get; set; }

        public WishListsAccessingViewModel( NavigatorViewModel parent)
        {
            this._parent = parent;
            _wishLists = new ObservableCollection<WishList>(UserManager.CurrentUser.WishListsAccessing);
        }

        public void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            WishList _wishList = (WishList)e.ClickedItem;
            this._parent.CurrentData = new WishListAccessingViewModel(WishLists.Where(val =>
                    val.WishListID == _wishList.WishListID).SingleOrDefault(), this._parent);
        }

    }
}
