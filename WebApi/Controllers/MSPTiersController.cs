using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceCalculator.Api.Services;
using PriceCalculator.App.Data;
using PriceCalculator.App.Interfaces;
using PriceCalculator.App.Model;
using PriceCalculator.Data.Model;

namespace PriceCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MSPTiersController(IUnitOfWork unitOfWork, PriceCalculationService priceCalculation) : ControllerBase
    {
        [HttpPost("CalculatePrice")]
        public async Task<ActionResult<decimal>> CalculateTotalPrice([FromBody] PriceCalculationRequest request)
        {
            var totalPrice = await priceCalculation.CalculatePricesAsync(request);

            return Ok(totalPrice);
        }

        [HttpGet]
        public async Task<IEnumerable<MSPTier>> GetMSPTiers()
        {
            return await unitOfWork.MSPTiers.GetAllAsync();
        }

        // GET: api/MSPTiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MSPTier>> GetMSPTier(int id)
        {
            var mSPTier = await unitOfWork.MSPTiers.GetAsync(i => i.Id == id);

            if (mSPTier == null)
            {
                return NotFound();
            }

            return mSPTier;
        }

        // PUT: api/MSPTiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMSPTier(int id, MSPTier mSPTier)
        {
            if (id != mSPTier.Id)
            {
                return BadRequest();
            }
            await unitOfWork.MSPTiers.UpdateAsync(mSPTier);
            

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MSPTierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MSPTiers
        [HttpPost]
        public async Task<ActionResult<MSPTier>> PostMSPTier(MSPTier mSPTier)
        {
            var tier = await unitOfWork.MSPTiers.GetAsync(i => i.Name == mSPTier.Name);
            if (tier != null) return BadRequest(new { message = "MSPTier Already Exists." });

            await unitOfWork.MSPTiers.CreateAsync(mSPTier);
            await unitOfWork.SaveAsync();

            return CreatedAtAction("GetMSPTier", new { id = mSPTier.Id }, mSPTier);
        }

        // DELETE: api/MSPTiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMSPTier(int id)
        {
            var mSPTier = await unitOfWork.MSPTiers.GetAsync(i => i.Id == id);
            if (mSPTier == null)
            {
                return NotFound();
            }

            await unitOfWork.MSPTiers.DeleteAsync(mSPTier);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool MSPTierExists(int id)
        {
            var tier = unitOfWork.MSPTiers.GetAsync(i => i.Id == id);
            return tier != null;
        }
    }
}
