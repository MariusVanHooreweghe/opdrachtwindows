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
    public class WishListOwnerViewModel
    {
        public Models.WishList wishList { get; set; }
        public ObservableCollection<Wish> Wishes { get; set; }

        public WishListOwnerViewModel(Models.WishList wishList)
        {
            Wishes = new ObservableCollection<Wish>(wishList.Wishes);
        }

        private void AddWish(object title, object description, object categorie, object imageUrl)
        {
           
        }
    }
}
