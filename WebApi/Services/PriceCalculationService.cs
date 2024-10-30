using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PriceCalculator.App.Data;
using PriceCalculator.App.Interfaces;
using PriceCalculator.App.Model;
using PriceCalculator.App.Repositories;
using PriceCalculator.Data.Model;

namespace PriceCalculator.Api.Services
{
    public class PriceCalculationService
    {
        //private readonly UnitOfWork _unitOfWork;
        public ApplicationDbContext _context { get; set; }

        public PriceCalculationService(/*UnitOfWork unitOfWork,*/ApplicationDbContext context)
        {
            _context = context;
            //_unitOfWork = unitOfWork;
        }
        public async Task<decimal> CalculatePricesAsync(PriceCalculationRequest request)
        {
            //var msptier = await _unitOfWork.MSPTiers.GetAsync(m => m.Id == request.MSPTierId && m.IsActive);
            var msptier = await _context.MSPTiers.FindAsync(request.MSPTierId);


            if (msptier == null)
            {
                return 0;
            }
            decimal totalPrice = 0;

            foreach (var resourceSelection in request.ResourceSelections)
            {
               // var resource = await _unitOfWork.Resources.GetAsync(i => i.Id == resourceSelection.ResourceId);
                var resource = await _context.Resources.FindAsync(resourceSelection.ResourceId);

                if (resource != null)
                {
                    var resourcePrice = (resource.BasePrice * msptier.Percentage);
                    totalPrice += resourcePrice * resourceSelection.Quantity;
                }
            }

            return  decimal.Round(totalPrice,2);
        }
        
            //public List<ResourcePrice> CalculatePrices(PriceCalculationRequest request)
            //{
            //    List<ResourcePrice> calculatedPrices = new();
            
            //    // Get the active global discount
            //    var globalDiscount = _context.MSPTiers.FirstOrDefault(m=> m.Id==request.MSPTierId);
            //    decimal discountValue = globalDiscount != null ? globalDiscount.Percentage : 0m;

            //    foreach (var resource in request.ResourceSelections)
            //    {
            //        // Get the quantity for the resource from the quantities dictionary
            //        int quantity = resource.Quantity.ContainsKey(resource.Id) ? quantities[resource.Id] : 1;
            //    var resourcePrice = _context.Resources.Select(p=>p.BasePrice).FindAsync(res.ResourceId);
            //    // Calculate price for the resource based on the discount and quantity
            //    decimal calculatedPrice = resourcePrice * (discountValue) * quantity;

            //        calculatedPrices.Add(new ResourcePrice
            //        {
            //            ResourceId = resource.Id,
            //            Resource = resource,
            //            Quantity = quantity,
            //            CalculatedPrice = calculatedPrice
            //        });
            //    }

            //    return calculatedPrices;
            //}

            //public decimal CalculateTotalPrice(List<ProductPrice> resourcePrices)
            //{
            //    return resourcePrices.Sum(p => p.CalculatedPrice);
            //}
        

    }

}

