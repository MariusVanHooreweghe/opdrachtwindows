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

        public WishListAccessingViewModel(WishList wishList, NavigatorViewModel parent)
        {
            this.WishList = wishList;
            this._parent = parent;
            this._wishes = new ObservableCollection<Wish>(wishList.Wishes);

            OpenAccessorsCommand = new RelayCommand(_ => OpenAccessors());
            BuyWishCommand = new RelayCommand(_ => BuyWish());
        }

        private void BuyWish()
        {
            if (Wish.IsChecked)
            {
                Wish.IsChecked = false;
                Wish.Buyer = UserManager.CurrentUser;
                UserManager.CurrentUser.WishesBuying.Remove(Wish);

            }
            else
            {
                Wish.IsChecked = true;
                Wish.Buyer = null;
                UserManager.CurrentUser.WishesBuying.Add(Wish);
            }
            RaisePropertyChanged("IsChecked");
            RaisePropertyChanged("Wish");
            RaisePropertyChanged("Wishes");
        }

        private void OpenAccessors()
        {
            this._parent.CurrentData = new AccessorsAccessingViewModel(this);
        }

    }
}
