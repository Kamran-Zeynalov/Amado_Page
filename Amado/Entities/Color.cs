namespace Amado.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }

        public List<ProductColor>? PorductColors { get; set; }
    }
}
