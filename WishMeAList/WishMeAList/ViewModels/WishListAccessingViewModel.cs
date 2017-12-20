using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class WishListAccessingViewModel : ViewModelBase
    {
        private NavigatorViewModel _parent { get; set; }
        private Collection<Wish> _wishes;

        private Wish _wish;
        public Wish Wish {
            get { return _wish; }
            set { _wish = value; RaisePropertyChanged("Wish"); }
        }

        public WishList WishList { get; set; }
        public RelayCommand OpenAccessorsCommand { get; set; }
        public RelayCommand BuyWishCommand {get;set;}

        private Boolean _isChecked;
        public Boolean IsChecked {
            get { return _isChecked; }
            set { _isChecked = value; RaisePropertyChanged("IsChecked"); }
        }

        public ObservableCollection<Wish> Wishes
        {
            get { return new ObservableCollection<Wish>(_wishes); }
            set { _wishes = value; RaisePropertyChanged("Wishes"); }
        }
        public List<WishCategorie> EnumVal { get; set; }

        private WishCategorie _filterCategory;
        public WishCategorie FilterCategory {
            get { return _filterCategory; }
            set {
                _filterCategory = value;
                Wishes = new ObservableCollection<Wish>(WishList.Wishes);
                if (_filterCategory != WishCategorie.DEFAULT)
                {
                    Wishes = new ObservableCollection<Wish>(Wishes.Where(val => val.Categorie == _filterCategory).ToList());
                }
                RaisePropertyChanged("FilterCategory");
                RaisePropertyChanged("Wishes");
            }

        }

        public WishListAccessingViewModel(WishList wishList, NavigatorViewModel parent)
        {
            this.WishList = wishList;
            this._parent = parent;
            this._wishes = new ObservableCollection<Wish>(wishList.Wishes);
            EnumVal = Enum.GetValues(typeof(WishCategorie)).Cast<WishCategorie>().ToList();


            OpenAccessorsCommand = new RelayCommand(_ => OpenAccessors());
            BuyWishCommand = new RelayCommand(_ => BuyWish());
        }

        private void BuyWish()
        {
            if (WishList == null)
            {
                DisplayDialog();
            }
            else
            {
                if (Wish.IsChecked)
                {
                    Wish.IsChecked = false;
                    Wish.Buyer = null;
                    Wish.BuyerID = null;
                    UserManager.CurrentUser.WishesBuying.Remove(Wish);
                }
                else
                {
                    Wish.IsChecked = true;
                    Wish.Buyer = UserManager.CurrentUser;
                    Wish.BuyerID = Wish.Buyer.UserID;
                    UserManager.CurrentUser.WishesBuying.Add(Wish);
                }
                UpdateWish(Wish);
                RaisePropertyChanged("IsChecked");
                RaisePropertyChanged("Wish");
                RaisePropertyChanged("Wishes");
            }
        }

        private async Task UpdateWish(Wish wish)
        {
            HttpClient client = new HttpClient();
            string wishJson = JsonConvert.SerializeObject(Wish);
            await client.PutAsync("http://localhost:65172/api/Wishes/" + wish.WishID, new StringContent(wishJson, System.Text.Encoding.UTF8, "application/json"));
        }


        private void OpenAccessors()
        {
            this._parent.CurrentData = new AccessorsAccessingViewModel(this);
        }

        private async void DisplayDialog()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "No Wish selected",
                Content = "Please select a Wish and try again.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await dialog.ShowAsync();
        }

    }
}
