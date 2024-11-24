using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceCalculator.Data.Interfaces;
using PriceCalculator.Data.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PriceCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController(IUnitOfWork unitOfWork) : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<Discount>> GetDiscounts()
        {
            var discounts = await unitOfWork.Discounts.GetAllAsync(includeProperties: "DiscountResources,DiscountResources.Resource");
            return discounts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(int id)
        {
            var discountRule = await unitOfWork.Discounts.GetAsync(i => i.Id == id, includeProperties: "DiscountResources,DiscountResources.Resource");

            if (discountRule == null)
            {
                return NotFound();
            }

            return discountRule;
        }

        [HttpPost]
        public async Task<ActionResult<Discount>> CreateDiscount(DiscountVM discountRule)
        {
            // Create a new Discount entity from the view model
            var discount = new Discount
            {
                MaxQuantity = discountRule.MaxQuantity,
                MinQuantity = discountRule.MinQuantity,
                DiscountPercentage = discountRule.DiscountPercentage
            };

            // Save the Discount entity to the database
            await unitOfWork.Discounts.CreateAsync(discount);
            await unitOfWork.SaveAsync(); // Ensure that the discount is saved first to generate its Id

            // Add Discount Resources
            foreach (var resourceId in discountRule.ResourceIds)
            {
                var newDiscountResources = new DiscountResource()
                {
                    DiscountId = discount.Id,  // Use the Id of the saved discount
                    ResourceId = resourceId
                };
                await unitOfWork.DiscountResources.CreateAsync(newDiscountResources);
            }

            // Commit all changes
            await unitOfWork.SaveAsync();

            // Return the created Discount entity
            return CreatedAtAction(nameof(GetDiscount), new { id = discount.Id }, discount);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, DiscountVM discountRule)
        {
            if (id != discountRule.Id)
            {
                return BadRequest();
            }

            // Retrieve the discount entity to update
            var discount = await unitOfWork.Discounts.GetAsync(i => i.Id == discountRule.Id);

            if (discount == null)
            {
                return NotFound();
            }

            // Update the properties of the Discount entity
            discount.MaxQuantity = discountRule.MaxQuantity;
            discount.MinQuantity = discountRule.MinQuantity;
            discount.DiscountPercentage = discountRule.DiscountPercentage;

            // Save the updated Discount entity to the database
            await unitOfWork.Discounts.UpdateAsync(discount);

            try
            {
                // Commit changes for Discount updates
                await unitOfWork.SaveAsync();

                // Get existing Discount Resources from the database (ensure this is a collection, like IEnumerable<DiscountResource>)
                var existingResources = await unitOfWork.DiscountResources.GetAllAsync(n => n.DiscountId == discountRule.Id);

                // Ensure existingResources is treated as a collection, even if it's a single object
                if (existingResources == null)
                {
                    existingResources = new List<DiscountResource>(); // Initialize as empty if null
                }
                else
                {
                    // If it's a single object, wrap it into a collection
                    existingResources = (IEnumerable<DiscountResource>)(new[] { existingResources }); // This wraps the single object into an array
                }

                // Remove Discount Resources that are no longer in the ResourceIds list
                var resourcesToRemove = new List<DiscountResource>();

                // Loop to find resources to remove
                foreach (var resource in existingResources)
                {
                    bool shouldRemove = true;

                    // Check if the resourceId is in the discountRule.ResourceIds list
                    foreach (var resourceId in discountRule.ResourceIds)
                    {
                        if (resource.ResourceId == resourceId)
                        {
                            shouldRemove = false;
                            break; // If found, no need to remove this resource
                        }
                    }

                    if (shouldRemove)
                    {
                        resourcesToRemove.Add(resource);
                    }
                }

                // Delete resources that are no longer needed
                if (resourcesToRemove.Count > 0) // Check if there are any resources to remove
                {
                    foreach (var resource in resourcesToRemove)
                    {
                        await unitOfWork.DiscountResources.DeleteAsync(resource); // Assuming DeleteAsync accepts a single resource
                    }
                    await unitOfWork.DiscountResources.SaveAsync();
                }


                // Add new Discount Resources
                foreach (var resourceId in discountRule.ResourceIds)
                {
                    bool resourceExists = false;

                    // Check if the resource already exists in the existingResources
                    foreach (var existingResource in existingResources)
                    {
                        if (existingResource.ResourceId == resourceId)
                        {
                            resourceExists = true;
                            break; // If found, no need to add this resource
                        }
                    }

                    // If the resource does not exist, add it
                    if (!resourceExists)
                    {
                        var newDiscountResource = new DiscountResource()
                        {
                            DiscountId = discount.Id,  // Use the Id of the saved discount
                            ResourceId = resourceId
                        };
                        await unitOfWork.DiscountResources.CreateAsync(newDiscountResource);
                    }
                }

                // Commit all changes to DiscountResources
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues (e.g., record was modified by another user)
                if (!DiscountExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent(); // Successfully updated
        }

        private bool DiscountExists(int id)
        {
            var discount = unitOfWork.Discounts.GetAsync(e => e.Id == id);
            return discount != null;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discountRule = await unitOfWork.Discounts.GetAsync(i => i.Id == id);
            if (discountRule == null)
            {
                return NotFound();
            }

            unitOfWork.Discounts.DeleteAsync(discountRule);
            await unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
