using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;

namespace WishMeAList.ViewModels
{
    public class AccessorsAccessingViewModel : ViewModelBase
    {
        public String Title { get; set; }
        private Collection<User> _accessors;
        private WishListAccessingViewModel _parent;
        public ObservableCollection<User> Accessors
        {
            get { return new ObservableCollection<User>(_accessors.OrderBy(val => val.FirstName).ThenBy(val => val.LastName)); }
            set { _accessors = value; RaisePropertyChanged("Accessors"); }
        }

        public AccessorsAccessingViewModel(WishListAccessingViewModel parent)
        {
            _parent = parent;
            this.Title = parent.WishList.Title + " - Accessors";
            //this._accessors = new ObservableCollection<User>(parent.WishList.Accessors);
            InitAccessors();
        }


        private async void InitAccessors()
        {
            var accessorsJson = "{}";
            Accessors = new ObservableCollection<User>();
            try
            {
                HttpClient client = new HttpClient();
                accessorsJson = await client.GetStringAsync(new Uri("http://localhost:65172/api/wishlists/accessors/" + _parent.WishList.WishListID));
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            Accessors = JsonConvert.DeserializeObject<ObservableCollection<User>>(accessorsJson);
            RaisePropertyChanged("Accessors");
        }
    }
}
