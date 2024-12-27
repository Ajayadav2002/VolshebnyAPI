namespace Volshebny_API.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? CompanyName { get; set; }
        public string? EmailId { get; set; }
        public bool? IsGSTIN { get; set; }
        public string? GSTNumber { get; set; }
        public string? Address { get; set; }
        public string? Landmark { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string? Pincode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? SpType { get; set; }
    }
}
