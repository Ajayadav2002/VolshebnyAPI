using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Volshebny_API.Models;

namespace Volshebny_API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IConfiguration _config;
        public SupplierController(IConfiguration config)
        {
            _config = config;
        }

        [Route("api/CommonSupplier")]
        [HttpPost]
        public IActionResult Supplier([FromBody]Supplier supplier)
        {
            try
            {
                int result;
                ServiceRequestProcessor processor = new ServiceRequestProcessor();
                APIResult apiResult;

                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("usp_commonsupplier", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("_Id", supplier.Id);
                    command.Parameters.AddWithValue("_FirstName", supplier.FirstName);
                    command.Parameters.AddWithValue("_LastName", supplier.LastName);
                    command.Parameters.AddWithValue("_MobileNo", supplier.MobileNo);
                    command.Parameters.AddWithValue("_CompanyName", supplier.CompanyName);
                    command.Parameters.AddWithValue("_Type", supplier.Type);
                    command.Parameters.AddWithValue("_Country", supplier.Country);
                    command.Parameters.AddWithValue("_EmailId", supplier.EmailId);
                    command.Parameters.AddWithValue("_GSTCertificate", supplier.GSTCertificate);
                    command.Parameters.AddWithValue("_IsGSTIN", supplier.IsGSTIN);
                    command.Parameters.AddWithValue("_GSTNumber", supplier.GSTNumber);
                    command.Parameters.AddWithValue("_Address", supplier.Address);
                    command.Parameters.AddWithValue("_Amount", supplier.Amount);
                    command.Parameters.AddWithValue("_Status", supplier.Status);
                    command.Parameters.AddWithValue("_Landmark", supplier.Landmark);
                    command.Parameters.AddWithValue("_CountryId", supplier.CountryId);
                    command.Parameters.AddWithValue("_StateId", supplier.StateId);
                    command.Parameters.AddWithValue("_CityId", supplier.CityId);
                    command.Parameters.AddWithValue("_Pincode", supplier.Pincode);
                    command.Parameters.AddWithValue("_CreatedBy", supplier.CreatedBy);
                    command.Parameters.AddWithValue("_CreatedDate", supplier.CreatedDate);
                    command.Parameters.AddWithValue("_ModifiedBy", supplier.ModifiedBy);
                    command.Parameters.AddWithValue("_ModifiedDate", supplier.ModifiedDate);
                    command.Parameters.AddWithValue("_IsActive", supplier.IsActive);
                    command.Parameters.AddWithValue("_IsDeleted", supplier.IsDeleted);
                    command.Parameters.AddWithValue("_SpType", supplier.SpType);

                    MySqlParameter resultParam = new MySqlParameter("_Result", MySqlDbType.Int32);
                    resultParam.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(resultParam);

                    if (supplier.SpType == "C" || supplier.SpType == "U" || supplier.SpType == "D")
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
                    else if (supplier.SpType == "E" || supplier.SpType == "R")
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<Supplier> supplierlist = new List<Supplier>();

                                while (reader.Read())
                                {
                                    var supplierModel = new Supplier
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                        LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                        MobileNo = reader["MobileNo"] != DBNull.Value ? reader["MobileNo"].ToString() : null,
                                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                        Type = reader["Type"] != DBNull.Value ? reader["Type"].ToString() : null,
                                        Country = reader["Country"] != DBNull.Value ? reader["Country"].ToString() : null,
                                        EmailId = reader["EmailId"] != DBNull.Value ? reader["EmailId"].ToString() : null,
                                        IsGSTIN = reader["IsGSTIN"] != DBNull.Value ? Convert.ToBoolean(reader["IsGSTIN"]) : (bool?)null,
                                        GSTNumber = reader["GSTNumber"] != DBNull.Value ? reader["GSTNumber"].ToString() : null,
                                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : null,
                                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : (decimal?)null,
                                        Landmark = reader["Landmark"] != DBNull.Value ? reader["Landmark"].ToString() : null,
                                        CountryId = reader["CountryId"] != DBNull.Value ? Convert.ToInt32(reader["CountryId"]) : (int?)null,
                                        StateId = reader["StateId"] != DBNull.Value ? Convert.ToInt32(reader["StateId"]) : (int?)null,
                                        CityId = reader["CityId"] != DBNull.Value ? Convert.ToInt32(reader["CityId"]) : (int?)null,
                                        Pincode = reader["Pincode"] != DBNull.Value ? reader["Pincode"].ToString() : null,
                                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : (int?)null,
                                        CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                                        ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                                        ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : (bool?)null,
                                    };

                                    supplierlist.Add(supplierModel);
                                }
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                apiResult.data = supplierlist;
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

		[HttpPost]
		[Route("api/UpdateInvoiceStatus")]
		public IActionResult UpdateInvoiceStatus([FromBody] Supplier model)
		{
			try
			{
				string connectionString = _config.GetConnectionString("DefaultConnection");

				using (MySqlConnection con = new MySqlConnection(connectionString))
				{
					con.Open();

					string query = "update tblsupplier set Status = @Status where Id = @Id";

					using (MySqlCommand cmd = new MySqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@Status", model.Status);
						cmd.Parameters.AddWithValue("@Id", model.Id);

						int InsertRows = cmd.ExecuteNonQuery();

						if (InsertRows > 0)
						{
							return Ok("Invoice status updated successfully.");
						}
						else
						{
							return NotFound("Invoice not found.");
						}
					}
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex.Message);
			}
		}

	}
}
