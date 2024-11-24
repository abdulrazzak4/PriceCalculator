namespace PriceCalculator.App.Model
{
    public class DiscountDTO
    {
        public int Id { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        public List<DiscountResourceDTO>? DiscountResources { get; set; }

        public List<ResourceDTO> Resources { get; set; } = new List<ResourceDTO>();
        public List<int>? ResourceIds { get; set; }
    }

}
