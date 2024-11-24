using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PriceCalculator.Data.Model
{
    public class DiscountResource
    {
        public int DiscountId { get; set; }
        [JsonIgnore]
        public Discount? Discount { get; set; }

        public int ResourceId { get; set; }
        [JsonIgnore]
        public Resource? Resource { get; set; }
    }
}
