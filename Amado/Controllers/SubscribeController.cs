using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Amado.Controllers
{

    public class SubscribeController : Controller
    {
        private readonly AmadoDbContext _context;

        public SubscribeController(AmadoDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Subscribe(SubscribeVM newSub)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return NotFound();
            }

            bool isDuplicated = _context.Subscribes
                .Any(c => c.Email == newSub.Email);

            if (isDuplicated)
            {
                TempData["Subscribe"] = false;
                return RedirectToAction("Index", "Subscribe");
            }
            var subscribeEntity = new Subscribe
            {
                Email = newSub.Email
            };

            ViewBag.Subscribe = subscribeEntity.Email;


            _context.Subscribes.Add(subscribeEntity);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
