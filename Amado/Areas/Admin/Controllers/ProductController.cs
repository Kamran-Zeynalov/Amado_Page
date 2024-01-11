using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AmadoDbContext _context;
        public ProductController(AmadoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.OrderByDescending(p => p.Id).ToList();

            ProductListVM productList = new()
            {
                Products = products,
            };
            return View(productList);
        }

        public IActionResult Create()
        {
            var categories = _context.Category.AsNoTracking().ToList();
            var brands = _context.Brands.AsNoTracking().ToList();
            var colors = _context.Colors.AsNoTracking().ToList();

            var model = new ProductCreateVM
            {
                Categories = categories,
                Brands = brands,
                Colors = colors
            };
            return View(model);
        }
    }
}
