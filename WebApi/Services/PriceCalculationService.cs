using Microsoft.EntityFrameworkCore;
using PriceCalculator.Data.Data;
using PriceCalculator.Data.Model;
using PriceCalculator.Data.Repositories;

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
            // Retrieve the MSPTier using the provided MSPTierId
            var msptier = await _context.MSPTiers.FindAsync(request.MSPTierId);

            if (msptier == null)
            {
                return 0;
            }

            decimal totalPrice = 0;
            decimal totalScopePriceModifier = 0;
            decimal totalPriceWithDiscount = 0;

            foreach (var resourceSelection in request.ResourceSelections)
            {
                // Retrieve the resource and its associated discounts
                var resource = await _context.Resources.Include(dr => dr.DiscountResources)
                                                         .ThenInclude(d => d.Discount)
                                                         .FirstOrDefaultAsync(r => r.Id == resourceSelection.ResourceId);

                // Initialize the discount to 0 if not found
                decimal resourceDiscountPercentage = 0;
                if (resource != null && resource.DiscountResources != null && resource.DiscountResources.Count > 0)
                {
                    // Loop through the discounts for this resource
                    foreach (var discountResource in resource.DiscountResources)
                    {
                        // Check if the resource quantity is within the discount's MinQuantity and MaxQuantity
                        if (resourceSelection.Quantity >= discountResource.Discount.MinQuantity &&
                            resourceSelection.Quantity <= discountResource.Discount.MaxQuantity)
                        {
                            // If the quantity is within the discount's range, apply the discount percentage
                            resourceDiscountPercentage = discountResource.Discount.DiscountPercentage;
                            break; // Apply the first valid discount found (or adjust this logic for multiple discounts)
                        }
                    }
                }


                foreach (var scopeId in resourceSelection.ScopeIds)
                {
                    var scope = await _context.Scopes.FindAsync(scopeId);
                    if (scope != null)
                    {
                        // Calculate the scope's price modifier
                        decimal scopePriceModifier = scope.PriceModifier;
                        totalScopePriceModifier += scopePriceModifier;
                    }
                }

                if (resource != null)
                {
                    // Calculate the base price for the resource
                    var basePrice = resource.BasePrice * msptier.Percentage;

                    // Calculate the price with scope modifiers
                    var priceWithModifiers = basePrice + (basePrice * totalScopePriceModifier);

                    // Calculate the total price for the resource selection
                    var resourceTotalPrice = priceWithModifiers * resourceSelection.Quantity;

                    // Add to the total price (before discount)
                    totalPrice += resourceTotalPrice;

                    // Apply the discount if applicable, and calculate the price with discount for this resource
                    decimal resourceTotalPriceWithDiscount = resourceTotalPrice;

                    if (resourceDiscountPercentage > 0)
                    {
                        // Apply the discount to the resource total price if the discount percentage is greater than 0
                        resourceTotalPriceWithDiscount = resourceTotalPrice * (1 - resourceDiscountPercentage / 100);
                    }

                    // Add to the total price with discount
                    totalPriceWithDiscount += resourceTotalPriceWithDiscount;
                }
            }

            // Return either the price with discount, or the MSP tier floor price if applicable
            if (totalPriceWithDiscount > msptier.FloorPrice)
            {
                return decimal.Round(totalPriceWithDiscount, 2);
            }
            else
            {
                return msptier.FloorPrice;
            }
        }



        //public async Task<decimal> CalculatePricesAsync(PriceCalculationRequest request)
        //{
        //    //var msptier = await _unitOfWork.MSPTiers.GetAsync(m => m.Id == request.MSPTierId && m.IsActive);
        //    var msptier = await _context.MSPTiers.FindAsync(request.MSPTierId);


        //    if (msptier == null)
        //    {
        //        return 0;
        //    }
        //    decimal totalPrice = 0;
        //    decimal totalScopePriceModifier = 0;
        //    decimal totalPriceWithDiscount = 0;

        //    foreach (var resourceSelection in request.ResourceSelections)
        //    {
        //      // var resource = await _unitOfWork.Resources.GetAsync(i => i.Id == resourceSelection.ResourceId);
        //        var resource = await _context.Resources.Include(dr => dr.DiscountResources).ThenInclude(d => d.Discount).FindAsync(resourceSelection.ResourceId);
        //        foreach (var scopeId in resourceSelection.ScopeIds) {
        //            var scope = await _context.Scopes.FindAsync(scopeId);
        //            var scopePriceModifier = scope.PriceModifier;
        //            totalScopePriceModifier += scopePriceModifier;
        //        }
        //        if (resource != null)
        //        {
        //            var resourcePrice = (resource.BasePrice * msptier.Percentage);
        //            totalPrice +=(( resourcePrice + ( resourcePrice * totalScopePriceModifier))* resourceSelection.Quantity);
        //        }
        //        totalPriceWithDiscount += totalPriceWithDiscount;
        //    }
        //    if (totalPrice > msptier.FloorPrice)
        //    {
        //        return decimal.Round(totalPrice, 2);
        //    }
        //    else
        //    {
        //        return msptier.FloorPrice;
        //    }
        //}

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

