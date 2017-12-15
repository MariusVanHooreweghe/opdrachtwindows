using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;

namespace WishMeAList.ViewModels
{
    public class AccessorsAccessingViewModel : ViewModelBase
    {
        public String Title { get; set; }
        private Collection<User> _accessors;
        public ObservableCollection<User> Accessors
        {
            get { return new ObservableCollection<User>(_accessors.OrderBy(val => val.FirstName).ThenBy(val => val.LastName)); }
            set { _accessors = value; RaisePropertyChanged("Accessors"); }
        }

        public AccessorsAccessingViewModel(WishListAccessingViewModel parent)
        {
            this.Title = parent.WishList.Title + " - Accessors";
            this._accessors = new ObservableCollection<User>(parent.WishList.Accessors);
        }
    }
}
