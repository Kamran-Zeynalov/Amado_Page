using Amado.Data;
using Amado.Entities;
using Amado.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amado.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AmadoDbContext _context;

        public HomeController(AmadoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CheckOut> checkOuts = _context.CheckOuts.Include(c => c.Country).ToList();
            return View(checkOuts);
        }

    }
}
