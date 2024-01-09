using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AmadoDbContext _context;

        public ProductController(AmadoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Product product = _context.Products.FirstOrDefault();
            return View(product);
        }
    }
}
