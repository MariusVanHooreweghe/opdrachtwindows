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
        public RelayCommand ShowFriendsCommand { get; set; }

        public NavigatorViewModel()
        {
            // Dummy data
            initData();
            // ----------
            ShowWishlistsOwningCommand = new RelayCommand(_ => ShowWishlistsOwning());
            ShowWishlistsAccessingCommand = new RelayCommand(_ => ShowWishlistsAccessing());
            ShowWishesBuyingCommand = new RelayCommand(_ => ShowWishesBuying());
            ShowFriendsCommand = new RelayCommand(_ => ShowFriends());
            ShowWishlistsOwning();
        }

        private void ShowWishlistsOwning()
        {
            CurrentData = new WishListsViewModel(WishListsOwning, this);
        }

        private void ShowWishlistsAccessing()
        {
            CurrentData = new WishListsAccessingViewModel(WishListsAccessing, this);
        }

        private void ShowWishesBuying()
        {
            CurrentData = new WishesBuyingViewModel();
        }

        private void ShowFriends()
        {
            CurrentData = new FriendsViewModel(this);
        }

        // Dummy data
        public List<WishList> WishListsOwning { get; set; }
        public List<WishList> WishListsAccessing { get; set; }

        public void initData()
        {
            User ThisUser = new User {
                UserID = 2,
                FirstName = "Ruben",
                LastName = "Standaert",
                WishListsAccessing = new Collection<WishList>(),
                WishListsOwning = new Collection<WishList>(),
                WishesBuying = new Collection<Wish>()
            };
            User OtherUser = new User {
                UserID = 1,
                FirstName = "Thibault",
                LastName = "Gobert",
                WishListsAccessing = new Collection<WishList>(),
                WishListsOwning = new Collection<WishList>(),
                WishesBuying = new Collection<Wish>()
            };

            Collection<User> ThisUserInACollection = new Collection<User>();
            ThisUserInACollection.Add(ThisUser);
            Collection<User> OtherUserInACollection = new Collection<User>();
            OtherUserInACollection.Add(OtherUser);

            Wish wish1 = new Wish
            {
                Buyer = OtherUser,
                Categorie = WishCategorie.KITCHEN,
                Description = "Het liefst een gele.",
                IsChecked = false,
                Title = "Mixer",
                WishID = 1,
                WishListID = 1
            };

            Wish wish2 = new Wish
            {
                Categorie = WishCategorie.KITCHEN,
                Description = "Verras ons!",
                IsChecked = false,
                Title = "Leuke set handdoeken",
                WishID = 2,
                WishListID = 1
            };

            Wish wish3 = new Wish
            {
                Categorie = WishCategorie.LIVING_ROOM,
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
            WishListsAccessing.Add(wishList1);

            ThisUser.Friends = OtherUserInACollection;
            OtherUser.Friends = ThisUserInACollection;

            UserManager.CurrentUser = ThisUser;
        }
        // ---------------------
    }
}
