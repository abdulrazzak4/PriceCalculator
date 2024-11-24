namespace PriceCalculator.App.Model
{
    public class PriceCalculationRequestDTO
    {
        public List<ResourceSelectionDTO> ResourceSelections { get; set; }
        public int MSPTierId { get; set; }
    }
}
