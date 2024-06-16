using Microsoft.EntityFrameworkCore;
using CarMicroserviceAPI.Models;
using MvcCars.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarMicroserviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRecordsController : ControllerBase
    {
        private readonly CarsContext _context;

        public ServiceRecordsController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/ServiceRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRecord>>> GetServiceRecords()
        {
            return await _context.ServiceRecords
                .Include(sr => sr.Car!)
                .ThenInclude(c => c.Owner)
                .ToListAsync();
        }

        // GET: api/ServiceRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRecord>> GetServiceRecord(int id)
        {
            var serviceRecord = await _context.ServiceRecords
                .Include(sr => sr.Car!)
                .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(sr => sr.Id == id);

            if (serviceRecord == null)
            {
                return NotFound();
            }

            return serviceRecord;
        }
        // POST: api/ServiceRecords
        [HttpPost]
        public async Task<ActionResult<ServiceRecord>> PostServiceRecord(ServiceRecord serviceRecord)
        {
            // Ensure the ServiceDate is set to UTC
            serviceRecord.ServiceDate = DateTime.SpecifyKind(serviceRecord.ServiceDate, DateTimeKind.Utc);

            if (serviceRecord.Car != null)
            {
                _context.Cars.Attach(serviceRecord.Car);

                if (serviceRecord.Car.Owner != null)
                {
                    _context.Owners.Attach(serviceRecord.Car.Owner);
                }
            }

            _context.ServiceRecords.Add(serviceRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceRecord", new { id = serviceRecord.Id }, serviceRecord);
        }

        // PUT: api/ServiceRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRecord(int id, ServiceRecord serviceRecord)
        {
            if (id != serviceRecord.Id)
            {
                return BadRequest();
            }

            // Ensure the ServiceDate is set to UTC
            serviceRecord.ServiceDate = DateTime.SpecifyKind(serviceRecord.ServiceDate, DateTimeKind.Utc);

            _context.Entry(serviceRecord).State = EntityState.Modified;

            if (serviceRecord.Car != null)
            {
                _context.Entry(serviceRecord.Car).State = EntityState.Modified;

                if (serviceRecord.Car.Owner != null)
                {
                    _context.Entry(serviceRecord.Car.Owner).State = EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(serviceRecord);
        }

        // DELETE: api/ServiceRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRecord(int id)
        {
            var serviceRecord = await _context.ServiceRecords.FindAsync(id);
            if (serviceRecord == null)
            {
                return NotFound();
            }

            _context.ServiceRecords.Remove(serviceRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceRecordExists(int id)
        {
            return _context.ServiceRecords.Any(e => e.Id == id);
        }
    }
}
