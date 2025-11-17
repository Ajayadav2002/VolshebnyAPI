namespace Volshebny_API.Models
{
    public class UnconfirmedLead
    {
		public int UnconfirmedId { get; set; }
		public int leadId { get; set; }
		public string? TourCountry { get; set; }
		public string? Status { get; set; }

		public string? Destinations { get; set; }
		public int? Total_Travellers { get; set; } 
		public DateTime? Start_date { get; set; }
		public DateTime? End_date { get; set; }
		public Decimal? Package_charge { get; set; }
		public int CreatedBy { get; set; }

		public int? ModifiedBy { get; set; }

		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public bool IsActive { get; set; }

		public bool IsDelete { get; set; }
		public string? SpType { get; set; }
	}
}
