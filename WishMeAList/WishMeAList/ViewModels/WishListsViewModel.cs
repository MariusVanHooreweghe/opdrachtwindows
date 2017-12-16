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
        private Collection<WishList> _wishLists { get; set; }
        private WishList _wishList;
        public WishList WishList {
            get { return _wishList; }
            set { _wishList = value; RaisePropertyChanged("WishList"); }
        }
        public ObservableCollection<WishList> WishLists
        {
            get { return new ObservableCollection<WishList>(_wishLists); }
            set { _wishLists = value; RaisePropertyChanged("WishLists"); }
        }
        public RelayCommand AddWishListCommand { get; set; }
        public RelayCommand DeleteWishListCommand { get; set; }
        public RelayCommand ViewWishesCommand { get; set; }
        public String ViewWishesVisibility { get { return WishLists.Count == 0 ? "Collapsed" : "Visible"; } }


        private NavigatorViewModel _parent { get; set; }


        public WishListsViewModel( NavigatorViewModel parent)
        {
            this._parent = parent;
            WishLists = new ObservableCollection<WishList>(UserManager.CurrentUser.WishListsOwning);
          
            AddWishListCommand = new RelayCommand(_ => ShowAddWishList());
            DeleteWishListCommand = new RelayCommand(_ => DeleteWishList());
            ViewWishesCommand = new RelayCommand(_ => ViewWishes());
        }

        private void ShowAddWishList()
        {
           this._parent.CurrentData = new AddWishListViewModel(WishLists, this._parent);
        }
        private void DeleteWishList()
        {
            if( WishList == null)
            {
                DisplayDialog();
                return;
            }
            _wishLists.Remove(WishList);
            UserManager.CurrentUser.WishListsOwning = _wishLists;
            RaisePropertyChanged("WishLists"); 
            RaisePropertyChanged("ViewWishesVisibility");
        }
  

        public void ViewWishes()
        {
            if(WishList == null)
            {
                DisplayDialog();
                return;
            }
            this._parent.CurrentData = new WishListViewModel(WishLists.Where(val => 
                    val.WishListID == WishList.WishListID).SingleOrDefault(), this._parent);
        }

        private async void DisplayDialog()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "No Wishlist selected",
                Content = "Please select a Wishlist and try again.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await dialog.ShowAsync();
        }

    }
}
