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
             getUser(txtUsername.Text);
          
        }

        private async Task getUser(String username)
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:65172/api/Users/"));
            ObservableCollection<User> Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
            if(Users.Any(val => val.Username.Trim().Equals(username.Trim())))
            {
                DisplayDialog("Username is already in use.", "Please enter other username and try again.");
                return;
            }else if (!txtPassword.Password.Trim().ToString().Equals(txtPasswordRepetition.Password.ToString()))
            {
                DisplayDialog("Passwords do not match", "Please enter matching passwords and try again.");
                return;
            }else if (txtFirstName.Text.Trim().Equals("") || txtLastName.Text.Trim().Equals(""))
            {
                DisplayDialog("You have empty fields", "Please fill in all fields and try again.");
                return;

            }
            User user = new User {
                Username = txtUsername.Text,
                Password = txtPassword.Password.ToString(),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text
            };
            PostUser(user);
            UserManager.CurrentUser = Users.Where(val => val.Username.Equals(username)).FirstOrDefault();
            RegisterViewModel vm = DataContext as RegisterViewModel;
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
        

        private async void PostUser(User user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            Debug.WriteLine(userJson);
            HttpClient client = new HttpClient();
            var res = await client.PostAsync("http://localhost:65172/api/users/", new StringContent(userJson, System.Text.Encoding.UTF8, "application/json"));
            if (res.Content != null)
            {
                string newUserJson = await res.Content.ReadAsStringAsync();
              
            }
        }
    }
}
