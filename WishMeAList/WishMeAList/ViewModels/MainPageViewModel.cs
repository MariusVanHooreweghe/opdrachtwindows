using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ViewModelBase _currentData;
        public ViewModelBase CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; RaisePropertyChanged("CurrentData"); }
        }
        public RelayCommand ShowLogInCommand { get; set; }
        public RelayCommand ShowRegisterCommand { get; set; }

        public MainPageViewModel()
        {
            ShowLogInCommand = new RelayCommand(_ => ShowLogIn());
            ShowRegisterCommand = new RelayCommand(_ => ShowRegister());
            ShowLogIn();
        }

        public void ShowLogIn()
        {
            CurrentData = new LogInViewModel(this);
        }

        public void ShowRegister()
        {
            CurrentData = new RegisterViewModel(this);
        }

        public void ShowNavigator()
        {
            CurrentData = new NavigatorViewModel(this);
        }
    }
}
