using System.ComponentModel.DataAnnotations;

namespace CarMicroserviceAPI.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string? NameCar { get; set; }
        public string? ModelCar { get; set; }
        public int Year { get; set; }
        public string? ConstructionCompany { get; set; }
    }
}
