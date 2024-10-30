using System.ComponentModel.DataAnnotations;

namespace PriceCalculator.App.Model
{
    public class MSPTierDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Percentage is required")]
        public decimal Percentage { get; set; }
        public bool IsActive { get; set; }
    }
}
