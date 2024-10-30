using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceCalculator.App.Data;
using PriceCalculator.App.Interfaces;
using PriceCalculator.App.Model;

namespace PriceCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController(IUnitOfWork unitOfWork) : ControllerBase
    {
        // GET: api/Resources
        [HttpGet]
        public async Task<IEnumerable<Resource>> GetResources()
        {
            return await unitOfWork.Resources.GetAllAsync();
        }

        // GET: api/Resources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
        {
            var resource = await unitOfWork.Resources.GetAsync(i=> i.Id==id);

            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // PUT: api/Resources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(int id, Resource resource)
        {
            if (id != resource.Id)
            {
                return BadRequest();
            }

            await unitOfWork.Resources.UpdateAsync(resource);

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
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

        // POST: api/Resources
        [HttpPost]
        public async Task<ActionResult<Resource>> PostResource(Resource resource)
        {
            var Resource = await unitOfWork.Resources.GetAsync(i => i.Name == resource.Name);
            if (Resource != null) return BadRequest(new { message = "Resource Already Exists." });

            await unitOfWork.Resources.CreateAsync(resource);
            await unitOfWork.SaveAsync();

            return CreatedAtAction("GetResource", new { id = resource.Id }, resource);
        }

        // DELETE: api/Resources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var resource = await unitOfWork.Resources.GetAsync(i => i.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            await unitOfWork.Resources.DeleteAsync(resource);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool ResourceExists(int id)
        {
            var resource = unitOfWork.Resources.GetAsync(i => i.Id == id);
            return resource !=null;
        }
    }
}
