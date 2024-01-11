using Amado.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Desc { get; set; }
        public bool InStock { get; set; }
        public int? CategoryId { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }
        public int? BrandId { get; set; }
        [ValidateNever]
        public List<Brand> Brands { get; set; }
        public int? ColorId { get; set; }
        [ValidateNever]
        public List<Color>? Colors { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
