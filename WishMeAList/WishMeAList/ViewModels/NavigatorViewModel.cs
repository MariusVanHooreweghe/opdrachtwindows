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
    public class NavigatorViewModel : ViewModelBase
    {
        private ViewModelBase _currentData;
        public ViewModelBase CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; RaisePropertyChanged("CurrentData"); }
        }
        public RelayCommand ShowWishlistsOwningCommand { get; set; }
        public RelayCommand ShowWishlistsAccessingCommand { get; set; }
        public RelayCommand ShowWishesBuyingCommand { get; set; }

        public NavigatorViewModel()
        {
            // Dummy data
            initData();
            // ----------
            ShowWishlistsOwningCommand = new RelayCommand(_ => ShowWishlistsOwning());
            ShowWishlistsAccessingCommand = new RelayCommand(_ => ShowWishlistsAccessing());
            ShowWishesBuyingCommand = new RelayCommand(_ => ShowWishesBuying());
            ShowWishlistsOwning();
        }

        private void ShowWishlistsOwning()
        {
            CurrentData = new WishListsViewModel(WishListsOwning);
        }

        private void ShowWishlistsAccessing()
        {
            CurrentData = new WishListsViewModel(WishListsAccessing);
        }

        private void ShowWishesBuying()
        {
            CurrentData = new WishesBuyingViewModel();
        }

        // Dummy data
        public List<WishList> WishListsOwning { get; set; }
        public List<WishList> WishListsAccessing { get; set; }

        public void initData()
        {
            User ThisUser = new User {
                UserID = 2
            };
            User OtherUser = new User {
                UserID = 1
            };

            Collection<User> ThisUserInACollection = new Collection<User>();
            ThisUserInACollection.Add(ThisUser);

            Wish wish1 = new Wish
            {
                Buyer = OtherUser,
                Categorie = WishCategorie.KEUKEN,
                Description = "Het liefst een gele.",
                IsChecked = false,
                Title = "Mixer",
                WishID = 1,
                WishListID = 1
            };

            Wish wish2 = new Wish
            {
                Categorie = WishCategorie.KEUKEN,
                Description = "Verras ons!",
                IsChecked = false,
                Title = "Leuke set handdoeken",
                WishID = 2,
                WishListID = 1
            };

            Wish wish3 = new Wish
            {
                Categorie = WishCategorie.LIVING,
                Description = "Een staanlamp voor in de hoek, achter de zetel.",
                IsChecked = false,
                Title = "Staanlamp",
                WishID = 3,
                WishListID = 1
            };

            Wish wish4 = new Wish
            {
                Categorie = WishCategorie.BABY,
                Description = "Een buggy voor onze kleine Benny",
                IsChecked = false,
                Title = "Buggy",
                WishID = 4,
                WishListID = 2
            };

            Collection<Wish> WishesInACollection = new Collection<Wish>();
            Collection<Wish> OtherWishesInACollection = new Collection<Wish>();
            WishesInACollection.Add(wish1);
            WishesInACollection.Add(wish2);
            WishesInACollection.Add(wish3);
            OtherWishesInACollection.Add(wish4);

            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            date1.AddDays(13);
            date2.AddDays(43);

            WishList wishList1 = new WishList
            {
                Accessors = ThisUserInACollection,
                Wishes = WishesInACollection,
                DateOfEvent = date1,
                OwnerID = 1,
                Title = "Housewarming",
                WishListID = 1
            };

            WishList wishList2 = new WishList {
                Wishes = OtherWishesInACollection,
                WishListID = 2,
                DateOfEvent = date2,
                OwnerID = 2,
                Title = "Babyborrel"
            };

            WishListsOwning = new List<WishList>();
            WishListsOwning.Add(wishList2);
            WishListsAccessing = new List<WishList>();
            WishListsOwning.Add(wishList1);
        }
        // ---------------------
    }
}
