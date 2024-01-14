using Amado.Entities;

namespace Amado.Models
{
    public class CheckOutVM
    {
        public CheckOut CheckOut{ get; set; }
        public List<Country>? Countries { get; set; }
    }
}
