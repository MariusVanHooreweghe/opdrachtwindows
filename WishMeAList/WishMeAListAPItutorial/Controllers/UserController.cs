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
    [Route("api/Users")]
    public class UserController : Controller
    {
        private readonly WishListContext _context;

        public UserController(WishListContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.WishesBuying).Include(u => u.WishListsOwning).ThenInclude(wl => wl.Wishes).Include(u => u.WishListsAccessing).ToList();
        }
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var item = _context.Users.FirstOrDefault(t => t.UserID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("friendships/{id}", Name = "GetFriends")]
        public IEnumerable<User> GetFriendsById(long id)
        {
            IEnumerable<User> befriendedFriends = _context.Friendships.Where(f => f.BefrienderID == id).Select(f => f.Befriended).ToList();
            IEnumerable<User> befrienderFriends = _context.Friendships.Where(f => f.BefriendedID == id).Select(f => f.Befriender).ToList();
            return Enumerable.Union(befriendedFriends, befrienderFriends);
        }
        [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Users.Add(item);
            if (item.WishListsAccessing?.Count() != 0)
            {
                foreach (WishListAccessor wla in item.WishListsAccessing){
                    _context.WishListAccessors.Add(wla);
                }
            }
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = item.UserID }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] User item)
        {
            if (item == null || item.UserID != id)
            {
                return BadRequest();
            }

            var user = _context.Users.FirstOrDefault(t => t.UserID == id);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.WishListsOwning = item.WishListsOwning;
            user.WishListsAccessing = item.WishListsAccessing;
            user.WishesBuying = item.WishesBuying;

            _context.Users.Update(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var user = _context.Users.FirstOrDefault(t => t.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}