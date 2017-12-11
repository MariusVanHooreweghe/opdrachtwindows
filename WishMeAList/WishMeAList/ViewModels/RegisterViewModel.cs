using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        public RelayCommand ShowLogInCommand { get; set; }
        private MainPageViewModel _parent { get; set; }

        public RegisterViewModel(MainPageViewModel parent)
        {
            this._parent = parent;
            ShowLogInCommand = new RelayCommand(_ => _parent.ShowLogIn());
        }

        public void ShowNavigatorView()
        {
            this._parent.ShowNavigator();
        }
    }
}
