﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Models
{
    public class User
    {
        public int UserID { get; set; }
        public virtual Collection<WishList> WishListsOwning { get; set; }
        public virtual Collection<WishList> WishListsAccessing { get; set; }
        public virtual Collection<Wish> WishesBuying { get; set; }
    }
}
