using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator.Data.Model
{
    public class DiscountVM
    {
        public int Id { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        //public List<ResourceDTO> Resources { get; set; } = new List<ResourceDTO>();
        public List<int>? ResourceIds { get; set; }
    }
}
