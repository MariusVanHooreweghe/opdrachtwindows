using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishMeAList.Models;
using WishMeAListAPI.Models;


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
    }
}