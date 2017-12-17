using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using WishMeAList.Utils;
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
            getUser(txtUsername.Text);
        }

        private async Task getUser(String username)
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:65172/api/Users/"));
            ObservableCollection<User> Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
            if (!Users.Any(val => val.Username.Equals(username)) || !txtPassword.Password.ToString()
                .Equals(Users.Where(val => val.Username.Equals(username)).FirstOrDefault().Password))
            {
                DisplayDialog("Username or password do not match", "Please enter a valid username/password and try again.");
                return;
            }
            UserManager.CurrentUser = Users.Where(val => val.Username.Equals(username)).FirstOrDefault();
            LogInViewModel vm = DataContext as LogInViewModel;
            vm.ShowNavigatorView();
        }

        private async void DisplayDialog(String title, String content)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await dialog.ShowAsync();
        }

    }
}
