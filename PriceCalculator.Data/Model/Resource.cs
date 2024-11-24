using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace PriceCalculator.Data.Model
{
    public class Resource
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Precision(18, 2)]
        public decimal BasePrice { get; set; }

        public List<Scope>? Scopes { get; set; }
        //[JsonIgnore] // Prevents back reference from being serialized
        public List<DiscountResource>? DiscountResources { get; set; }

    }
}
