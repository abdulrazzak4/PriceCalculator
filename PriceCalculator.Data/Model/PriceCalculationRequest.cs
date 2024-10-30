using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator.Data.Model
{
    public class PriceCalculationRequest
    {
        public List<ResourceSelection> ResourceSelections { get; set; }
        public int MSPTierId { get; set; }
    }
}
