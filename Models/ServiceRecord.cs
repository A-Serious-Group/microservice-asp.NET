using System.ComponentModel.DataAnnotations;

namespace CarMicroserviceAPI.Models
{
    public class ServiceRecord
    {
        [Key]
        public int Id { get; set; }
        public required int CarId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? Description { get; set; }
        
        public Car? Car { get; set; }
    }
}