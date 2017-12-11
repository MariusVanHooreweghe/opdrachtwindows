using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;

namespace WishMeAList.ViewModels
{
    public class WishListsViewModel : ViewModelBase
    {
        public WishList wishList { get; set; }
        public ObservableCollection<WishList> Wishes { get; set; }

        public WishListsViewModel(List<Models.WishList> wishLists)
        {
            Wishes = new ObservableCollection<Models.WishList>(wishLists);
        }
    }
}
