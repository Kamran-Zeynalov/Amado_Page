using Amado.Data;
using Amado.Entities;
using Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Text.Json;

namespace Amado.Controllers
{
    public class ShopController : Controller
    {
        private readonly AmadoDbContext _context;

        public ShopController(AmadoDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page, string order = "desc")
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
                Products = pagedProducts
                .Skip((page - 1) * productsPerPage)
                .Take(3)
                .ToList(),
                TotalPageCount = totalPageCount,
                CurrentPage = page,
            };

            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();

            Product? product = _context.Products
                .Include(p => p.ProductImages).ThenInclude(pi => pi.Image)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            IndexVM model = new()
            {
                Product = product,
            };

            return View(model);
        }


        public IActionResult AddToBasket(int? id)
        {
            if (id is null) return NotFound();

            var foundProduct = _context.Products.FirstOrDefault(x => x.Id == id);

            if (foundProduct is null) return NotFound();

            Request.Cookies.TryGetValue("basket", out var basketSerialized);

            Basket basket = null!;
            if (basketSerialized is null)
            {
                basket = new Basket();
            }
            else
            {
                basket = JsonSerializer.Deserialize<Basket>(basketSerialized)!;
            }


            var foundBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == foundProduct.Id);

            if (foundBasketItem is null)
            {
                foundBasketItem = new BasketItem
                {
                    ProductId = foundProduct.Id,
                    Count = 1
                };

                basket.BasketItems.Add(foundBasketItem);
            }
            else
            {
                foundBasketItem.Count++;
            }

            Response.Cookies.Append("basket", JsonSerializer.Serialize(basket));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteFromBasket(int? id)
        {
            if (id is null) return NotFound();

            Request.Cookies.TryGetValue("basket", out var basketSerialized);

            if (basketSerialized is null) return RedirectToAction("Index", "Home");

            var basket = JsonSerializer.Deserialize<Basket>(basketSerialized)!;

            var foundBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == id);

            if (foundBasketItem is null) return NotFound();

            basket.BasketItems.Remove(foundBasketItem);

            Response.Cookies.Append("basket", JsonSerializer.Serialize(basket));

            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Search(string? input)
        {
            var products = input == null ? new List<Product>()
                : _context.Products
                    .Where(x => x.Name.ToLower().StartsWith(input.ToLower()))
                    .ToList();

            return ViewComponent("SearchResult", products);
        }

        public IActionResult Filter(int page, string order = "desc")
        {
            if (page <= 0) page = 1;

            int productsPerPage = 3;
            var productCount = _context.Products.Count();

            int totalPageCount = (int)Math.Ceiling(((decimal)productCount / productsPerPage));

            var products = order switch
            {
                "desc" => _context.Products.OrderByDescending(x => x.Id),
                "asc" => _context.Products.OrderBy(x => x.Id),
                _ => _context.Products.OrderByDescending(x => x.Id)
            };

            var model = new IndexVM
            {
                Products = products
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .ToList(),
                TotalPageCount = totalPageCount,
                CurrentPage = page
            };

            return PartialView("_ShopPartial", model);
        }

        public IActionResult Sorted(int titleid, int brandid, int colorid, int page = 1)
        {
            IQueryable<Product> allproductss = _context.Products
                .Include(p => p.ProductImages).ThenInclude(pi => pi.Image)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductColors).ThenInclude(pc => pc.Color);

            ViewBag.TotalPage = Math.Ceiling((double)_context.Products.Count() / 8);
            ViewBag.CurrentPage = page;
            if (titleid == 0 && brandid == 0 && colorid == 0)
            {
                List<Product> products = allproductss.ToList();
                var pro = new IndexVM
                {
                    Products = products,
                };
                return PartialView("_ShopPartial", pro);
            }

            List<Product> sortedProducts = allproductss
                   .Where(x => (titleid == 0 || x.CategoryId == titleid)
                            && (brandid == 0 || x.BrandId == brandid)
                            && (colorid == 0 || x.ProductColors.FirstOrDefault().ColorId == colorid))
                   .OrderBy(x => x.Id)
                   .Skip((page - 1) * 8)
                   .ToList();
            var model = new IndexVM
            {
                Products = sortedProducts,
            };
            return PartialView("_ShopPartial", model);
        }
    }
}
