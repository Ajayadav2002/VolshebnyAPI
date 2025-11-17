using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Volshebny_API.Models;

namespace Volshebny_API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [Route("api/CommonUser")]
        [HttpPost]
        public IActionResult Users([FromBody]User user)
        {
            try
            {
                int result;
                ServiceRequestProcessor processor = new ServiceRequestProcessor();
                APIResult apiResult;

                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("usp_commonuser", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("_Id", user.Id);
                    command.Parameters.AddWithValue("_FirstName", user.FirstName);
                    command.Parameters.AddWithValue("_LastName", user.LastName);
                    command.Parameters.AddWithValue("_MobileNo", user.MobileNo);
                    command.Parameters.AddWithValue("_CompanyName", user.CompanyName);
                    command.Parameters.AddWithValue("_EmailId", user.EmailId);
                    command.Parameters.AddWithValue("_IsGSTIN", user.IsGSTIN);
                    command.Parameters.AddWithValue("_GSTNumber", user.GSTNumber);
                    command.Parameters.AddWithValue("_Address", user.Address);
                    command.Parameters.AddWithValue("_Landmark", user.Landmark);
                    command.Parameters.AddWithValue("_CountryId", user.CountryId);
                    command.Parameters.AddWithValue("_StateId", user.StateId);
                    command.Parameters.AddWithValue("_CityId", user.CityId);
                    command.Parameters.AddWithValue("_Pincode", user.Pincode);
                    command.Parameters.AddWithValue("_CreatedBy", user.CreatedBy);
                    command.Parameters.AddWithValue("_CreatedDate", user.CreatedDate);
                    command.Parameters.AddWithValue("_ModifiedBy", user.ModifiedBy);
                    command.Parameters.AddWithValue("_ModifiedDate", user.ModifiedDate);
                    command.Parameters.AddWithValue("_IsActive", user.IsActive);
                    command.Parameters.AddWithValue("_Authority", user.Authority);
                    command.Parameters.AddWithValue("_IsDeleted", user.IsDeleted);
                    command.Parameters.AddWithValue("_SpType", user.SpType);

                    MySqlParameter resultParam = new MySqlParameter("_Result", MySqlDbType.Int32);
                    resultParam.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(resultParam);

                    if (user.SpType == "C" || user.SpType == "U" || user.SpType == "D")
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
                    else if (user.SpType == "E" || user.SpType == "R")
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<User> userlist = new List<User>();

                                while (reader.Read())
                                {
                                    var userModel = new User
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                        LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                        MobileNo = reader["MobileNo"] != DBNull.Value ? reader["MobileNo"].ToString() : null,
                                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                        EmailId = reader["EmailId"] != DBNull.Value ? reader["EmailId"].ToString() : null,
                                        IsGSTIN = reader["IsGSTIN"] != DBNull.Value ? Convert.ToBoolean(reader["IsGSTIN"]) : (bool?)null,
                                        GSTNumber = reader["GSTNumber"] != DBNull.Value ? reader["GSTNumber"].ToString() : null,
                                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                                        Landmark = reader["Landmark"] != DBNull.Value ? reader["Landmark"].ToString() : null,
                                        CountryId = reader["CountryId"] != DBNull.Value ? Convert.ToInt32(reader["CountryId"]) : (int?)null,
                                        StateId = reader["StateId"] != DBNull.Value ? Convert.ToInt32(reader["StateId"]) : (int?)null,
                                        CityId = reader["CityId"] != DBNull.Value ? Convert.ToInt32(reader["CityId"]) : (int?)null,
                                        Pincode = reader["Pincode"] != DBNull.Value ? reader["Pincode"].ToString() : null,
                                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : (int?)null,
                                        CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                                        ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                                        ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                                        Authority = reader["Authority"] != DBNull.Value ? reader["Authority"].ToString() : null,
                                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : (bool?)null,
                                    };

                                    userlist.Add(userModel);
                                }
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                apiResult.data = userlist;
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
    }
}
