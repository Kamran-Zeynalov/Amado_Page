//using Amado.Models;
using Amado.Data;
using Amado.Entities;
using Amado.Models;
using Amado.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Amado.Controllers
{
    public class HomeController : Controller
    {
        private readonly AmadoDbContext _context;

        public HomeController(AmadoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Home";

            List<Product> products = _context.Products.Include(p => p.ProductImages).ThenInclude(pi => pi.Image).Take(10).ToList();

            IndexVM model = new()
            {
                Products = products,
            };
            return View(model);
        }
    }
}