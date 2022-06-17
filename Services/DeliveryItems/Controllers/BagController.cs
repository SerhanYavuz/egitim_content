using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryItems;
using DeliveryItems.Models;

namespace DeliveryItems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Bag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BagModel>>> GetBags()
        {
          if (_context.Bags == null)
          {
              return NotFound();
          }
            return await _context.Bags.ToListAsync();
        }

        // GET: api/Bag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BagModel>> GetBagModel(Guid id)
        {
          if (_context.Bags == null)
          {
              return NotFound();
          }
            var bagModel = await _context.Bags.FindAsync(id);

            if (bagModel == null)
            {
                return NotFound();
            }

            return bagModel;
        }

        // PUT: api/Bag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBagModel(Guid id, BagModel bagModel)
        {
            if (id != bagModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(bagModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BagModelExists(id))
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

        // POST: api/Bag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BagModel>> PostBagModel(BagModel bagModel)
        {
          if (_context.Bags == null)
          {
              return Problem("Entity set 'AppDbContext.Bags'  is null.");
          }
            _context.Bags.Add(bagModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBagModel", new { id = bagModel.Id }, bagModel);
        }

        // DELETE: api/Bag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBagModel(Guid id)
        {
            if (_context.Bags == null)
            {
                return NotFound();
            }
            var bagModel = await _context.Bags.FindAsync(id);
            if (bagModel == null)
            {
                return NotFound();
            }

            _context.Bags.Remove(bagModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BagModelExists(Guid id)
        {
            return (_context.Bags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
