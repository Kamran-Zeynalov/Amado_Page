using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Services;
using Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AmadoDbContext _context;
        private readonly FileService _fileService;

        public ProductController(AmadoDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
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

        [HttpPost]
        public IActionResult Create(ProductCreateVM productVm)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return View(productVm);
            }

            var newProduct = new Product
            {
                Name = productVm.Name,
                Price = productVm.Price,
                Desc = productVm.Desc,
                InStock = productVm.InStock
            };

            var fCategory = _context.Category.FirstOrDefault(x => x.Id == productVm.CategoryId);
            var fBrand = _context.Brands.FirstOrDefault(x => x.Id == productVm.BrandId);
            var fColor = _context.Colors.FirstOrDefault(x => x.Id == productVm.ColorId);

            if (fCategory == null || fBrand == null || fColor == null)
            {
                productVm.Categories = _context.Category.AsNoTracking().ToList();
                productVm.Brands = _context.Brands.AsNoTracking().ToList();
                productVm.Colors = _context.Colors.AsNoTracking().ToList();
                return View(productVm);
            }

            newProduct.Category = fCategory;
            newProduct.Brand = fBrand;

            var imageUrls = _fileService.AddFile(productVm.Images, Path.Combine("img", "product-img"));

            newProduct.ProductImages = imageUrls.Select(imageUrl => new ProductImage
            {
                Image = new Image { Url = imageUrl }
            }).ToList();

            _context.Add(newProduct);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            Product? product = _context.Products.Include(p => p.Category)
                .Include(p => p.ProductImages).ThenInclude(p => p.Image)
                .FirstOrDefault(x => x.Id == id);

            List<Category> category = _context.Category.AsNoTracking().ToList();
            List<Brand> brands = _context.Brands.AsNoTracking().ToList();

            if (product is null) return NotFound();

            List<string> imageUrls = product.ProductImages.Select(pi => pi.Image.Url).ToList() ?? new List<string>();

            ProductEditVM editedModel = new()
            {
                Name = product.Name,
                BrandId = product.Brand.Id,
                Brands = brands,
                Categories = category,
                CategoryId = product.Category.Id,
                Price = product.Price,
                ColorId = product.ProductColors.FirstOrDefault()?.ColorId,
                ImageUrl = imageUrls,
            };
            return View(editedModel);
        }
    }
}
