using System;
using System.Collections.Generic;
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
using WishMeAList.Models;
using WishMeAList.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WishMeAList.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationsView : Page
    {
        public NotificationsView()
        {
            this.InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            Notification not = (Notification)btn.CommandParameter;
            NotificationsViewModel vm = DataContext as NotificationsViewModel;
            vm.AcceptNotification(not);
        }

        private void Deny_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            Notification not = (Notification)btn.CommandParameter;
            NotificationsViewModel vm = DataContext as NotificationsViewModel;
            vm.DenyNotification(not);
        }
  
    }
}
