using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator.App.Model
{
    public class PriceRequest
    {
        public List<int> ResourceIds { get; set; }    // List of Resource IDs selected by the user
       public Dictionary<int, int>? Quantities { get; set; }  // Quantity for each Resource ID
        public int MSPTierId { get; set; }
    }
}
