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
    public class PackageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PackageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Package
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageModel>>> GetPackages()
        {
          if (_context.Packages == null)
          {
              return NotFound();
          }
            return await _context.Packages.ToListAsync();
        }

        // GET: api/Package/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageModel>> GetPackageModel(Guid id)
        {
          if (_context.Packages == null)
          {
              return NotFound();
          }
            var packageModel = await _context.Packages.FindAsync(id);

            if (packageModel == null)
            {
                return NotFound();
            }

            return packageModel;
        }

        // PUT: api/Package/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageModel(Guid id, PackageModel packageModel)
        {
            if (id != packageModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(packageModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageModelExists(id))
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

        // POST: api/Package
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PackageModel>> PostPackageModel(PackageModel packageModel)
        {
          if (_context.Packages == null)
          {
              return Problem("Entity set 'AppDbContext.Packages'  is null.");
          }
            _context.Packages.Add(packageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackageModel", new { id = packageModel.Id }, packageModel);
        }

        // DELETE: api/Package/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageModel(Guid id)
        {
            if (_context.Packages == null)
            {
                return NotFound();
            }
            var packageModel = await _context.Packages.FindAsync(id);
            if (packageModel == null)
            {
                return NotFound();
            }

            _context.Packages.Remove(packageModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageModelExists(Guid id)
        {
            return (_context.Packages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
