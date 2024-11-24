using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceCalculator.Data.Data;
using PriceCalculator.Data.Interfaces;
using PriceCalculator.Data.Model;
using System;

namespace PriceCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ScopesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all scopes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scope>>> GetScopes()
        {
            return await _context.Scopes.ToListAsync();
        }

        // Get a specific scope by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Scope>> GetScope(int id)
        {
            var scope = await _context.Scopes.FirstOrDefaultAsync(q => q.Id == id);

            if (scope == null)
                return NotFound();

            return scope;
        }

        // Create a new scope
        [HttpPost]
        public async Task<ActionResult<Scope>> CreateScope(Scope scope)
        {
            _context.Scopes.Add(scope);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetScope), new { id = scope.Id }, scope);
        }

        // Update an existing scope
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScope(int id, Scope scope)
        {
            if (id != scope.Id)
                return BadRequest();

            _context.Entry(scope).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScopeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // Delete a scope
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScope(int id)
        {
            var scope = await _context.Scopes.FirstOrDefaultAsync(q => q.Id == id);
            if (scope == null)
                return NotFound();

            _context.Scopes.Remove(scope);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScopeExists(int id) => _context.Scopes.Any(q => q.Id == id);
    }
}


