namespace Volshebny_API.Models
{

    public class LeadDetailsViewModel
    {
        public LeadViewModels Lead { get; set; }
        public List<Destinationlead> Destinations { get; set; }
        public List<Traveller> Travellers { get; set; }
        public List<Transportations> Transportations { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Miscellaneouss> MiscellaneousItems { get; set; }
    }

    public class LeadMaster
	{
		public int leadId { get; set; }
		public string? TourID { get; set; }
		public int? ClientId { get; set; }
		public string? OriginCountry { get; set; }
		public string? OriginCity { get; set; }
		public string? DestinCountry { get; set; }
		public string? DestinCity { get; set; }
		public int? TotalPassengers { get; set; }
		public int? TourDays { get; set; }
		public DateTime? DepartureDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public string? AccommodationType { get; set; }
		public string? SpecialNotes { get; set; }
		public bool TravelInsurance { get; set; }
		public string? TourDestination { get; set; }
		public string? TourType { get; set; }
		public DateTime? CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
		public int? TotalCharges { get; set; }
		public int? ChargesPerPerson { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Gender { get; set; }
		public int Age { get; set; }
		public string? ContactNo { get; set; }
		public string? Email_Id { get; set; }
		public string? Address { get; set; }
		public string? Nationality { get; set; }
		public string? PassportNumber { get; set; }
		public string? PassportAttachment { get; set; }
		public string? VisaStatus { get; set; }
		public string? VisaType { get; set; }
		public string? EmergencyContactName { get; set; }
		public string? EmergencyContactPhone { get; set; }
		public string? FromCountry { get; set; }
		public string? FromCity { get; set; }
		public string? ToCoutry { get; set; }
		public string? ToCity { get; set; }
		public string? TravelModel { get; set; }
		public string? PartnerName { get; set; }
		public DateTime Departure { get; set; }
		public DateTime ArrivalDate { get; set; }
		public string? FaxNumber { get; set; }
		public DateTime DepartureTime { get; set; }
		public string? HotelCountry { get; set; }
		public string? HotelCity { get; set; }
		public string? HotelName { get; set; }
		public int? ForPeople { get; set; }
		public int? ForNights { get; set; }
		public bool? BreakFast { get; set; }
		public string? MealPreference { get; set; }
		public string? Activity { get; set; }
		public string? ActivityCountry { get; set; }
		public string? ActivityCity { get; set; }
		public bool Applicable { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifiedBy { get; set; }
		public bool? IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string? SpType { get; set; }
	}
	public class AddViewModel
	{
		//for insert data
		public List<LeadViewModel> LeadsList { get; set; } = new List<LeadViewModel>();
		public List<Destinationlead> DestinationList { get; set; } = new List<Destinationlead>();
		public List<Travellers> TravellersList { get; set; } = new List<Travellers>();
		public List<Transportation> TransportationList { get; set; } = new List<Transportation>();
		public List<Hotels> HotelList { get; set; } = new List<Hotels>();
		public List<Miscellaneous> MiscellaneousList { get; set; } = new List<Miscellaneous>();

	}
	public class ViewModel
	{
		public List<LeadDetails> Leads { get; set; } = new List<LeadDetails>();
	}

	public class LeadDetails
	{
		public LeadViewModels Lead { get; set; }
		public List<Destinationlead> Destinations { get; set; } = new List<Destinationlead>();
		public List<Travellers> Travellers { get; set; } = new List<Travellers>();
		public List<Transportation> TransportModes { get; set; } = new List<Transportation>();
		public List<Hotels> Hotels { get; set; } = new List<Hotels>();
		public List<Miscellaneous> Activities { get; set; } = new List<Miscellaneous>();
	}

	public class LeadViewModels
	{
		public int LeadId { get; set; }
		public string? TourID { get; set; }
		public int ClientId { get; set; }
		public string? OriginCity { get; set; }
		public string? OriginCountry { get; set; }
		public string? DestinCountry { get; set; }
		public string? DestinCity { get; set; }
		public string? AccommodationType { get; set; }
		public string? SpecialNotes { get; set; }
		public bool TravelInsurance { get; set; }
		public int TotalPassengers { get; set; }
		public int TourDays { get; set; }
		public DateTime DepartureDate { get; set; }
		public DateTime ReturnDate { get; set; }
        public bool IsActive { get; set; }
        public string? SpType { get; set; }
        public bool? IsDeleted { get; set; }
    }

	public class LeadViewModel
	{
		public int leadId { get; set; }
		public string? TourID { get; set; }
		public int? ClientId { get; set; }
		public string? OriginCity { get; set; }
		public string? OriginCountry { get; set; }
		public string? DestinCountry { get; set; }
		public string? DestinCity { get; set; }
		public int? TotalPassengers { get; set; }
		public int? TourDays { get; set; }
		public DateTime? DepartureDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public string? AccommodationType { get; set; }
		public string? SpecialNotes { get; set; }
		public bool TravelInsurance { get; set; }
		public string? TourDestination { get; set; }
		public string? TourType { get; set; }
		public DateTime? CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
		public int? TotalCharges { get; set; }
		public int? ChargesPerPerson { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Gender { get; set; }
		public int Age { get; set; }
		public string? ContactNo { get; set; }
		public string? Email_Id { get; set; }
		public string? Address { get; set; }
		public string? Nationality { get; set; }
		public string? PassportNumber { get; set; }
		public string? PassportAttachment { get; set; }
		public string? VisaStatus { get; set; }
		public string? VisaType { get; set; }
		public string? EmergencyContactName { get; set; }
		public string? EmergencyContactPhone { get; set; }
		public string? FromCountry { get; set; }
		public string? FromCity { get; set; }
		public string? ToCoutry { get; set; }
		public string? ToCity { get; set; }
		public string? TravelModel { get; set; }
		public string? PartnerName { get; set; }
		public DateTime Departure { get; set; }
		public DateTime ArrivalDate { get; set; }
		public string? FaxNumber { get; set; }
		public DateTime DepartureTime { get; set; }
		public string? HotelCountry { get; set; }
		public string? HotelCity { get; set; }
		public string? HotelName { get; set; }
		public int? ForPeople { get; set; }
		public int? ForNights { get; set; }
		public bool? BreakFast { get; set; }
		public string? MealPreference { get; set; }
		public string? Activity { get; set; }
		public string? ActivityCountry { get; set; }
		public string? ActivityCity { get; set; }
		public bool Applicable { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifiedBy { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsDeleted { get; set; }
		public string? SpType { get; set; }
	}

	public class Destinationlead
	{
		public int DestinationId { get; set; }
		public int LeadId { get; set; }
		public string? TourDestination { get; set; }
		public string? TourType { get; set; }
		public DateTime CheckInDate { get; set; }
		public TimeSpan CheckInTime { get; set; }
		public DateTime CheckoutDate { get; set; }
		public TimeSpan CheckoutTime { get; set; }
		public int TotalCharges { get; set; }
		public int TotalChargesPerPerson { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int CreatedBy { get; set; }
		public int ModifyBy { get; set; }
		public bool IsActive { get; set; }
		public bool? IsDeleted { get; set; }

	}

	//GetDataById
    public class Traveller
    {
        public int TravellerId { get; set; }
        public int LeadId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public string? VisaStatus { get; set; }
        public string? Nationality { get; set; }
        public string? PassportNumber { get; set; }
        public string? UploadedPassport { get; set; }
        public string? VisaType { get; set; }
        public bool? TravelInsurance { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? SpecialNotes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string? SpType { get; set; }
    }

    public class Travellers
	{
		public int TravellerId { get; set; }
		public int LeadId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Gender { get; set; }
		public int Age { get; set; }
		public string? ContactNumber { get; set; }
		public string? EmailId { get; set; }
		public string? Address { get; set; }
		public string? VisaStatus { get; set; }
		public string? Nationality { get; set; }
		public string? PassportNumber { get; set; }
		public string? UploadedPassport { get; set; }
		public string? VisaType { get; set; }
		public bool? TravelInsurance { get; set; }
		public string? EmergencyContactNumber { get; set; }
		public string? EmergencyContactName { get; set; }
		public string? SpecialNotes { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifyBy { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsDeleted { get; set; }
		public string? SpType { get; set; }
	}
    //For GetData ById
    public class Transportations
    {
        public int TransportationId { get; set; }
        public int LeadId { get; set; }
        public string? FromCountry { get; set; }
        public string? FromCity { get; set; }
        public string? ToCountry { get; set; }
        public string? ToCity { get; set; }
        public string? ModeOfTravell { get; set; }
        public string? PartnerName { get; set; }
        public DateTime? DepartureDate { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public string? SpecialNotes { get; set; }
        public int? TotalCharge { get; set; }
        public int? TotalChargePerson { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class Transportation
	{
		public int TransportationId { get; set; }
		public int LeadId { get; set; }
		public string? FromCountry { get; set; }
		public string? FromCity { get; set; }
		public string? ToCountry { get; set; }
		public string? ToCity { get; set; }
		public string? ModeOfTravell { get; set; }
		public string? PartnerName { get; set; }
		public DateTime? DepartureDate { get; set; }
		public TimeSpan DepartureTime { get; set; }
		public DateTime ArrivalDate { get; set; }
		public TimeSpan? ArrivalTime { get; set; }
		public string? SpecialNotes { get; set; }
		public int? TotalCharge { get; set; }
		public int? TotalChargePerson { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifyBy { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsDeleted { get; set; }
	}

	//For Get data ById
    public class Hotel
    {
        public int HotelId { get; set; }
        public int LeadId { get; set; }
        public string? FromCountry { get; set; }
        public string? FromCity { get; set; }
        public string? HotelName { get; set; }
        public int? ForPeople { get; set; }
        public int? ForNights { get; set; }
        public DateTime? CheckInDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public TimeSpan? CheckoutTime { get; set; }
        public bool? BreakFastInclude { get; set; }
        public string? MealPreference { get; set; }
        public int TotalCharges { get; set; }
        public int TotalChargesPerPerson { get; set; }
        public string? SpecialNotes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class Hotels
	{
		public int HotelId { get; set; }
		public int LeadId { get; set; }
		public string? FromCountry { get; set; }
		public string? FromCity { get; set; }
		public string? HotelName { get; set; }
		public int? ForPeople { get; set; }
		public int? ForNights { get; set; }
		public DateTime CheckInDate { get; set; }
		public TimeSpan CheckInTime { get; set; }
		public DateTime CheckoutDate { get; set; }
		public TimeSpan CheckoutTime { get; set; }
		public bool? BreakFastInclude { get; set; }
		public string? MealPreference { get; set; }
		public int TotalCharges { get; set; }
		public int TotalChargesPerPerson { get; set; }
		public string? SpecialNotes { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifyBy { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsDeleted { get; set; }
	}

	//Get Data ById
    public class Miscellaneouss
    {
        public int MiscId { get; set; }
        public int LeadId { get; set; }
        public string? Activity { get; set; }
        public string? ActivityCountry { get; set; }
        public string? ActivityCity { get; set; }
        public int? TotalCharges { get; set; }
        public int? TotalChargesPerPerson { get; set; }
        public bool? ApplicableToAll { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class Miscellaneous
	{
		public int MiscId { get; set; }
		public int LeadId { get; set; }
		public string? Activity { get; set; }
		public string? ActivityCountry { get; set; }
		public string? ActivityCity { get; set; }
		public int? TotalCharges { get; set; }
		public int? TotalChargesPerPerson { get; set; }
		public bool? ApplicableToAll { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifyBy { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsDeleted { get; set; }
	}
}
