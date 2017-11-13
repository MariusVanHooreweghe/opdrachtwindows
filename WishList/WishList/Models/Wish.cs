using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Models
{
    public class Wish : INotifyPropertyChanged
    {
        public int WishID { get; set; }
        [ForeignKey("WishList")]
        public int WishListID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WishCategorie Categorie { get; set; }
        public bool IsChecked { get; set; }
        public string ImageURL { get; set; }
        public User Buyer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
