using Amado.Entities;

namespace Amado.Areas.Admin.Models
{
    public class ProductDetailVM
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string? Desc { get; set; }
        public bool InStock { get; set; }
        public int? CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
        public int? BrandId { get; set; }
        public List<Brand>? Brands { get; set; }
        public int? ColorId { get; set; }
        public List<Color>? Colors { get; set; }
        public List<string> ImageUrl { get; set; }
    }
}
