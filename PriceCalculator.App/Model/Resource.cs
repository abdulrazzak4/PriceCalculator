
namespace PriceCalculator.App.Model
{
    public class Resource
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal BasePrice { get; set; }
    }
}
