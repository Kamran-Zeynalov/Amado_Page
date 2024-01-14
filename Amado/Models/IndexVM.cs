using Amado.Entities;

namespace Amado.Models
{
    public class IndexVM
    {
        public List<Product>? Products { get; set; }
        public Product Product { get; set; }

        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public string Order { get; set; }
    }
}
