
using Microsoft.EntityFrameworkCore;

namespace PriceCalculator.Data.Model
{
    public class MSPTier
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [Precision(18, 2)]
        public decimal Percentage { get; set; }
        public int FloorPrice { get; set; }

        public bool IsActive { get; set; }
    }
}
