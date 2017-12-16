using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WishMeAListAPItutorial.Models
{
    public class Friendship
    {
        [ForeignKey("User")]
        public int BefrienderID { get; set; }
        public User Befriender { get; set; }
        [ForeignKey("User")]
        public int BefriendedID { get; set; }
        public User Befriended { get; set; }
    }
}
