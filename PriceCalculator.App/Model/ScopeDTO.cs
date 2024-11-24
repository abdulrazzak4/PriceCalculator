namespace PriceCalculator.App.Model
{
    public class ScopeDTO
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public decimal PriceModifier { get; set; }
        public int ResourceId { get; set; }
        public ResourceDTO? Resource { get; set; }
    }
}
