namespace PriceCalculator.App.Model
{
    public class PriceRequestDTO
    {
        public List<int> ResourceIds { get; set; }    // List of Resource IDs selected by the user
        public Dictionary<int, int> Quantities { get; set; }  // Quantity for each Resource ID
    }
}
