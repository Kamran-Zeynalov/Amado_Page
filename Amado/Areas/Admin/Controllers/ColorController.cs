using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ColorController : Controller
    {
        private readonly AmadoDbContext _context;

        public ColorController(AmadoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Color> colors = _context.Colors.AsEnumerable();
            return View(colors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Color newColor)
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
            bool isDuplicated = _context.Colors.Any(c => c.Name == newColor.Name);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            bool isValueDuplicated = _context.Colors.Any(c => c.Value == newColor.Value);
            if (isValueDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Colors.Add(newColor);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Color colors = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (colors is null) return NotFound();
            return View(colors);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Color edited)
        {
            if (id != edited.Id) return BadRequest();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            bool duplicate = _context.Colors.Any(c => c.Name == edited.Name && c.Id != id);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate color name");
                return View(color);
            }
            bool isValueDuplicate = _context.Colors.Any(c => c.Value == edited.Value && c.Id != id);
            if (isValueDuplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate color value");
                return View(color);
            }
            color.Name = edited.Name;
            color.Value = edited.Value;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            return View(color);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Color brandToDelete = _context.Colors.FirstOrDefault(c => c.Id == id);

            if (brandToDelete == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(brandToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
