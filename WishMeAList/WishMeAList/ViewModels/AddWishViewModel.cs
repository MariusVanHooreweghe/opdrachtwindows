﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WishMeAList.Models;
using WishMeAList.Utils;

namespace WishMeAList.ViewModels
{
    class AddWishViewModel : ViewModelBase
    {
        public Wish Wish {get;set;}
        public WishList WishList { get; set; }
        public ObservableCollection<Wish> Wishes { get; set; }
        public NavigatorViewModel _parent { get; set; }
        public RelayCommand ConfirmWishCommand { get; set; }

        public List<WishCategorie> EnumVal { get; set; }

        private String _title;
        public String Title {
            get { return _title; }
            set { _title = value; RaisePropertyChanged("Title"); }
        }
        private String _description;
        public String Description {
            get { return _description; }
            set { _description = value; RaisePropertyChanged("Description"); }
        }
        private WishCategorie _categorie;
        public WishCategorie Categorie {
            get { return _categorie;  }
            set { _categorie = value; RaisePropertyChanged("Categorie"); }
        }


        public AddWishViewModel(WishList wishlist, NavigatorViewModel parent)
        {
            _parent = parent;
            WishList = wishlist;
            Wishes = new ObservableCollection<Wish>(wishlist.Wishes);
            ConfirmWishCommand = new RelayCommand(_ => ConfirmWish());
            EnumVal = Enum.GetValues(typeof(WishCategorie)).Cast<WishCategorie>().ToList();
        }

        public void ConfirmWish()
        {
            Wish = new Wish
            {
                Title = this.Title,
                Description = this.Description,
                Categorie = this.Categorie
            };

            Wishes.Add(Wish);
            //sorting the wishes by Category then by Title
            Wishes = new ObservableCollection<Wish>(Wishes.OrderBy(val => val.Categorie).ThenBy(val => val.Title));
            WishList.Wishes = Wishes;

            _parent.WishListsOwning.Where(val => val.WishListID == WishList.WishListID).SingleOrDefault().Wishes = Wishes;
            _parent.CurrentData = new WishListViewModel(WishList, this._parent);
        }

    }
}