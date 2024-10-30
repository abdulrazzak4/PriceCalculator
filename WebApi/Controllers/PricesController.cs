using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Api.Services;
using PriceCalculator.App.Data;
using PriceCalculator.App.Interfaces;
using PriceCalculator.App.Model;
using PriceCalculator.Data.Model;
using System.Linq;
using System.Text;

namespace PriceCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
            private readonly PriceCalculationService _priceService;
            private readonly ApplicationDbContext _context;
            private readonly IPdfService _pdfService;
        private readonly IUnitOfWork _unitOfWork;

        public PricesController(PriceCalculationService priceService, ApplicationDbContext context, IPdfService pdfService, IUnitOfWork unitOfWork)
            {
                _priceService = priceService;
                _context = context;
                _pdfService = pdfService;
            _unitOfWork = unitOfWork;
        }

        //[HttpPost("generate-pdf")]
        //public async Task<IActionResult> GeneratePdfReport([FromBody] PriceCalculationRequest request)
        //{request.
        //    //var resource = _context.Resources.Where(p => request.ResourceIds.Contains(p.Id)).ToList();
        //    //var resource =await _unitOfWork.Resources.GetAsync(p => p.Id== request.ResourceSelections.Contains());
        //    //if (!resource.Any()) return NotFound("resource not found.");

        //    // Calculate prices with quantities
        //    //var resourcePrices = _priceService.CalculatePrices(request);
        //    var totalPrice = _priceService.CalculatePricesAsync(request);

        //    // Generate HTML for the report
        //    var htmlContent = GenerateHtmlReport(r/*esourcePrices,*/ totalPrice);

        //    // Generate PDF from HTML
        //     var pdf = _pdfService.GeneratePdf(htmlContent);

        //    return File(pdf, "application/pdf", "PriceReport.pdf");
        //    //return Ok();
        //}

        //private string GenerateHtmlReport(/*List<ResourcePrice> resourcePrices,*/ decimal totalPrice)
        //{
        //    var sb = new StringBuilder();
        //    sb.Append("<h1>Resource Price Report</h1>");
        //    sb.Append("<table border='1'>");
        //    sb.Append("<tr><th>Resource</th><th>Quantity</th><th>Price</th></tr>");

        //    foreach (var ResourcePrice in resourcePrices)
        //    {
        //        sb.Append($"<tr><td>{ResourcePrice.Resource.Name}</td><td>{ResourcePrice.Quantity}</td><td>{ResourcePrice.CalculatedPrice:C}</td></tr>");
        //    }

        //    sb.Append("</table>");
        //    sb.Append($"<h2>Total Price: {totalPrice:C}</h2>");

        //    return sb.ToString();
        //}


    }
}
