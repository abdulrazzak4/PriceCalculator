namespace PriceCalculator.Data.Model
{
    public class ResourceSelection
    {
        public int ResourceId { get; set; }
        public List<int> ScopeIds { get; set; }
        public int Quantity { get; set; }

    }
}
