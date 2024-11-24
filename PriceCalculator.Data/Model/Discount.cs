using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace PriceCalculator.Data.Model
{
    public class Discount
    {
        public int Id { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        [Precision(18, 2)]
        public decimal DiscountPercentage { get; set; }
        //[JsonIgnore] // Prevents back reference from being serialized
        public List<DiscountResource>? DiscountResources { get; set; }
    }
}
