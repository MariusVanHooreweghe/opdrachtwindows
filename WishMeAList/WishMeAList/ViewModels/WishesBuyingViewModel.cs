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
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    public class WishesBuyingViewModel : ViewModelBase
    {
        private Collection<Wish> _wishesBuying;
        public ObservableCollection<Wish> WishesBuying {
            get { return new ObservableCollection<Wish>(_wishesBuying); }
            set { _wishesBuying = value; RaisePropertyChanged("Wishes"); }
        }

        public WishesBuyingViewModel()
        {
            WishesBuying = new ObservableCollection<Wish>();
            InitWishes();
        }

        private async Task InitWishes()
        {
            try
            {
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(new Uri("http://localhost:65172/api/wishes/buyer/" + UserManager.CurrentUser.UserID));
                WishesBuying = JsonConvert.DeserializeObject<ObservableCollection<Wish>>(json);
                RaisePropertyChanged("WishesBuying");
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                WishesBuying = new ObservableCollection<Wish>();
            }
        }
    }
}
