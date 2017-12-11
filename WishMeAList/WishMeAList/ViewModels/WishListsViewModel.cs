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
    public class WishListsViewModel : ViewModelBase
    {
        public WishList wishList { get; set; }
        public ObservableCollection<WishList> WishLists { get; set; }
        public RelayCommand AddWishListCommand { get; set; }

        public WishListsViewModel(List<WishList> wishLists)
        {
            WishLists = new ObservableCollection<WishList>(wishLists);
            AddWishListCommand = new RelayCommand(_ => ShowAddWishList());
        }

        private void ShowAddWishList()
        {

        }
    }
}
