using Amado.Entities;

namespace Amado.Models
{
    public class BasketVM
    {
        public List<(BasketItem, Product)> Items { get; set; }

    }
}
