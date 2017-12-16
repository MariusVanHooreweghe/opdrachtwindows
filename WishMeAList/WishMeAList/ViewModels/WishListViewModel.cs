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
    public class WishListViewModel : ViewModelBase
    {
        public WishList WishList { get; set; }

        private Collection<Wish> _wishes;
        public ObservableCollection<Wish> Wishes
        {
            get { return new ObservableCollection<Wish>(_wishes); }
            set { _wishes = value; RaisePropertyChanged("Wishes"); }
        }
        private Wish _wishToDelete { get; set; }
        public Wish WishToDelete {
            get { return _wishToDelete; }
            set { _wishToDelete = value; RaisePropertyChanged("WishToDelete"); }
        }

        public RelayCommand AddWishCommand { get; set; }
        public RelayCommand DeleteWishCommand { get; set; }
        public RelayCommand OpenAccessorsCommand { get; set; }
        public RelayCommand ToggleCheckedWishCommand { get; set; }

        private NavigatorViewModel _parent { get; set; }
        public String ViewWishesVisibility { get { return Wishes.Count == 0 ? "Collapsed" : "Visible"; } }



        public WishListViewModel(WishList wishList, NavigatorViewModel parent)
        {
            this.WishList = wishList;
            this._parent = parent;
            this._wishes = new ObservableCollection<Wish>(wishList.Wishes);

            ToggleCheckedWishCommand = new RelayCommand((param) => ToggleCheckedWish(param));
            AddWishCommand = new RelayCommand(_ => AddWish());
            DeleteWishCommand = new RelayCommand(_ => DeleteWish());
            OpenAccessorsCommand = new RelayCommand(_ => OpenAccessors());
        }

        private void AddWish()
        {
            this._parent.CurrentData = new AddWishViewModel(WishList, _parent);
        }

        private void DeleteWish()
        {
            if(WishToDelete == null)
            {
                DisplayDialog();
                return;
            }
            _wishes.Remove(WishToDelete);
            UserManager.CurrentUser.WishListsOwning.Where(val => val.WishListID == WishList.WishListID).FirstOrDefault().Wishes = _wishes;
            RaisePropertyChanged("Wishes");
            RaisePropertyChanged("ViewWishesVisibility");
        }

        private async void DisplayDialog()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "No Wish selected",
                Content = "Please select a Wish and try again.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await dialog.ShowAsync();
        }

        private void ToggleCheckedWish(object wishID)
        {
            Wish wish = Wishes.FirstOrDefault(w => w.WishID == int.Parse(wishID.ToString()));
            bool isChecking = !wish.IsChecked;
            wish.IsChecked = isChecking;
            if (isChecking)
            {
                // wish.Buyer = currentUser
                // currentUser.WishedBuying.Add(wish);
            }
            else
            {
                // wish.Buyer = null;
                // currentUser.WishedBuying.Remove(w => w.wishID = wish.wishID);
            }
        }

        private void OpenAccessors()
        {
            this._parent.CurrentData = new AccessorsViewModel(this);
        }
    }

}
