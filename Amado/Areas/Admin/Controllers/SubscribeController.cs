using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SubscribeController : Controller
    {
        private readonly AmadoDbContext _context;

        public SubscribeController(AmadoDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            List<Subscribe> subscribes = _context.Subscribes.ToList();

            List<SubscribeVM> model = new();
            foreach (var subscribeItem in subscribes)
            {
                SubscribeVM subscribeVM = new()
                {
                    Email = subscribeItem.Email
                };

                model.Add(subscribeVM);
            }
            return View(model);
        }
    }
}
