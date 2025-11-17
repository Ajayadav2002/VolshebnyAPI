using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Data;
using System.Diagnostics;
using Volshebny_API.Models;

namespace Volshebny_API.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class LeadsController : ControllerBase
	{
		private readonly IConfiguration _config;
		public LeadsController(IConfiguration config)
		{
			_config = config;
		}

		[Route("api/CommonLeads")]
		[HttpPost]
		public IActionResult MasterLead([FromBody] AddViewModel model)
		{
			try
			{
				int result;
				ServiceRequestProcessor processor = new ServiceRequestProcessor();
				APIResult apiResult;

				using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
				{
					connection.Open();
					var Leads = model.LeadsList.FirstOrDefault();
					MySqlCommand command = new MySqlCommand("usp_commonleads", connection);
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("_leadId", Leads.leadId);
					command.Parameters.AddWithValue("_TourID", Leads.TourID);
					command.Parameters.AddWithValue("_ClientId", Leads.ClientId);
					command.Parameters.AddWithValue("_OriginCountry", Leads.OriginCountry);
					command.Parameters.AddWithValue("_OriginCity", Leads.OriginCity);
					command.Parameters.AddWithValue("_DestinCountry", Leads.DestinCountry);
					command.Parameters.AddWithValue("_DestinCity", Leads.DestinCity);
					command.Parameters.AddWithValue("_TotalPassengers", Leads.TotalPassengers);
					command.Parameters.AddWithValue("_TourDays", Leads.TourDays);
					command.Parameters.AddWithValue("_DepartureDate", Leads.DepartureDate);
					command.Parameters.AddWithValue("_ReturnDate", Leads.ReturnDate);
					command.Parameters.AddWithValue("_AccommodationType", Leads.AccommodationType);
					command.Parameters.AddWithValue("_SpecialNotes", Leads.SpecialNotes);
					command.Parameters.AddWithValue("_TravelInsurance", Leads.TravelInsurance);
					command.Parameters.AddWithValue("_TourDestination", Leads.TourDestination);
					command.Parameters.AddWithValue("_TourType", Leads.TourType);
					command.Parameters.AddWithValue("_CheckIn", Leads.CheckIn);
					command.Parameters.AddWithValue("_CheckOut", Leads.CheckOut);
					command.Parameters.AddWithValue("_TotalCharges", Leads.TotalCharges);
					command.Parameters.AddWithValue("_ChargesPerPerson", Leads.ChargesPerPerson);
					command.Parameters.AddWithValue("_FirstName", Leads.FirstName);
					command.Parameters.AddWithValue("_LastName", Leads.LastName);
					command.Parameters.AddWithValue("_Gender", Leads.Gender);
					command.Parameters.AddWithValue("_Age", Leads.Age);
					command.Parameters.AddWithValue("_ContactNo", Leads.ContactNo);
					command.Parameters.AddWithValue("_Email_Id", Leads.Email_Id);
					command.Parameters.AddWithValue("_Address", Leads.Address);
					command.Parameters.AddWithValue("_Nationality", Leads.Nationality);
					command.Parameters.AddWithValue("_PassportNumber", Leads.PassportNumber);
					command.Parameters.AddWithValue("_PassportAttachment", Leads.PassportAttachment);
					command.Parameters.AddWithValue("_VisaStatus", Leads.VisaStatus);
					command.Parameters.AddWithValue("_VisaType", Leads.VisaType);
					command.Parameters.AddWithValue("_EmergencyContactName", Leads.EmergencyContactName);
					command.Parameters.AddWithValue("_EmergencyContactPhone", Leads.EmergencyContactPhone);
					command.Parameters.AddWithValue("_FromCountry", Leads.FromCountry);
					command.Parameters.AddWithValue("_FromCity", Leads.FromCity);
					command.Parameters.AddWithValue("_ToCoutry", Leads.ToCoutry);
					command.Parameters.AddWithValue("_ToCity", Leads.ToCity);
					command.Parameters.AddWithValue("_TravelModel", Leads.TravelModel);
					command.Parameters.AddWithValue("_PartnerName", Leads.PartnerName);
					command.Parameters.AddWithValue("_Departure", Leads.Departure);
					command.Parameters.AddWithValue("_ArrivalDate", Leads.ArrivalDate);
					command.Parameters.AddWithValue("_FaxNumber", Leads.FaxNumber);
					command.Parameters.AddWithValue("_DepartureTime", Leads.DepartureTime);
					command.Parameters.AddWithValue("_HotelCountry", Leads.HotelCountry);
					command.Parameters.AddWithValue("_HotelCity", Leads.HotelCity);
					command.Parameters.AddWithValue("_HotelName", Leads.HotelName);
					command.Parameters.AddWithValue("_ForPeople", Leads.ForPeople);
					command.Parameters.AddWithValue("_ForNights", Leads.ForNights);
					command.Parameters.AddWithValue("_BreakFast", Leads.BreakFast);
					command.Parameters.AddWithValue("_MealPreference", Leads.MealPreference);
					command.Parameters.AddWithValue("_Activity", Leads.Activity);
					command.Parameters.AddWithValue("_ActivityCountry", Leads.ActivityCountry);
					command.Parameters.AddWithValue("_ActivityCity", Leads.ActivityCity);
					command.Parameters.AddWithValue("_Applicable", Leads.Applicable);
					command.Parameters.AddWithValue("_CreatedDate", Leads.CreatedDate);
					command.Parameters.AddWithValue("_ModifiedDate", Leads.ModifiedDate);
					command.Parameters.AddWithValue("_CreatedBy", Leads.CreatedBy);
					command.Parameters.AddWithValue("_ModifiedBy", Leads.ModifiedBy);
					command.Parameters.AddWithValue("_IsActive", Leads.IsActive);
					command.Parameters.AddWithValue("_IsDeleted", Leads.IsDeleted);
					command.Parameters.AddWithValue("_SpType", Leads.SpType);

					MySqlParameter resultParam = new MySqlParameter("_Result", MySqlDbType.Int32);
					resultParam.Direction = ParameterDirection.Output;
					command.Parameters.Add(resultParam);

					if (Leads.SpType == "C" || Leads.SpType == "U" || Leads.SpType == "D")
					{
						command.ExecuteNonQuery();
						result = Convert.ToInt32(command.Parameters["_Result"].Value);

						if (result != 0)
						{
							//Insert destinations
							foreach (var dest in model.DestinationList)
							{
								MySqlCommand destCmd = new MySqlCommand("usp_commonDestinationLead", connection);
								destCmd.CommandType = CommandType.StoredProcedure;

								destCmd.Parameters.AddWithValue("_DestinationId", dest.DestinationId);
								destCmd.Parameters.AddWithValue("_LeadId", result);
								destCmd.Parameters.AddWithValue("_TourDestination", dest.TourDestination);
								destCmd.Parameters.AddWithValue("_TourType", dest.TourType);
								destCmd.Parameters.AddWithValue("_CheckInDate", dest.CheckInDate);
								destCmd.Parameters.AddWithValue("_CheckInTime", dest.CheckInTime);
								destCmd.Parameters.AddWithValue("_CheckoutDate", dest.CheckoutDate);
								destCmd.Parameters.AddWithValue("_CheckoutTime", dest.CheckoutTime);
								destCmd.Parameters.AddWithValue("_TotalCharges", dest.TotalCharges);
								destCmd.Parameters.AddWithValue("_TotalChargesPerPerson", dest.TotalChargesPerPerson);
								destCmd.Parameters.AddWithValue("_CreatedBy", dest.CreatedBy);
								destCmd.Parameters.AddWithValue("_ModifyBy", dest.ModifyBy);
								destCmd.Parameters.AddWithValue("_IsActive", dest.IsActive);
								destCmd.Parameters.AddWithValue("_SpType", "C");

								MySqlParameter destResult = new MySqlParameter("_Result", MySqlDbType.Int32)
								{
									Direction = ParameterDirection.Output
								};
								destCmd.Parameters.Add(destResult);
								destCmd.ExecuteNonQuery();
							}

							//Insert travellers
							foreach (var traveller in model.TravellersList)
							{
								MySqlCommand travCmd = new MySqlCommand("usp_commonTravellers", connection);
								travCmd.CommandType = CommandType.StoredProcedure;

								travCmd.Parameters.AddWithValue("_TravellerId", traveller.TravellerId);
								travCmd.Parameters.AddWithValue("_LeadId", result);
								travCmd.Parameters.AddWithValue("_FirstName", traveller.FirstName);
								travCmd.Parameters.AddWithValue("_LastName", traveller.LastName);
								travCmd.Parameters.AddWithValue("_Gender", traveller.Gender);
								travCmd.Parameters.AddWithValue("_Age", traveller.Age);
								travCmd.Parameters.AddWithValue("_ContactNumber", traveller.ContactNumber);
								travCmd.Parameters.AddWithValue("_EmailId", traveller.EmailId);
								travCmd.Parameters.AddWithValue("_Address", traveller.Address);
								travCmd.Parameters.AddWithValue("_Nationality", traveller.Nationality);
								travCmd.Parameters.AddWithValue("_PassportNumber", traveller.PassportNumber);
								travCmd.Parameters.AddWithValue("_UploadedPassport", traveller.UploadedPassport);
								travCmd.Parameters.AddWithValue("_VisaStatus", traveller.VisaStatus);
								travCmd.Parameters.AddWithValue("_VisaType", traveller.VisaType);
								travCmd.Parameters.AddWithValue("_TravelInsurance", traveller.TravelInsurance);
								travCmd.Parameters.AddWithValue("_EmergencyContactNumber", traveller.EmergencyContactNumber);
								travCmd.Parameters.AddWithValue("_EmergencyContactName", traveller.EmergencyContactName);
								travCmd.Parameters.AddWithValue("_SpecialNotes", traveller.SpecialNotes);
								travCmd.Parameters.AddWithValue("_CreatedDate", traveller.CreatedDate);
								travCmd.Parameters.AddWithValue("_ModifiedDate", traveller.ModifiedDate);
								travCmd.Parameters.AddWithValue("_CreatedBy", traveller.CreatedBy);
								travCmd.Parameters.AddWithValue("_ModifyBy", traveller.ModifyBy);
								travCmd.Parameters.AddWithValue("_IsActive", traveller.IsActive);
								travCmd.Parameters.AddWithValue("_SpType", "C");

								MySqlParameter travResult = new MySqlParameter("_Result", MySqlDbType.Int32)
								{
									Direction = ParameterDirection.Output
								};
								travCmd.Parameters.Add(travResult);
								travCmd.ExecuteNonQuery();
							}

							//Insert transportations
							foreach (var transportations in model.TransportationList)
							{
								MySqlCommand transporCmd = new MySqlCommand("usp_commonTransportation", connection);
								transporCmd.CommandType = CommandType.StoredProcedure;

								transporCmd.Parameters.AddWithValue("_TransportationId", transportations.TransportationId);
								transporCmd.Parameters.AddWithValue("_LeadId", result);
								transporCmd.Parameters.AddWithValue("_FromCountry", transportations.FromCountry);
								transporCmd.Parameters.AddWithValue("_FromCity", transportations.FromCity);
								transporCmd.Parameters.AddWithValue("_ToCountry", transportations.ToCountry);
								transporCmd.Parameters.AddWithValue("_ToCity", transportations.ToCity);
								transporCmd.Parameters.AddWithValue("_ModeOfTravell", transportations.ModeOfTravell);
								transporCmd.Parameters.AddWithValue("_PartnerName", transportations.PartnerName);
								transporCmd.Parameters.AddWithValue("_DepartureDate", transportations.DepartureDate);
								transporCmd.Parameters.AddWithValue("_DepartureTime", transportations.DepartureTime);
								transporCmd.Parameters.AddWithValue("_ArrivalDate", transportations.ArrivalDate);
								transporCmd.Parameters.AddWithValue("_ArrivalTime", transportations.ArrivalTime);
								transporCmd.Parameters.AddWithValue("_SpecialNotes", transportations.SpecialNotes);
								transporCmd.Parameters.AddWithValue("_TotalCharge", transportations.TotalCharge);
								transporCmd.Parameters.AddWithValue("_TotalChargePerson", transportations.TotalChargePerson);
								transporCmd.Parameters.AddWithValue("_CreatedDate", transportations.CreatedDate);
								transporCmd.Parameters.AddWithValue("_ModifiedDate", transportations.ModifiedDate);
								transporCmd.Parameters.AddWithValue("_CreatedBy", transportations.CreatedBy);
								transporCmd.Parameters.AddWithValue("_ModifyBy", transportations.ModifyBy);
								transporCmd.Parameters.AddWithValue("_IsActive", transportations.IsActive);
								transporCmd.Parameters.AddWithValue("_SpType", "C");

								MySqlParameter transResult = new MySqlParameter("_Result", MySqlDbType.Int32)
								{
									Direction = ParameterDirection.Output
								};
								transporCmd.Parameters.Add(transResult);
								transporCmd.ExecuteNonQuery();
							}

							//Insert Hotels
							foreach (var Hotels in model.HotelList)
							{
								MySqlCommand HotelCmd = new MySqlCommand("usp_commonHotel", connection);
								HotelCmd.CommandType = CommandType.StoredProcedure;

								HotelCmd.Parameters.AddWithValue("_HotelId", Hotels.HotelId);
								HotelCmd.Parameters.AddWithValue("_LeadId", result);
								HotelCmd.Parameters.AddWithValue("_FromCountry", Hotels.FromCountry);
								HotelCmd.Parameters.AddWithValue("_FromCity", Hotels.FromCity);
								HotelCmd.Parameters.AddWithValue("_HotelName", Hotels.HotelName);
								HotelCmd.Parameters.AddWithValue("_ForPeople", Hotels.ForPeople);
								HotelCmd.Parameters.AddWithValue("_ForNights", Hotels.ForNights);
								HotelCmd.Parameters.AddWithValue("_CheckInDate", Hotels.CheckInDate);
								HotelCmd.Parameters.AddWithValue("_CheckInTime", Hotels.CheckInTime);
								HotelCmd.Parameters.AddWithValue("_CheckoutDate", Hotels.CheckoutDate);
								HotelCmd.Parameters.AddWithValue("_CheckoutTime", Hotels.CheckoutTime);
								HotelCmd.Parameters.AddWithValue("_BreakFastInclude", Hotels.BreakFastInclude);
								HotelCmd.Parameters.AddWithValue("_MealPreference", Hotels.MealPreference);
								HotelCmd.Parameters.AddWithValue("_TotalCharges", Hotels.TotalCharges);
								HotelCmd.Parameters.AddWithValue("_TotalChargesPerPerson", Hotels.TotalChargesPerPerson);
								HotelCmd.Parameters.AddWithValue("_CreatedDate", Hotels.CreatedDate);
								HotelCmd.Parameters.AddWithValue("_ModifiedDate", Hotels.ModifiedDate);
								HotelCmd.Parameters.AddWithValue("_SpecialNotes", Hotels.SpecialNotes);
								HotelCmd.Parameters.AddWithValue("_CreatedBy", Hotels.CreatedBy);
								HotelCmd.Parameters.AddWithValue("_ModifyBy", Hotels.ModifyBy);
								HotelCmd.Parameters.AddWithValue("_IsActive", Hotels.IsActive);
								HotelCmd.Parameters.AddWithValue("_SpType", "C");

								MySqlParameter hotelResult = new MySqlParameter("_Result", MySqlDbType.Int32)
								{
									Direction = ParameterDirection.Output
								};
								HotelCmd.Parameters.Add(hotelResult);
								HotelCmd.ExecuteNonQuery();
							}

							// Insert Miscellaneous
							foreach (var Misc in model.MiscellaneousList)
							{
								MySqlCommand Miscellaneous = new MySqlCommand("usp_commonMiscellaneous", connection);
								Miscellaneous.CommandType = CommandType.StoredProcedure;

								Miscellaneous.Parameters.AddWithValue("_MiscId", Misc.MiscId);
								Miscellaneous.Parameters.AddWithValue("_LeadId", result);
								Miscellaneous.Parameters.AddWithValue("_Activity", Misc.Activity);
								Miscellaneous.Parameters.AddWithValue("_ActivityCountry", Misc.ActivityCountry);
								Miscellaneous.Parameters.AddWithValue("_ActivityCity", Misc.ActivityCity);
								Miscellaneous.Parameters.AddWithValue("_TotalCharges", Misc.TotalCharges);
								Miscellaneous.Parameters.AddWithValue("_TotalChargesPerPerson", Misc.TotalChargesPerPerson);
								Miscellaneous.Parameters.AddWithValue("_ApplicableToAll", Misc.ApplicableToAll);
								Miscellaneous.Parameters.AddWithValue("_CreatedDate", Misc.CreatedDate);
								Miscellaneous.Parameters.AddWithValue("_ModifiedDate", Misc.ModifiedDate);
								Miscellaneous.Parameters.AddWithValue("_CreatedBy", Misc.CreatedBy);
								Miscellaneous.Parameters.AddWithValue("_ModifyBy", Misc.ModifyBy);
								Miscellaneous.Parameters.AddWithValue("_IsActive", Misc.IsActive);
								Miscellaneous.Parameters.AddWithValue("_SpType", "C");

								MySqlParameter MiscResult = new MySqlParameter("_Result", MySqlDbType.Int32)
								{
									Direction = ParameterDirection.Output
								};
								Miscellaneous.Parameters.Add(MiscResult);
								Miscellaneous.ExecuteNonQuery();
							}

							apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, "Lead saved successfully.");
							apiResult.data = new { Id = result };
							return Ok(apiResult);
						}
						else
						{
							apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.InsertUpdateFailed, "Failed to save lead.");
							return Ok(apiResult);
						}
					}
					else if (Leads.SpType == "R" || Leads.SpType == "E")
					{
						using (var reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								List<LeadViewModel> leadList = new List<LeadViewModel>();

								while (reader.Read())
								{
									LeadViewModel lead = new LeadViewModel
									{
										leadId = Convert.ToInt32(reader["leadId"]),
										TourID = reader["TourID"] != DBNull.Value ? reader["TourID"].ToString() : null,
										ClientId = reader["ClientId"] != DBNull.Value ? Convert.ToInt32(reader["ClientId"]) : (int?)null,
										OriginCountry = reader["OriginCountry"] != DBNull.Value ? reader["OriginCountry"].ToString() : null,
										OriginCity = reader["OriginCity"] != DBNull.Value ? reader["OriginCity"].ToString() : null,
										DestinCountry = reader["DestinCountry"] != DBNull.Value ? reader["DestinCountry"].ToString() : null,
										DestinCity = reader["DestinCity"] != DBNull.Value ? reader["DestinCity"].ToString() : null,
										TotalPassengers = reader["TotalPassengers"] != DBNull.Value ? Convert.ToInt32(reader["TotalPassengers"]) : (int?)null,
										TourDays = reader["TourDays"] != DBNull.Value ? Convert.ToInt32(reader["TourDays"]) : (int?)null,
										DepartureDate = reader["DepartureDate"] != DBNull.Value ? Convert.ToDateTime(reader["DepartureDate"]) : (DateTime?)null,
										ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : (DateTime?)null,
										AccommodationType = reader["AccommodationType"] != DBNull.Value ? reader["AccommodationType"].ToString() : null,
										SpecialNotes = reader["SpecialNotes"] != DBNull.Value ? reader["SpecialNotes"].ToString() : null,
										TravelInsurance = reader["TravelInsurance"] != DBNull.Value && Convert.ToBoolean(reader["TravelInsurance"]),
										TourDestination = reader["TourDestination"] != DBNull.Value ? reader["TourDestination"].ToString() : null,
										TourType = reader["TourType"] != DBNull.Value ? reader["TourType"].ToString() : null,
										CheckIn = reader["CheckIn"] != DBNull.Value ? Convert.ToDateTime(reader["CheckIn"]) : (DateTime?)null,
										CheckOut = reader["CheckOut"] != DBNull.Value ? Convert.ToDateTime(reader["CheckOut"]) : (DateTime?)null,
										TotalCharges = reader["TotalCharges"] != DBNull.Value ? Convert.ToInt32(reader["TotalCharges"]) : (int?)null,
										ChargesPerPerson = reader["ChargesPerPerson"] != DBNull.Value ? Convert.ToInt32(reader["ChargesPerPerson"]) : (int?)null,
										FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
										LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
										Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : null,
										Age = Convert.ToInt32(reader["Age"]),
										ContactNo = reader["ContactNo"] != DBNull.Value ? reader["ContactNo"].ToString() : null,
										Email_Id = reader["Email_Id"] != DBNull.Value ? reader["Email_Id"].ToString() : null,
										Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
										Nationality = reader["Nationality"] != DBNull.Value ? reader["Nationality"].ToString() : null,
										PassportNumber = reader["PassportNumber"] != DBNull.Value ? reader["PassportNumber"].ToString() : null,
										PassportAttachment = reader["PassportAttachment"] != DBNull.Value ? reader["PassportAttachment"].ToString() : null,
										VisaStatus = reader["VisaStatus"] != DBNull.Value ? reader["VisaStatus"].ToString() : null,
										VisaType = reader["VisaType"] != DBNull.Value ? reader["VisaType"].ToString() : null,
										EmergencyContactName = reader["EmergencyContactName"] != DBNull.Value ? reader["EmergencyContactName"].ToString() : null,
										EmergencyContactPhone = reader["EmergencyContactPhone"] != DBNull.Value ? reader["EmergencyContactPhone"].ToString() : null,
										FromCountry = reader["FromCountry"] != DBNull.Value ? reader["FromCountry"].ToString() : null,
										FromCity = reader["FromCity"] != DBNull.Value ? reader["FromCity"].ToString() : null,
										ToCoutry = reader["ToCoutry"] != DBNull.Value ? reader["ToCoutry"].ToString() : null,
										ToCity = reader["ToCity"] != DBNull.Value ? reader["ToCity"].ToString() : null,
										TravelModel = reader["TravelModel"] != DBNull.Value ? reader["TravelModel"].ToString() : null,
										PartnerName = reader["PartnerName"] != DBNull.Value ? reader["PartnerName"].ToString() : null,
										Departure = Convert.ToDateTime(reader["Departure"]),
										ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"]),
										FaxNumber = reader["FaxNumber"] != DBNull.Value ? reader["FaxNumber"].ToString() : null,
										DepartureTime = Convert.ToDateTime(reader["DepartureTime"]),
										HotelCountry = reader["HotelCountry"] != DBNull.Value ? reader["HotelCountry"].ToString() : null,
										HotelCity = reader["HotelCity"] != DBNull.Value ? reader["HotelCity"].ToString() : null,
										HotelName = reader["HotelName"] != DBNull.Value ? reader["HotelName"].ToString() : null,
										ForPeople = reader["ForPeople"] != DBNull.Value ? Convert.ToInt32(reader["ForPeople"]) : (int?)null,
										ForNights = reader["ForNights"] != DBNull.Value ? Convert.ToInt32(reader["ForNights"]) : (int?)null,
										BreakFast = reader["BreakFast"] != DBNull.Value ? Convert.ToBoolean(reader["BreakFast"]) : (bool?)null,
										MealPreference = reader["MealPreference"] != DBNull.Value ? reader["MealPreference"].ToString() : null,
										Activity = reader["Activity"] != DBNull.Value ? reader["Activity"].ToString() : null,
										ActivityCountry = reader["ActivityCountry"] != DBNull.Value ? reader["ActivityCountry"].ToString() : null,
										ActivityCity = reader["ActivityCity"] != DBNull.Value ? reader["ActivityCity"].ToString() : null,
										Applicable = reader["Applicable"] != DBNull.Value && Convert.ToBoolean(reader["Applicable"]),
										CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
										ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
										CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : (int?)null,
										ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
										IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : (bool?)null,
									};

									leadList.Add(lead);
								}
								apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
								apiResult.data = leadList;
								return Ok(apiResult);
							}
							else
							{
								apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, ServiceRequestProcessor.StatusCode.DataNotFound.ToString());
								apiResult.data = new { };
								return NotFound(apiResult);
							}
						}
					}
					else
					{
						apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.ServerError, "Invalid SpType");
						apiResult.data = new { };
						return BadRequest(apiResult);
					}
				}
			}
			catch (Exception ex)
			{
				ServiceRequestProcessor oServiceRequestProcessor = new ServiceRequestProcessor();
				return BadRequest(oServiceRequestProcessor.onError(ex.Message));
			}
		}

		[Route("api/ManageUnconfirmedLead")]
        [HttpPost]
        public IActionResult ManageUnconfirmedLead([FromBody] UnconfirmedLead lead)
        {
            try
            {
                int result;
                ServiceRequestProcessor processor = new ServiceRequestProcessor();
                APIResult apiResult;

                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("usp_commonUnconfirmedLead", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("_UnconfirmedId", lead.UnconfirmedId);
                    command.Parameters.AddWithValue("_leadId", lead.leadId);
                    command.Parameters.AddWithValue("_TourCountry", lead.TourCountry);
                    command.Parameters.AddWithValue("_Destinations", lead.Destinations);
                    command.Parameters.AddWithValue("_Total_Travellers", lead.Total_Travellers);
                    command.Parameters.AddWithValue("_Start_date", lead.Start_date);
                    command.Parameters.AddWithValue("_End_date", lead.End_date);
                    command.Parameters.AddWithValue("_Package_charge", lead.Package_charge);
                    command.Parameters.AddWithValue("_Status", lead.Status);
                    command.Parameters.AddWithValue("_CreatedBy", lead.CreatedBy);
                    command.Parameters.AddWithValue("_ModifiedBy", lead.ModifiedBy);
                    command.Parameters.AddWithValue("_CreatedDate", lead.CreatedDate);
                    command.Parameters.AddWithValue("_ModifiedDate", lead.ModifiedDate);
                    command.Parameters.AddWithValue("_IsActive", lead.IsActive);
                    command.Parameters.AddWithValue("_IsDelete", lead.IsDelete);
                    command.Parameters.AddWithValue("_SpType", lead.SpType);

                    MySqlParameter resultParam = new MySqlParameter("_Result", MySqlDbType.Int32);
                    resultParam.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(resultParam);

                    if (lead.SpType == "C" || lead.SpType == "U" || lead.SpType == "D")
                    {
                        command.ExecuteNonQuery();
                        result = Convert.ToInt32(command.Parameters["_Result"].Value);

                        if (result != 0)
                        {
                            apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                            apiResult.data = new { Id = result };
                            return Ok(apiResult);
                        }
                        else
                        {
                            apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.InsertUpdateFailed, ServiceRequestProcessor.StatusCode.InsertUpdateFailed.ToString());
                            apiResult.data = new { Id = result };
                            return Ok(apiResult);
                        }
                    }
                    else if (lead.SpType == "E" || lead.SpType == "R")
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<UnconfirmedLead> leadList = new List<UnconfirmedLead>();

                                while (reader.Read())
                                {
                                    var leadModel = new UnconfirmedLead
                                    {
                                        UnconfirmedId = Convert.ToInt32(reader["UnconfirmedId"]),
                                        leadId = Convert.ToInt32(reader["leadId"]),
                                        TourCountry = reader["TourCountry"]?.ToString(),
                                        Destinations = reader["Destinations"]?.ToString(),
                                        Status = reader["Status"]?.ToString(),
										Total_Travellers = reader["Total_Travellers"] != DBNull.Value ? Convert.ToInt32(reader["Total_Travellers"]) : 0,
										Start_date = reader["Start_date"] != DBNull.Value ? Convert.ToDateTime(reader["Start_date"]) : (DateTime?)null,
										End_date = reader["End_date"] != DBNull.Value ? Convert.ToDateTime(reader["End_date"]) : (DateTime?)null,
										Package_charge = reader["Package_charge"] != DBNull.Value ? Convert.ToDecimal(reader["Package_charge"]) : (int?)null,
                                        CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                        ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                        ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                                    };

                                    leadList.Add(leadModel);
                                }
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                apiResult.data = leadList;
                                return Ok(apiResult);
                            }
                            else
                            {
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, ServiceRequestProcessor.StatusCode.DataNotFound.ToString());
                                apiResult.data = new { };
                                return NotFound(apiResult);
                            }
                        }
                    }
                    else
                    {
                        apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.ServerError, "Invalid SpType");
                        apiResult.data = new { };
                        return BadRequest(apiResult);
                    }
                }
            }
            catch (Exception ex)
            {
                ServiceRequestProcessor oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("api/GetLeadsReports")]
        [HttpGet]
        public IActionResult GetLeads()
        {
            try
            {
                ServiceRequestProcessor processor = new ServiceRequestProcessor();
                var viewModel = new ViewModel();
                var leadsDict = new Dictionary<int, LeadDetails>();

                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                using (MySqlCommand cmd = new MySqlCommand("usp_GetLeadDetails", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int leadId = reader["LeadId"] != DBNull.Value ? Convert.ToInt32(reader["LeadId"]) : 0;

                            var lead = new LeadViewModels
                            {
                                LeadId = leadId,
                                TourID = reader["TourID"]?.ToString(),
                                ClientId = reader["ClientId"] != DBNull.Value ? Convert.ToInt32(reader["ClientId"]) : 0,
                                OriginCity = reader["OriginCity"]?.ToString(),
                                OriginCountry = reader["OriginCountry"]?.ToString(),
                                DestinCountry = reader["DestinCountry"]?.ToString(),
                                DestinCity = reader["DestinCity"]?.ToString(),
                                AccommodationType = reader["AccommodationType"]?.ToString(),
                                SpecialNotes = reader["SpecialNotes"]?.ToString(),
								TravelInsurance = reader["TravelInsurance"] != DBNull.Value ? Convert.ToBoolean(reader["TravelInsurance"]) : false,
                                TotalPassengers = reader["TotalPassengers"] != DBNull.Value ? Convert.ToInt32(reader["TotalPassengers"]) : 0,
                                TourDays = reader["TourDays"] != DBNull.Value ? Convert.ToInt32(reader["TourDays"]) : 0,
                                DepartureDate = reader["DepartureDate"] != DBNull.Value ? Convert.ToDateTime(reader["DepartureDate"]) : DateTime.MinValue,
                                ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : DateTime.MinValue,
                                IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                IsDeleted= reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false
                            };

                            leadsDict[leadId] = new LeadDetails { Lead = lead };
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int leadId = reader["LeadId"] != DBNull.Value ? Convert.ToInt32(reader["LeadId"]) : 0;
                                if (leadsDict.TryGetValue(leadId, out var leadDetail))
                                {
                                    leadDetail.Destinations.Add(new Destinationlead
                                    {
                                        LeadId = leadId,
                                        TourDestination = reader["TourDestination"]?.ToString(),
                                        TourType = reader["TourType"]?.ToString(),
                                        CheckInDate = reader["CheckInDate"] != DBNull.Value ? Convert.ToDateTime(reader["CheckInDate"]) : DateTime.MinValue,
                                        CheckInTime = reader["CheckInTime"] != DBNull.Value ? (TimeSpan)reader["CheckInTime"] : default,
                                        CheckoutDate = reader["CheckoutDate"] != DBNull.Value ? Convert.ToDateTime(reader["CheckoutDate"]) : DateTime.MinValue,
                                        CheckoutTime = reader["CheckoutTime"] != DBNull.Value ? (TimeSpan)reader["CheckoutTime"] : default,
                                        TotalCharges = reader["TotalCharges"] != DBNull.Value ? Convert.ToInt32(reader["TotalCharges"]) : 0,
                                        TotalChargesPerPerson = reader["TotalChargesPerPerson"] != DBNull.Value ? Convert.ToInt32(reader["TotalChargesPerPerson"]) : 0,
                                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                        ModifyBy = reader["ModifyBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifyBy"]) : 0,
                                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                        IsDeleted = reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false
                                    });
                                }
                            }
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int leadId = reader["LeadId"] != DBNull.Value ? Convert.ToInt32(reader["LeadId"]) : 0;
                                if (leadsDict.TryGetValue(leadId, out var leadDetail))
                                {
                                    leadDetail.Travellers.Add(new Travellers
                                    {
                                        LeadId = leadId,
                                        FirstName = reader["FirstName"]?.ToString(),
                                        LastName = reader["LastName"]?.ToString(),
                                        Gender = reader["Gender"]?.ToString(),
                                        Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : 0,
                                        ContactNumber = reader["ContactNumber"]?.ToString(),
                                        EmailId = reader["EmailId"]?.ToString(),
                                        Address = reader["Address"]?.ToString(),
                                        VisaStatus = reader["VisaStatus"]?.ToString(),
                                        Nationality = reader["Nationality"]?.ToString(),
                                        PassportNumber = reader["PassportNumber"]?.ToString(),
                                        UploadedPassport = reader["UploadedPassport"]?.ToString(),
                                        VisaType = reader["VisaType"]?.ToString(),
                                        TravelInsurance = reader["TravelInsurance"] != DBNull.Value ? Convert.ToBoolean(reader["TravelInsurance"]) : false,
                                        EmergencyContactNumber = reader["EmergencyContactNumber"]?.ToString(),
                                        EmergencyContactName = reader["EmergencyContactName"]?.ToString(),
                                        SpecialNotes = reader["SpecialNotes"]?.ToString(),
                                        CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                        ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue,
                                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                        IsDeleted= reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false
                                    });
                                }
                            }
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int leadId = reader["LeadId"] != DBNull.Value ? Convert.ToInt32(reader["LeadId"]) : 0;
                                if (leadsDict.TryGetValue(leadId, out var leadDetail))
                                {
                                    leadDetail.TransportModes.Add(new Transportation
                                    {
                                        LeadId = leadId,
                                        FromCountry = reader["FromCountry"]?.ToString(),
                                        FromCity = reader["FromCity"]?.ToString(),
                                        ToCountry = reader["ToCountry"]?.ToString(),
                                        ToCity = reader["ToCity"]?.ToString(),
                                        ModeOfTravell = reader["ModeOfTravell"]?.ToString(),
                                        PartnerName = reader["PartnerName"]?.ToString(),
                                        DepartureDate = reader["DepartureDate"] != DBNull.Value ? Convert.ToDateTime(reader["DepartureDate"]) : DateTime.MinValue,
                                        ArrivalDate = reader["ArrivalDate"] != DBNull.Value ? Convert.ToDateTime(reader["ArrivalDate"]) : DateTime.MinValue,
                                        DepartureTime = reader["DepartureTime"] != DBNull.Value ? (TimeSpan)reader["DepartureTime"] : default,
                                        ArrivalTime = reader["ArrivalTime"] != DBNull.Value ? (TimeSpan)reader["ArrivalTime"] : default,
                                        SpecialNotes = reader["SpecialNotes"]?.ToString(),
                                        TotalCharge = reader["TotalCharge"] != DBNull.Value ? Convert.ToInt32(reader["TotalCharge"]) : 0,
                                        TotalChargePerson = reader["TotalChargePerson"] != DBNull.Value ? Convert.ToInt32(reader["TotalChargePerson"]) : 0,
                                        CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                        ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue,
                                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                        ModifyBy = reader["ModifyBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifyBy"]) : 0,
                                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                        IsDeleted = reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false
                                    });
                                }
                            }
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int leadId = reader["LeadId"] != DBNull.Value ? Convert.ToInt32(reader["LeadId"]) : 0;
                                if (leadsDict.TryGetValue(leadId, out var leadDetail))
                                {
                                    leadDetail.Hotels.Add(new Hotels
                                    {
                                        LeadId = leadId,
                                        HotelName = reader["HotelName"]?.ToString(),
                                        FromCountry = reader["FromCountry"]?.ToString(),
                                        FromCity = reader["FromCity"]?.ToString(),
                                        ForPeople = reader["ForPeople"] != DBNull.Value ? Convert.ToInt32(reader["ForPeople"]) : 0,
                                        ForNights = reader["ForNights"] != DBNull.Value ? Convert.ToInt32(reader["ForNights"]) : 0,
                                        CheckInDate = reader["CheckInDate"] != DBNull.Value ? Convert.ToDateTime(reader["CheckInDate"]) : DateTime.MinValue,
                                        CheckInTime = reader["CheckInTime"] != DBNull.Value ? (TimeSpan)reader["CheckInTime"] : default,
                                        CheckoutTime = reader["CheckoutTime"] != DBNull.Value ? (TimeSpan)reader["CheckoutTime"] : default,
                                        CheckoutDate = reader["CheckoutDate"] != DBNull.Value ? Convert.ToDateTime(reader["CheckoutDate"]) : DateTime.MinValue,
                                        BreakFastInclude = reader["BreakFastInclude"] != DBNull.Value ? Convert.ToBoolean(reader["BreakFastInclude"]) : false,
                                        MealPreference = reader["MealPreference"]?.ToString(),
                                        TotalCharges = reader["TotalCharges"] != DBNull.Value ? Convert.ToInt32(reader["TotalCharges"]) : 0,
                                        TotalChargesPerPerson = reader["TotalChargesPerPerson"] != DBNull.Value ? Convert.ToInt32(reader["TotalChargesPerPerson"]) : 0,
                                        SpecialNotes = reader["SpecialNotes"]?.ToString(),
                                        CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                        ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue,
                                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                        ModifyBy = reader["ModifyBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifyBy"]) : 0,
                                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                        IsDeleted = reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false
                                    });
                                }
                            }
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                int leadId = reader["LeadId"] != DBNull.Value ? Convert.ToInt32(reader["LeadId"]) : 0;
                                if (leadsDict.TryGetValue(leadId, out var leadDetail))
                                {
                                    leadDetail.Activities.Add(new Miscellaneous
                                    {
                                        LeadId = leadId,
                                        Activity = reader["Activity"]?.ToString(),
                                        ActivityCountry = reader["ActivityCountry"]?.ToString(),
                                        ActivityCity = reader["ActivityCity"]?.ToString(),
                                        TotalCharges = reader["TotalCharges"] != DBNull.Value ? Convert.ToInt32(reader["TotalCharges"]) : 0,
                                        TotalChargesPerPerson = reader["TotalChargesPerPerson"] != DBNull.Value ? Convert.ToInt32(reader["TotalChargesPerPerson"]) : 0,
                                        ApplicableToAll = reader["ApplicableToAll"] != DBNull.Value ? Convert.ToBoolean(reader["ApplicableToAll"]) : false,
                                        CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                                        ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : DateTime.MinValue,
                                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : 0,
                                        ModifyBy = reader["ModifyBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifyBy"]) : 0,
                                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false,
                                        IsDeleted = reader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(reader["IsDeleted"]) : false
                                    });
                                }
                            }
                        }
                        return Ok(new { status_code = 100, message = "Success", data = leadsDict.Values.ToList() });
                    }
                }
               
            }
            catch (Exception ex)
            {
                ServiceRequestProcessor oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }


        [HttpGet]
        [Route("api/GetLeadFullDetails/{leadId}")]
        public IActionResult GetLeadFullDetails(int leadId)
        {
            try
            {
                var result = new LeadDetailsViewModel
                {
                    Lead = new LeadViewModels(),
                    Destinations = new List<Destinationlead>(),
                    Travellers = new List<Traveller>(),
                    Transportations = new List<Transportations>(),
                    Hotels = new List<Hotel>(),
                    MiscellaneousItems = new List<Miscellaneouss>()
                };

                using (MySqlConnection con = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    using (MySqlCommand cmd = new MySqlCommand("usp_GetLeadDetailsById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@leadId", leadId);

                        con.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            // 1. Lead
                            if (reader.Read())
                            {
                                result.Lead = new LeadViewModels
                                {
                                    LeadId = Convert.ToInt32(reader["LeadId"]),
                                    TourID = reader["TourID"]?.ToString(),
                                    ClientId = Convert.ToInt32(reader["ClientId"]),
                                    OriginCity = reader["OriginCity"]?.ToString(),
                                    OriginCountry = reader["OriginCountry"]?.ToString(),
                                    DestinCity = reader["DestinCity"]?.ToString(),
                                    DestinCountry = reader["DestinCountry"]?.ToString(),
                                    AccommodationType = reader["AccommodationType"]?.ToString(),
                                    SpecialNotes = reader["SpecialNotes"]?.ToString(),
                                    TravelInsurance = Convert.ToBoolean(reader["TravelInsurance"]),
                                    TotalPassengers = Convert.ToInt32(reader["TotalPassengers"]),
                                    TourDays = Convert.ToInt32(reader["TourDays"]),
                                    DepartureDate = Convert.ToDateTime(reader["DepartureDate"]),
                                    ReturnDate = Convert.ToDateTime(reader["ReturnDate"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    IsDeleted = reader["IsDeleted"] as bool?
                                };
                            }

                            // 2. Destination Leads
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    result.Destinations.Add(new Destinationlead
                                    {
                                        LeadId = Convert.ToInt32(reader["LeadId"]),
                                        TourDestination = reader["TourDestination"]?.ToString(),
                                        TourType = reader["TourType"]?.ToString(),
                                        CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                                        CheckInTime = (TimeSpan)reader["CheckInTime"],
                                        CheckoutDate = Convert.ToDateTime(reader["CheckoutDate"]),
                                        CheckoutTime = (TimeSpan)reader["CheckoutTime"],
                                        TotalCharges = Convert.ToInt32(reader["TotalCharges"]),
                                        TotalChargesPerPerson = Convert.ToInt32(reader["TotalChargesPerPerson"]),
                                        CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                        ModifyBy = Convert.ToInt32(reader["ModifyBy"]),
                                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                                        IsDeleted = reader["IsDeleted"] as bool?
                                    });
                                }
                            }

                            // 3. Travellers
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    result.Travellers.Add(new Traveller
                                    {
                                        LeadId = reader["LeadId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LeadId"]),
                                        FirstName = reader["FirstName"]?.ToString(),
                                        LastName = reader["LastName"]?.ToString(),
                                        Gender = reader["Gender"]?.ToString(),
                                        Age = reader["Age"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Age"]),
                                        ContactNumber = reader["ContactNumber"]?.ToString(),
                                        EmailId = reader["EmailId"]?.ToString(),
                                        Address = reader["Address"]?.ToString(),
                                        VisaStatus = reader["VisaStatus"]?.ToString(),
                                        Nationality = reader["Nationality"]?.ToString(),
                                        PassportNumber = reader["PassportNumber"]?.ToString(),
                                        UploadedPassport = reader["UploadedPassport"]?.ToString(),
                                        VisaType = reader["VisaType"]?.ToString(),
                                        TravelInsurance = reader["TravelInsurance"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["TravelInsurance"]),
                                        EmergencyContactNumber = reader["EmergencyContactNumber"]?.ToString(),
                                        EmergencyContactName = reader["EmergencyContactName"]?.ToString(),
                                        SpecialNotes = reader["SpecialNotes"]?.ToString(),
                                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedBy"]),
                                        ModifyBy = reader["ModifyBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifyBy"]),
                                        IsActive = reader["IsActive"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsActive"]),
                                        IsDeleted = reader["IsDeleted"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsDeleted"])
                                    });
                                }
                            }

                            // 4. Transportations
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    result.Transportations.Add(new Transportations
                                    {
                                        LeadId = reader["LeadId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LeadId"]),
                                        FromCountry = reader["FromCountry"]?.ToString(),
                                        FromCity = reader["FromCity"]?.ToString(),
                                        ToCountry = reader["ToCountry"]?.ToString(),
                                        ToCity = reader["ToCity"]?.ToString(),
                                        ModeOfTravell = reader["ModeOfTravell"]?.ToString(),
                                        PartnerName = reader["PartnerName"]?.ToString(),
                                        DepartureDate = reader["DepartureDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DepartureDate"]),
                                        DepartureTime = reader["DepartureTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["DepartureTime"],
                                        ArrivalDate = reader["ArrivalDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ArrivalDate"]),
                                        ArrivalTime = reader["ArrivalTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["ArrivalTime"],
                                        SpecialNotes = reader["SpecialNotes"]?.ToString(),
                                        TotalCharge = reader["TotalCharge"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TotalCharge"]),
                                        TotalChargePerson = reader["TotalChargePerson"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TotalChargePerson"]),
                                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedBy"]),
                                        ModifyBy = reader["ModifyBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifyBy"]),
                                        IsActive = reader["IsActive"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsActive"]),
                                        IsDeleted = reader["IsDeleted"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsDeleted"])
                                    });
                                }
                            }


                            // 5. Hotels
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    result.Hotels.Add(new Hotel
                                    {
                                        LeadId = reader["LeadId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LeadId"]),
                                        FromCountry = reader["FromCountry"]?.ToString(),
                                        FromCity = reader["FromCity"]?.ToString(),
                                        HotelName = reader["HotelName"]?.ToString(),
                                        ForPeople = reader["ForPeople"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ForPeople"]),
                                        ForNights = reader["ForNights"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ForNights"]),
                                        CheckInDate = reader["CheckInDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CheckInDate"]),
                                        CheckInTime = reader["CheckInTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["CheckInTime"],
                                        CheckoutDate = reader["CheckoutDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CheckoutDate"]),
                                        CheckoutTime = reader["CheckoutTime"] == DBNull.Value ? (TimeSpan?)null : (TimeSpan)reader["CheckoutTime"],
                                        BreakFastInclude = reader["BreakFastInclude"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["BreakFastInclude"]),
                                        MealPreference = reader["MealPreference"]?.ToString(),
                                        TotalCharges = reader["TotalCharges"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalCharges"]),
                                        TotalChargesPerPerson = reader["TotalChargesPerPerson"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalChargesPerPerson"]),
                                        SpecialNotes = reader["SpecialNotes"]?.ToString(),
                                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedBy"]),
                                        ModifyBy = reader["ModifyBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifyBy"]),
                                        IsActive = reader["IsActive"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsActive"]),
                                        IsDeleted = reader["IsDeleted"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsDeleted"])
                                    });
                                }
                            }


                            // 6. Miscellaneous
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    result.MiscellaneousItems.Add(new Miscellaneouss
                                    {
                                        LeadId = reader["LeadId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LeadId"]),
                                        Activity = reader["Activity"]?.ToString(),
                                        ActivityCountry = reader["ActivityCountry"]?.ToString(),
                                        ActivityCity = reader["ActivityCity"]?.ToString(),
                                        TotalCharges = reader["TotalCharges"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TotalCharges"]),
                                        TotalChargesPerPerson = reader["TotalChargesPerPerson"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TotalChargesPerPerson"]),
                                        ApplicableToAll = reader["ApplicableToAll"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["ApplicableToAll"]),
                                        CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                                        CreatedBy = reader["CreatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedBy"]),
                                        ModifyBy = reader["ModifyBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifyBy"]),
                                        IsActive = reader["IsActive"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsActive"]),
                                        IsDeleted = reader["IsDeleted"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["IsDeleted"])
                                    });
                                }
                            }

                        }
                    }
                }

                return Ok(new { status_code = 100, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status_code = 500, message = ex.Message });
            }
        }


        [Route("api/DeleteMultipleLeads")]
        [HttpPost]
        public IActionResult DeleteLeads([FromQuery]int leadId)
        {
            try
            {
                using (var connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var command = new MySqlCommand("usp_DeleteLeadData", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_LeadId", leadId);

                    command.ExecuteNonQuery();
                    return Ok(new { success = true, message = "Lead deleted successfully." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred.", error = ex.Message });
            }
        }
    }
}