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
        public RelayCommand AddWishCommand { get; set; }
        public Models.WishList wishList { get; set; }
        public ObservableCollection<Wish> Wishes { get; set; }

        public WishListAccessorViewModel(Models.WishList wishList)
        {
            Wishes = new ObservableCollection<Wish>(wishList.Wishes);
            // TO DO: how to pass multiple params?
            ToggleCheckedWishCommand = new RelayCommand(param) => ToggleCheckedWish(param));
        }

        private void ToggleCheckedWish(object wishID, object isChecked)
        {
            bool isC;
            switch (isChecked)
            {
                case "true": isC = true; break;
                case "false": isC = false; break;
                default: isC = false; break;
            }
            Wish wish = Wishes.FirstOrDefault(w => w.WishID == int.Parse(wishID.ToString()));
            wish.IsChecked = isC;
        }
    }
}
