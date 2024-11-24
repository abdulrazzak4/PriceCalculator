namespace PriceCalculator.App.Model
{
    public class ScopeSelectionDTO
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}
