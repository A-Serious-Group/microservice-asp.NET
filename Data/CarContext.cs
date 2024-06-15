using CarMicroserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcCars.Data
{
    public class CarsContext : DbContext
    {
        public CarsContext (DbContextOptions<CarsContext> options)
            : base(options)
        {
        }

        public DbSet<CarMicroserviceAPI.Models.Car> Cars { get; set; } = default!;
        public DbSet<Owner> Owners { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
    }
}
