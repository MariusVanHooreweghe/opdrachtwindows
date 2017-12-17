using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WishMeAList.ViewModels;

namespace WishMeAList.Views
{

    public sealed partial class LogInView : Page
    {
        public LogInView()
        {
            this.InitializeComponent();
        }

        // Event handler
        private void LogIn(object sender, RoutedEventArgs e)
        {
            // TO DO
            Debug.WriteLine($"Implement logging in logic here with username {txtUsername.Text} and password {txtPassword.Password.ToString()}");
            LogInViewModel vm = DataContext as LogInViewModel;
            vm.ShowNavigatorView();
        }

    }
}
