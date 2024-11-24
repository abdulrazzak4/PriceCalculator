using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Api.Services;
using PriceCalculator.Data.Model;

namespace PriceCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController(PriceCalculationService priceCalculation) : ControllerBase
    {
        [HttpPost("CalculatePrice")]
        public async Task<ActionResult<decimal>> CalculateTotalPrice([FromBody] PriceCalculationRequest request)
        {
            var totalPrice = await priceCalculation.CalculatePricesAsync(request);

            return Ok(totalPrice);
        }
    }
}
