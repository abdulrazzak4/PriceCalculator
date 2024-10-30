using Microsoft.EntityFrameworkCore;

namespace PriceCalculator.App.Model
{
    public class Resource
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Precision(18, 1)]
        public decimal BasePrice { get; set; }
    }
}
