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
    public class WishesBuyingViewModel : ViewModelBase
    {
        private Collection<Wish> _wishesBuying;
        public ObservableCollection<Wish> WishesBuying {
            get { return new ObservableCollection<Wish>(_wishesBuying); }
            set { _wishesBuying = value; RaisePropertyChanged("Wishes"); }
        }

        public WishesBuyingViewModel()
        {
            WishesBuying = new ObservableCollection<Wish>(UserManager.CurrentUser.WishesBuying);
        }
    }
}
