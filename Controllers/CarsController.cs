using Microsoft.EntityFrameworkCore;
using CarMicroserviceAPI.Models;
using MvcCars.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarMicroserviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsContext _context;

        public CarsController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Include(c => c.Owner).ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Owner)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var serviceRecords = await _context.ServiceRecords
                   .Where(sr => sr.CarId == car.Id)
                   .ToListAsync();

            var serviceRecordsDto = serviceRecords?.Select(sr => new ServiceRecordsDto
            {
                Id = sr.Id,
                CarId = sr.CarId,
                Description = sr.Description,
                ServiceDate = sr.ServiceDate
            }).ToList();

            var carDto = new CarDto
            {
                Id = car.Id,
                NameCar = car.NameCar,
                ModelCar = car.ModelCar,
                Year = car.Year,
                ConstructionCompany = car.ConstructionCompany,
                Owner = car.Owner != null ? new OwnerDto
                {
                    Id = car.Owner.Id,
                    Name = car.Owner.Name,
                    Address = car.Owner.Address,
                    PhoneNumber = car.Owner.PhoneNumber
                } : null,
                ServiceRecords = serviceRecordsDto
            };

            return Ok(carDto);
        }



        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            if (car.Owner != null)
            {
                _context.Entry(car.Owner).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(car);
        }


        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (car.Owner != null)
            {
                _context.Owners.Attach(car.Owner);
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }


        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
