using System.ComponentModel.DataAnnotations.Schema;

namespace Amado.Entities
{
    public class CheckOut
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Town { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string PhoneNum { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int CountryId { get; set; }
        public Country Country { get; set; }


        [NotMapped]
        public List<Country> Countries { get; set; }

    }
}
