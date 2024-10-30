using System.ComponentModel.DataAnnotations;

namespace PriceCalculator.App.Model
{
    public class ResourceDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "BasePrice is required")]
        public decimal BasePrice { get; set; }
    }
}
