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
    public class FriendsViewModel : ViewModelBase
    {
        public ObservableCollection<User> Friends { get { return new ObservableCollection<User>(UserManager.CurrentUser.Friends); } }
        private NavigatorViewModel _parent { get; set; }

        public FriendsViewModel(NavigatorViewModel parent)
        {
            this._parent = parent;
        }
    }
}
