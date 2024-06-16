public class CarDto
{
    public int Id { get; set; }
    public string? NameCar { get; set; }
    public string? ModelCar { get; set; }
    public int Year { get; set; }
    public string? ConstructionCompany { get; set; }
    public OwnerDto? Owner { get; set; }
    // public ServiceRecordsDto? ServiceRecords {get; set;}
    public List<ServiceRecordsDto>? ServiceRecords { get; set; }
}