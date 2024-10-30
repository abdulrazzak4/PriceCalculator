namespace PriceCalculator.App.Model
{
    public class ResourcePrice
    {
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
        public int Quantity { get; set; }
        public decimal CalculatedPrice { get; set; }  // Price for the Resource after tier
    }
}
