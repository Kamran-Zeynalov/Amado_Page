using Amado.Data;
using Amado.Entities;
using Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Amado.Controllers
{
    public class ShopController : Controller
    {
        private readonly AmadoDbContext _context;

        public ShopController(AmadoDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int titleid, int page, string order = "desc")
        {
            if (page <= 0) page = 1;

            int productsPerPage = 3;
            var productCount = await _context.Products.CountAsync();

            int totalPageCount = (int)Math.Ceiling(((decimal)productCount / productsPerPage));

            var productsQuery = order switch
            {
                "desc" => _context.Products.OrderByDescending(x => x.Id),
                "asc" => _context.Products.OrderBy(x => x.Id),
                _ => _context.Products.OrderByDescending(x => x.Id)
            };

            var pagedProducts = await productsQuery
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .Include(p => p.ProductImages).ThenInclude(pi => pi.Image)
                .ToListAsync();

            ViewBag.Order = order;
            ViewBag.Categories = _context.Category.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Color = _context.Colors.ToList();

            var model = new IndexVM
            {
                Products = pagedProducts,
            };

            return View(model);
        }

        public IActionResult Sorted(int titleid, int page = 1)
        {
            IQueryable<Product> allproductss = _context.Products
                .Include(p => p.ProductImages).ThenInclude(pi => pi.Image)
                .Include(p => p.Category);
            ViewBag.TotalPage = Math.Ceiling((double)_context.Products.Count() / 8);
            ViewBag.CurrentPage = page;
            if (titleid == 0)
            {
                List<Product> products = allproductss.ToList();
                var pro = new IndexVM
                {
                    Products = products,
                };
                return PartialView("_ShopPartial", pro);

            }

            List<Product> sortvacans = allproductss.OrderBy(x => x.Id).Where(x => x.CategoryId == titleid).Skip((page - 1) * 8).ToList();
            var model = new IndexVM
            {
                Products = sortvacans,
            };
            return PartialView("_ShopPartial", model);
        }
    }
}
