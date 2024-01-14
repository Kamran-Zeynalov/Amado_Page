using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amado.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly AmadoDbContext _context;

        public CheckOutController(AmadoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendInfo(CheckOut newCheck)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return View();
            }

            bool isDuplicated = _context.CheckOuts
                .Any(c => c.Name == newCheck.Name && c.Surname == newCheck.Surname);

            List<Country> countries = _context.Countries.AsNoTracking().ToList();

            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }

            newCheck = new()
            {
                Countries = countries,
            };
            _context.CheckOuts.Add(newCheck);
            _context.SaveChanges();
            return View();
        }
    }
}
