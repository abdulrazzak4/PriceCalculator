namespace PriceCalculator.App.Model
{
    public class Price
    {
        public int Id { get; set; }
        public List<ResourcePrice> ResourcePrices { get; set; } = new List<ResourcePrice>();
        public decimal TotalPrice { get; set; }
    }
}
