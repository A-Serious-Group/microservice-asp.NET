using System.ComponentModel.DataAnnotations;

namespace CarMicroserviceAPI.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public required string  Name { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        
        public ICollection<Car>? Cars { get; set; }
    }
}