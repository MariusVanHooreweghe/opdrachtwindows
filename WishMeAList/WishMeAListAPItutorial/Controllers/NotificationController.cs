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
    [Route("api/Notifications")]
    public class NotificationController : Controller
    {
        private readonly WishListContext _context;

        public NotificationController(WishListContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Notification> GetAll()
        {
            return _context.Notifications.Include(n => n.Reciever).Include(n => n.Sender).Include(n => n.WishList).ToList();
        }
        [HttpGet("{id}", Name = "GetNotification")]
        public IActionResult GetById(long id)
        {
            var item = _context.Notifications.Include(n => n.Reciever).Include(n => n.Sender).Include(n => n.WishList).FirstOrDefault(n => n.NotificationID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("user/{id}", Name = "GetNotificationByUser")]
        public IEnumerable<Notification> GetByUserId(long id)
        { 
            return _context.Notifications.Include(n => n.Reciever).Include(n => n.Sender).Include(n => n.WishList).Where(w => w.RecieverID == id);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Notification item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Notifications.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetNotification", new { id = item.NotificationID }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Notification item)
        {
            if (item == null || item.NotificationID != id)
            {
                return BadRequest();
            }

            var notification = _context.Notifications.FirstOrDefault(n => n.NotificationID == id);
            if (notification == null)
            {
                return NotFound();
            }

            notification.Message = item.Message;
            notification.HasBeenRead = item.HasBeenRead;
            notification.Type = item.Type;
            notification.WishList = item.WishList;
            notification.Date = item.Date;
            notification.Reciever = notification.Reciever;
            notification.Sender = notification.Sender;
            _context.Notifications.Update(notification);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var notification = _context.Notifications.FirstOrDefault(n => n.NotificationID == id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}