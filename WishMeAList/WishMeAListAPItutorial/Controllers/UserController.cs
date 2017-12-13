﻿using System;
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

            //if (_context.Users.Count() == 0)
            //{
                //_context.Users.Add(new User());
                //_context.SaveChanges();
            //}
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