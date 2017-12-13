﻿using System;
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
        public RelayCommand ToggleCheckedWishCommand { get; set; }
        public WishList wishList { get; set; }
        public ObservableCollection<Wish> Wishes { get; set; }

        public WishListViewModel(WishList wishList)
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