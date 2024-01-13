using Amado.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amado.Areas.Admin.Models
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Desc { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
        public int BrandId { get; set; }
        public List<Brand>? Brands { get; set; }
        public int? ColorId { get; set; }
        public List<Color>? Colors { get; set; }
        [NotMapped]
        public List<int>? ImagesId { get; set; }
        [NotMapped]
        public List<Image>? AllImages { get; set; }
        [NotMapped]
        public List<IFormFile>? Images { get; set; }

        [NotMapped]
        public List<int>? DeletedImageIds { get; set; }
        //public List<string>? ImageUrl { get; set; }

    }
}
