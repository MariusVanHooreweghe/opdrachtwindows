using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishMeAListAPItutorial.Models;
using WishMeAListAPItutorial.Data;
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

            if (_context.WishLists.Count() == 0)
            {
                _context.WishLists.Add(new WishList { Title = "Item1" });
                _context.WishLists.Add(new WishList { Title = "Item2" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<WishList> GetAll()
        {
            return _context.WishLists.ToList();
        }
        [HttpGet("{id}", Name = "GetWishList")]
        public IActionResult GetById(long id)
        {
            var item = _context.WishLists.FirstOrDefault(t => t.WishListID == id);
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
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var wishList = _context.WishLists.FirstOrDefault(t => t.WishListID == id);
            if (wishList == null)
            {
                return NotFound();
            }

            _context.WishLists.Remove(wishList);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}