namespace Amado.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
    }
}
