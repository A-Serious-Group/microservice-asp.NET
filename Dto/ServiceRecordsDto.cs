

public class ServiceRecordsDto
    {
        public int Id { get; set; }
        public required int CarId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? Description { get; set; }

}
