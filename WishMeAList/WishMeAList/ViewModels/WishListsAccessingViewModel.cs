using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class WishListsAccessingViewModel : ViewModelBase
    {
        private Collection<WishList> _wishLists { get; set; }
        public ObservableCollection<WishList> WishLists {
            get { return new ObservableCollection<WishList>(_wishLists); }
            set { _wishLists = value; RaisePropertyChanged("WishLists"); }
        }
        private NavigatorViewModel _parent { get; set; }

        public WishListsAccessingViewModel( NavigatorViewModel parent)
        {
            this._parent = parent;
            _wishLists = new ObservableCollection<WishList>();
            InitWishLists();
        }

        private async Task InitWishLists()
        {
            try
            {
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(new Uri("http://localhost:65172/api/WishLists/accessor/" + UserManager.CurrentUser.UserID));
                WishLists = JsonConvert.DeserializeObject<ObservableCollection<WishList>>(json);
                RaisePropertyChanged("ViewWishesVisibility");
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                WishLists = new ObservableCollection<WishList>();
            }
        }

        public void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            WishList _wishList = (WishList)e.ClickedItem;
            this._parent.CurrentData = new WishListAccessingViewModel(WishLists.Where(val =>
                    val.WishListID == _wishList.WishListID).SingleOrDefault(), this._parent);
        }

    }
}
