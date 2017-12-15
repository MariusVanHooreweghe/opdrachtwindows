using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WishMeAList.Models
{
    public class WishList
    {
        public int WishListID { get; set; }
        [ForeignKey("User")]
        public int OwnerID { get; set; }
        public string Title { get; set; }
        public DateTime DateOfEvent { get; set; }
        public virtual Collection<Wish> Wishes { get; set; }
        public virtual Collection<User> Accessors { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WishList()
        {
            Wishes = new Collection<Wish>();
            Accessors = new Collection<User>();
        }

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
