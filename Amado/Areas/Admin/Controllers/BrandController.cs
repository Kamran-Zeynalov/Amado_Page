﻿using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly AmadoDbContext _context;

        public BrandController(AmadoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Brand> brands = _context.Brands.AsEnumerable();
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Brand newBrand)
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
            bool isDuplicated = _context.Brands.Any(c => c.Name == newBrand.Name);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Brands.Add(newBrand);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Brand category = _context.Brands.FirstOrDefault(c => c.Id == id);
            if (category is null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Brand edited)
        {
            if (id != edited.Id) return BadRequest();
            Brand brand = _context.Brands.FirstOrDefault(c => c.Id == id);
            if (brand is null) return NotFound();
            bool duplicate = _context.Brands.Any(c => c.Name == edited.Name && edited.Name != brand.Name);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate brand name");
                return View(brand);
            }
            brand.Name = edited.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Brand category = _context.Brands.FirstOrDefault(c => c.Id == id);
            if (category is null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var brandToDelete = _context.Brands.FirstOrDefault(c => c.Id == id);

            if (brandToDelete == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brandToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
