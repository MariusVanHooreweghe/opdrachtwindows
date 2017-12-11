using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        public RelayCommand ShowRegisterCommand { get; set; }
        private MainPageViewModel _parent { get; set; }

        public LogInViewModel(MainPageViewModel parent)
        {
            this._parent = parent;
            ShowRegisterCommand = new RelayCommand(_ => _parent.ShowRegister());
        }

        public void ShowNavigatorView()
        {
            this._parent.ShowNavigator();
        }
    }
}
