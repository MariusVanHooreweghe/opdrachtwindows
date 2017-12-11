using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishList.Models;
using WishList.Utils;

namespace WishList.ViewModels
{
    public class WishListAccessorViewModel
    {
        public RelayCommand ToggleCheckedWishCommand { get; set; }
        public Models.WishList wishList { get; set; }
        public ObservableCollection<Wish> Wishes { get; set; }

        public WishListAccessorViewModel(Models.WishList wishList)
        {
            Wishes = new ObservableCollection<Wish>(wishList.Wishes);
            ToggleCheckedWishCommand = new RelayCommand((param) => ToggleCheckedWish(param));
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
    }
}
