using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryPoints;
using DeliveryPoints.Models;

namespace DeliveryPoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPointController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeliveryPointController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryPoint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryPointModel>>> GetDeliveryPoints()
        {
          if (_context.DeliveryPoints == null)
          {
              return NotFound();
          }
            return await _context.DeliveryPoints.Where(d => d.IsDeleted == false).ToListAsync();
        }

        // GET: api/DeliveryPoint/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryPointModel>> GetDeliveryPointModel(Guid id)
        {
          if (_context.DeliveryPoints == null)
          {
              return NotFound();
          }
            var deliveryPointModel = await _context.DeliveryPoints.FindAsync(id);

            if (deliveryPointModel == null)
            {
                return NotFound();
            }

            return deliveryPointModel;
        }

        // PUT: api/DeliveryPoint/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryPointModel(Guid id, DeliveryPointModel deliveryPointModel)
        {
            if (id != deliveryPointModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(deliveryPointModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryPointModelExists(id))
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

        // POST: api/DeliveryPoint
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeliveryPointModel>> PostDeliveryPointModel(DeliveryPointModel deliveryPointModel)
        {
          if (_context.DeliveryPoints == null)
          {
              return Problem("Entity set 'AppDbContext.DeliveryPoints'  is null.");
          }
          deliveryPointModel.CreatedAt = DateTime.UtcNow;
          deliveryPointModel.IsDeleted = false;
            _context.DeliveryPoints.Add(deliveryPointModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryPointModel", new { id = deliveryPointModel.Id }, deliveryPointModel);
        }

        // DELETE: api/DeliveryPoint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryPointModel(Guid id)
        {
            if (_context.DeliveryPoints == null)
            {
                return NotFound();
            }
            var deliveryPointModel = await _context.DeliveryPoints.FindAsync(id);
            if (deliveryPointModel == null)
            {
                return NotFound();
            }
            deliveryPointModel.IsDeleted = true;
            _context.DeliveryPoints.Update(deliveryPointModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryPointModelExists(Guid id)
        {
            return (_context.DeliveryPoints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
