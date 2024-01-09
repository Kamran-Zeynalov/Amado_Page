namespace Amado.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Desc { get; set; }
        public bool InStock { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<ProductColor>? ProductColors { get; set; }
    }
}
