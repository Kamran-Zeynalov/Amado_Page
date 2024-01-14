namespace Amado.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CheckOut> CheckOuts { get; set; }
    }
}
