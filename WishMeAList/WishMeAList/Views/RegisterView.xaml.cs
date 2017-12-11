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
    public sealed partial class RegisterView : Page
    {
        public RegisterView()
        {
            this.InitializeComponent();
        }

        // Event handler
        private void Register(object sender, RoutedEventArgs e)
        {
            // TO DO
            Debug.WriteLine($"Implement register logic here with email {txtEmail.Text}, password {txtPassword.Password.ToString()} and password" +
                $" repetition {txtPasswordRepetition.Password.ToString()}");
            RegisterViewModel vm = DataContext as RegisterViewModel;
            vm.ShowNavigatorView();
        }
    }
}
