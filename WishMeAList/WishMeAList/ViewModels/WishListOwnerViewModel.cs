using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;

namespace WishMeAList.ViewModels
{
    public class WishListOwnerViewModel
    {
        public WishList wishList { get; set; }
        public ObservableCollection<Wish> Wishes { get; set; }

        public WishListOwnerViewModel(WishList wishList)
        {
            Wishes = new ObservableCollection<Wish>(wishList.Wishes);
        }

        private void AddWish(object title, object description, object categorie, object imageUrl)
        {

        }
    }
}
