namespace Volshebny_API.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }
        public string? DestinationName { get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public decimal TicketPrice { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? SpType { get; set; }
    }   

}
