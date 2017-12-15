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
        public WishList WishList { get; set; }
        private ObservableCollection<Wish> _wishes;
        public RelayCommand OpenAccessorsCommand { get; set; }
        public ObservableCollection<Wish> Wishes
        {
            get { return _wishes; }
            set { _wishes = value; RaisePropertyChanged("Wishes"); }
        }

        public WishListAccessingViewModel(WishList wishList, NavigatorViewModel parent)
        {
            this.WishList = wishList;
            this._parent = parent;
            this._wishes = new ObservableCollection<Wish>(wishList.Wishes);

            OpenAccessorsCommand = new RelayCommand(_ => OpenAccessors());
        }

        private void OpenAccessors()
        {
            this._parent.CurrentData = new AccessorsAccessingViewModel(this);
        }

    }
}
