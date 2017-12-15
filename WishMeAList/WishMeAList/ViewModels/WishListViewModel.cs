using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class WishListViewModel : ViewModelBase
    {
        public WishList WishList { get; set; }
        private ObservableCollection<Wish> _wishes;
        public ObservableCollection<Wish> Wishes {
            get { return _wishes; }
            set { _wishes = value; RaisePropertyChanged("Wishes"); }
        }

        public RelayCommand AddWishCommand { get; set; }
        public RelayCommand OpenAccessorsCommand { get; set; }
        public RelayCommand ToggleCheckedWishCommand { get; set; }

        private NavigatorViewModel _parent { get; set; }


        public WishListViewModel(WishList wishList, NavigatorViewModel parent)
        {
            this.WishList = wishList;
            this._parent = parent;
            this._wishes = new ObservableCollection<Wish>(wishList.Wishes);

            ToggleCheckedWishCommand = new RelayCommand((param) => ToggleCheckedWish(param));
            AddWishCommand = new RelayCommand(_ => AddWish());
            OpenAccessorsCommand = new RelayCommand(_ => OpenAccessors());
        }

        private void AddWish()
        {
            this._parent.CurrentData = new AddWishViewModel(WishList, _parent);
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
