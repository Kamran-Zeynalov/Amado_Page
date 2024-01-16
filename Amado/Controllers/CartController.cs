using Amado.Data;
using Amado.Entities;
using Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Amado.Controllers
{
    public class CartController : Controller
    {
        private readonly AmadoDbContext _context;

        public CartController(AmadoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Cart";

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

            List<(BasketItem, Product)> items = new();

            foreach (var basketItem in basket.BasketItems)
            {
                Product product = _context.Products.Include(p => p.ProductImages).ThenInclude(p => p.Image).FirstOrDefault(x => x.Id == basketItem.ProductId)!;

                items.Add(new(basketItem, product));
            }
            var model = new BasketVM
            {
                Items = items
            };

            return View(model);
        }
    }
}
