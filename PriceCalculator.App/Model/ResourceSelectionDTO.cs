namespace PriceCalculator.App.Model
{
    public class ResourceSelectionDTO
    {
        public List<int> ScopeIds { get; set; }
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public int Quantity { get; set; } = 1;
        public List<ScopeSelectionDTO> Scopes { get; set; } = new();
    }
}
