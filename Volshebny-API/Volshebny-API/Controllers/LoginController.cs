using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using Volshebny_API.Models;

namespace Volshebny_API.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [Route("api/AdminLogin")]
        [HttpPost]
        public IActionResult LoginAdminFranchisee(LoginAdmin loginadmin)
        {
            try
            {
                int result;
                ServiceRequestProcessor processor = new ServiceRequestProcessor();
                APIResult apiResult;
                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("usp_login", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_UserId", loginadmin.UserId);
                    command.Parameters.AddWithValue("p_Password", loginadmin.Password);

                    MySqlParameter resultParam = new MySqlParameter("p_Result", MySqlDbType.Int32);
                    resultParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParam);

                    command.ExecuteNonQuery();
                    result = Convert.ToInt32(command.Parameters["p_Result"].Value);


                    if (result == 1)
                    {

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<SuperAdminlogin> SuperAdminList = new List<SuperAdminlogin>();

                                while (reader.Read())
                                {
                                    var supadminModel = new SuperAdminlogin
                                    {
                                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetString("UserId"),
                                        Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString("Password"),
                                        IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? false : reader.GetBoolean("IsActive"),
                                        UserType = reader.IsDBNull(reader.GetOrdinal("UserType")) ? null : reader.GetString("UserType"),
                                        FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString("FirstName"),
                                        LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString("LastName"),
                                        result = result,
                                    };
                                    SuperAdminList.Add(supadminModel);
                                }
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                apiResult.data = SuperAdminList;
                                return Ok(apiResult);
                            }
                            else
                            {
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, ServiceRequestProcessor.StatusCode.DataNotFound.ToString());
                                apiResult.data = new { result = result };
                                return NotFound(apiResult);
                            }
                        }
                    }
                    else if (result == 2)
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<Adminlogin> AdminList = new List<Adminlogin>();

                                while (reader.Read())
                                {
                                    var adminModel = new Adminlogin
                                    {
                                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetString("UserId"),
                                        Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString("Password"),
                                        IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? false : reader.GetBoolean("IsActive"),
                                        UserType = reader.IsDBNull(reader.GetOrdinal("UserType")) ? null : reader.GetString("UserType"),
                                        FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString("FirstName"),
                                        LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString("LastName"),
                                        result = result,
                                    };
                                    AdminList.Add(adminModel);
                                }
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                apiResult.data = AdminList;
                                return Ok(apiResult);
                            }
                            else
                            {
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, ServiceRequestProcessor.StatusCode.DataNotFound.ToString());
                                apiResult.data = new { result = result };
                                return NotFound(apiResult);
                            }
                        }
                    }

                    if (result == 3)
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<SaleUserLogin> SalesUserList = new List<SaleUserLogin>();

                                while (reader.Read())
                                {
                                    var salesuserModel = new SaleUserLogin
                                    {
                                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetString("UserId"),
                                        Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString("Password"),
                                        IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? false : reader.GetBoolean("IsActive"),
                                        UserType = reader.IsDBNull(reader.GetOrdinal("UserType")) ? null : reader.GetString("UserType"),
                                        FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString("FirstName"),
                                        LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString("LastName"),
                                        result = result,
                                    };
                                    SalesUserList.Add(salesuserModel);
                                }
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                apiResult.data = SalesUserList;
                                return Ok(apiResult);
                            }
                            else
                            {
                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, ServiceRequestProcessor.StatusCode.DataNotFound.ToString());
                                apiResult.data = new { result = result };
                                return NotFound(apiResult);
                            }
                        }
                    }
                    else if (result == 0)
                    {
                        apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, ServiceRequestProcessor.StatusCode.DataNotFound.ToString());
                        apiResult.data = new { result = result };
                        return NotFound(apiResult);
                    }
                    else
                    {
                        apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, ServiceRequestProcessor.StatusCode.DataNotFound.ToString());
                        apiResult.data = new { result = result };
                        return NotFound(apiResult);
                    }
                }
            }
            catch (Exception ex)
            {
                ServiceRequestProcessor oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
                throw;
            }
        }
    }
}
