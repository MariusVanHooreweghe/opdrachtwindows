﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishMeAListAPItutorial.Models;
using WishMeAListAPItutorial.Data;
using Microsoft.EntityFrameworkCore;

namespace WishMeAListAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/WishLists")]
    public class WishListController : Controller
    {
        private readonly WishListContext _context;

        public WishListController(WishListContext context)
        {
            _context = context;

            //if (_context.WishLists.Count() == 0)
            //{
            //_context.WishLists.Add(new WishList { Title = "Item1", DateOfEvent = DateTime.Now, OwnerID = 1});
            //    _context.WishLists.Add(new WishList { Title = "Item2" });
            //_context.SaveChanges();
            //}
        }
        [HttpGet]
        public IEnumerable<WishList> GetAll()
        {
            return _context.WishLists.Include(wl => wl.Wishes).ThenInclude(w => w.Buyer).Include(wl => wl.Accessors).ThenInclude(wla => wla.User).ToList();
        }
        [HttpGet("user/{id}", Name ="GetWishListByUserID")]
        public IEnumerable<WishList> GetByUserID(long id)
        {
            return _context.WishLists.Include(wl => wl.Wishes).ThenInclude(w => w.Buyer).Where(wl => wl.OwnerID == id);
        }
        [HttpGet("accessor/{id}", Name = "GetWishListByAccessorID")]
        public IEnumerable<WishList> GetByAccessorID(long id)
        {
            return _context.WishLists.Include(wl => wl.Wishes).ThenInclude(w => w.Buyer).Where(wl => wl.Accessors.Select(a => a.UserID).Any(a => a == id));
        }
        [HttpGet("accessors/{id}", Name = "GetAccessorsByWishListID")]
        public IEnumerable<User> GetAccessorsByID(long id)
        {
            return _context.WishListAccessors.Include(wla => wla.User).ThenInclude(u => u.Notifications).Include(wla => wla.WishList).Where(wla => wla.WishListID == id).Select(wla => wla.User).Include(u => u.WishesBuying).ThenInclude(w => w.Buyer).Include(u => u.WishListsAccessing).Include(u => u.WishListsOwning);
        }
        [HttpGet("{id}", Name = "GetWishList")]
        public IActionResult GetById(long id)
        {
            var item = _context.WishLists.Include(wl => wl.Wishes).Include(wl => wl.Accessors).ThenInclude(wla => wla.User).FirstOrDefault(t => t.WishListID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] WishList item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.WishLists.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetWishList", new { id = item.WishListID }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] WishList item)
        {
            if (item == null || item.WishListID != id)
            {
                return BadRequest();
            }

            var wishList = _context.WishLists.FirstOrDefault(t => t.WishListID == id);
            if (wishList == null)
            {
                return NotFound();
            }

            wishList.Title = item.Title;

            _context.WishLists.Update(wishList);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPost("accessor/{userid}/create/{wishlistid}")]
        public IActionResult CreateAccess(long userid, long wishlistid)
        {
            WishListAccessor wla = new WishListAccessor();
            wla.UserID = (int)userid;
            wla.WishListID = (int)wishlistid;
            _context.WishListAccessors.Add(wla);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            List<WishListAccessor> wlAccessors = _context.WishListAccessors.Where(t => t.WishListID == id).ToList();
            wlAccessors.ForEach(wla => DeleteAccess(wla.WishListID, wla.UserID));
            var wishList = _context.WishLists.FirstOrDefault(t => t.WishListID == id);
            if (wishList == null)
            {
                return NotFound();
            }

            _context.WishLists.Remove(wishList);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{wishlistid}/accessors/{userid}")]
        public IActionResult DeleteAccess(long wishlistid, long userid)
        {
            var wishListAccessor = _context.WishListAccessors.FirstOrDefault(t => t.WishListID == wishlistid && t.UserID == userid);
            if (wishListAccessor == null)
            {
                return NotFound();
            }
            _context.WishListAccessors.Remove(wishListAccessor);
            _context.SaveChanges();
            DeleteWishBuying(wishlistid, userid);
            return new NoContentResult();
        }

        private void DeleteWishBuying(long wishlistid, long userid)
        {
            _context.Wishes.Where(w => w.BuyerID == userid && w.WishListID == wishlistid).ToList().ForEach(item => {
                item.BuyerID = null;
                _context.Wishes.Update(item);
                _context.SaveChanges();
            });
        }
    }
}