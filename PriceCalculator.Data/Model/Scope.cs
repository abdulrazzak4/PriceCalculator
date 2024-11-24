using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PriceCalculator.Data.Model
{
    public class Scope
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        [Precision(18, 2)]
        public decimal PriceModifier { get; set; }
        public int ResourceId { get; set; }
        [JsonIgnore] // Prevents back reference from being serialized
        public Resource? Resource { get; set; }
    }
}
