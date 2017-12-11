using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishMeAList.ViewModels
{
    public class WishListsViewModel
    {
        public Models.WishList wishList { get; set; }
        public ObservableCollection<Models.WishList> Wishes { get; set; }

        public WishListsViewModel(List<Models.WishList> wishLists)
        {
            Wishes = new ObservableCollection<Models.WishList>(wishLists);
        }
    }
}
