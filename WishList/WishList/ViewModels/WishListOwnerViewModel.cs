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
        public RelayCommand AddWishCommand { get; set; }
        public Models.WishList wishList { get; set; }
        public ObservableCollection<Wish> Wishes { get; set; }

        public WishListOwnerViewModel(Models.WishList wishList)
        {
            Wishes = new ObservableCollection<Wish>(wishList.Wishes);
            // TO DO: how to pass multiple params?
            AddWishCommand = new RelayCommand(param) => AddWish(param));
        }

        private void AddWish(object title, object description, object categorie, object isChecked, object imageUrl)
        {
            WishCategorie cat;
            bool isC;
            switch (categorie.ToString().ToLower())
            {
                case "MUZIEK_EN_FILMS": cat = WishCategorie.MUZIEK_EN_FILMS; break;
                case "KLEDIJ": cat = WishCategorie.KLEDIJ; break;
                case "BABY": cat = WishCategorie.BABY; break;
                case "SPELLETJES": cat = WishCategorie.SPELLETJES; break;
                case "SPORT": cat = WishCategorie.SPORT; break;
                case "DRANK_EN_VOEDING": cat = WishCategorie.DRANK_EN_VOEDING; break;
                case "KEUKEN": cat = WishCategorie.KEUKEN; break;
                default: cat = WishCategorie.ANDERE; break;

            }
            switch (isChecked)
            {
                case "true": isC = true; break;
                case "false": isC = false; break;
                default: isC = false; break;
            }
            // TO DO: init WishID and Buyer
            Wishes.Add(new Wish() { Title = title.ToString(), Description = description.ToString(), Categorie = cat,
                IsChecked = isC, ImageURL = imageUrl.ToString(), WishListID =  wishList.WishListID});
        }
    }
}
