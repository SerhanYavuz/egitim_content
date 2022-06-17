using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle;
using Vehicle.Models;

namespace Vehicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VehicleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehicles()
        {
          if (_context.Vehicles == null)
          {
              return NotFound();
          }
            return await _context.Vehicles.Where(v => v.IsDeleted == false).ToListAsync();
        }

        // GET: api/Vehicle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleModel(Guid id)
        {
          if (_context.Vehicles == null)
          {
              return NotFound();
          }
            var vehicleModel = await _context.Vehicles.FindAsync(id);

            if (vehicleModel == null || vehicleModel.IsDeleted)
            {
                return NotFound();
            }

            return vehicleModel;
        }

        // PUT: api/Vehicle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleModel(Guid id, VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return BadRequest("araç id'sini değiştiremezsiniz.");
            }
            vehicleModel.LicencePlate.Replace(" ", String.Empty).ToLower();
            if(LicencePlateExists(vehicleModel.LicencePlate)){
                return BadRequest("araç daha önce kaydedilmiş");
            }
            _context.Entry(vehicleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleModelExists(id))
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

        // POST: api/Vehicle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleModel>> PostVehicleModel(VehicleModel vehicleModel)
        {
          if (_context.Vehicles == null)
          {
              return Problem("Entity set 'AppDbContext.Vehicles'  is null.");
          }
          if(string.IsNullOrEmpty(vehicleModel.LicencePlate)){
              return Problem("Plaka alanı zorunludur");
          }
          //TODO validate licence plate
          if(LicencePlateExists(vehicleModel.LicencePlate)){
              return BadRequest("araç daha önce kaydedilmiş");
          }
            vehicleModel.LicencePlate.Replace(" ", String.Empty).ToLower();
            vehicleModel.IsDeleted = false;
            vehicleModel.CreatedAt= DateTime.UtcNow;
            _context.Vehicles.Add(vehicleModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleModel", new { id = vehicleModel.Id }, vehicleModel);
        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleModel(Guid id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            var vehicleModel = await _context.Vehicles.FindAsync(id);
            if (vehicleModel == null || vehicleModel.IsDeleted)
            {
                return NotFound();
            }
            vehicleModel.IsDeleted = true;
            _context.Vehicles.Update(vehicleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleModelExists(Guid id)
        {
            return (_context.Vehicles?.Any(e => e.Id == id&& e.IsDeleted == false)).GetValueOrDefault();
        }
        private bool LicencePlateExists(string licencePlate)
        {
            return (_context.Vehicles?.Any(e => (e.LicencePlate == licencePlate) && e.IsDeleted == false)).GetValueOrDefault();
        }

    }
}
