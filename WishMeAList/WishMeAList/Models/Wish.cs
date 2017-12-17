using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WishMeAList.Models
{
    public class Wish : INotifyPropertyChanged
    {
        [JsonProperty]
        public int WishID { get; set; }
        [ForeignKey("WishList")]
        public int WishListID { get; set; }
        [JsonProperty]
        public string Title { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public WishCategorie Categorie { get; set; }
        [JsonProperty]
        public bool IsChecked { get; set; }
        public string ImageURL { get; set; }
        [JsonProperty]
        public int? BuyerID { get; set; }
        [JsonIgnore]
        public User Buyer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
