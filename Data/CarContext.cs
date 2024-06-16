using Microsoft.EntityFrameworkCore;
using CarMicroserviceAPI.Models;

namespace MvcCars.Data
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
    }
}
