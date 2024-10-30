
namespace PriceCalculator.App.Model
{
    public class MSPTier
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Percentage { get; set; }
        public bool IsActive { get; set; }
    }
}
