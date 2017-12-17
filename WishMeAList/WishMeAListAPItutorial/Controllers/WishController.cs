using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WishMeAListAPItutorial.Data;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Wishes")]
    public class WishController : Controller
    {
        private readonly WishListContext _context;

        public WishController(WishListContext context)
        {
            _context = context;

            if (_context.Wishes.Count() == 0)
            {
            _context.Wishes.Add(new Wish { WishListID = 3, Title = "Wish 1", IsChecked = false, ImageURL = "https://i.imgur.com/wKjdA9G.jpg", Categorie = WishCategorie.ANDERE, BuyerID=1, Description="Corgi with a plant on his head" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Wish> GetAll()
        {
            return _context.Wishes.Include(u => u.Buyer).ToList();
        }
        [HttpGet("{id}", Name = "GetWish")]
        public IActionResult GetById(long id)
        {
            var item = _context.Wishes.Include(u => u.Buyer).FirstOrDefault(t => t.WishID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("buyer/{id}", Name = "GetWishesByBuyerID")]
        public IEnumerable<Wish> GetWishesByBuyerId(long id)
        {
            return _context.Wishes.Where(w => w.BuyerID == id).Include(w => w.Buyer).ToList();
        }
        [HttpPost]
        public IActionResult Create([FromBody] Wish item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Wishes.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetWish", new { id = item.WishID }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Wish item)
        {
            if (item == null || item.WishID != id)
            {
                return BadRequest();
            }

            var wish = _context.Wishes.FirstOrDefault(t => t.WishID == id);
            if (wish == null)
            {
                return NotFound();
            }
            wish.BuyerID = item.BuyerID;
            wish.Categorie = item.Categorie;
            wish.Description = item.Description;
            wish.ImageURL = item.ImageURL;
            wish.IsChecked = item.IsChecked;
            wish.Title = item.Title;
            wish.WishListID = item.WishListID;

            _context.Wishes.Update(wish);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var wish = _context.Wishes.FirstOrDefault(t => t.WishID == id);
            if (wish == null)
            {
                return NotFound();
            }

            _context.Wishes.Remove(wish);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}