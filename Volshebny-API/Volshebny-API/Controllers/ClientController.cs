using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using Volshebny_API.Models;

namespace Volshebny_API.Controllers
{
     //[Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ClientController(IConfiguration config)
        {
            _config = config;
        }

        [Route("api/CommonClient")]
        [HttpPost]
        public IActionResult Client([FromBody]List<Client> clients)
        {
            APIResult apiResult;
            ServiceRequestProcessor processor = new ServiceRequestProcessor();
            List<object> resultList = new List<object>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    foreach (var client in clients)
                    {
                        MySqlCommand command = new MySqlCommand("usp_commonclient", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("_Id", client.Id);
                        command.Parameters.AddWithValue("_FirstName", client.FirstName);
                        command.Parameters.AddWithValue("_LastName", client.LastName);
                        command.Parameters.AddWithValue("_MobileNo", client.MobileNo);
                        command.Parameters.AddWithValue("_CompanyName", client.CompanyName);
                        command.Parameters.AddWithValue("_EmailId", client.EmailId);
                        command.Parameters.AddWithValue("_IsGSTIN", client.IsGSTIN);
                        command.Parameters.AddWithValue("_GSTNumber", client.GSTNumber);
                        command.Parameters.AddWithValue("_Address", client.Address);
                        command.Parameters.AddWithValue("_Landmark", client.Landmark);
                        command.Parameters.AddWithValue("_CountryId", client.CountryId);
                        command.Parameters.AddWithValue("_StateId", client.StateId);
                        command.Parameters.AddWithValue("_CityId", client.CityId);
                        command.Parameters.AddWithValue("_Pincode", client.Pincode);
                        command.Parameters.AddWithValue("_CreatedBy", client.CreatedBy);
                        command.Parameters.AddWithValue("_CreatedDate", client.CreatedDate);
                        command.Parameters.AddWithValue("_ModifiedBy", client.ModifiedBy);
                        command.Parameters.AddWithValue("_GSTCertificate", client.GSTCertificate);
                        command.Parameters.AddWithValue("_ModifiedDate", client.ModifiedDate);
                        command.Parameters.AddWithValue("_IsActive", client.IsActive);
                        command.Parameters.AddWithValue("_IsDeleted", client.IsDeleted);
                        command.Parameters.AddWithValue("_SpType", client.SpType);

                        MySqlParameter resultParam = new MySqlParameter("_Result", MySqlDbType.Int32);
                        resultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParam);

                        if (client.SpType == "C" || client.SpType == "U" || client.SpType == "D")
                        {
                            command.ExecuteNonQuery();
                            int result = Convert.ToInt32(command.Parameters["_Result"].Value);

                            resultList.Add(new
                            {
                                ClientId = client.Id,
                                Result = result
                            });
                        }
                        else if (client.SpType == "E" || client.SpType == "R")
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    List<Client> clientList = new List<Client>();

                                    while (reader.Read())
                                    {
                                        var clientModel = new Client
                                        {
                                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : null,
                                            FirstName = reader["FirstName"]?.ToString(),
                                            LastName = reader["LastName"]?.ToString(),
                                            MobileNo = reader["MobileNo"]?.ToString(),
                                            CompanyName = reader["CompanyName"]?.ToString(),
                                            EmailId = reader["EmailId"]?.ToString(),
                                            IsGSTIN = reader["IsGSTIN"] != DBNull.Value ? Convert.ToBoolean(reader["IsGSTIN"]) : (bool?)null,
                                            GSTNumber = reader["GSTNumber"]?.ToString(),
                                            Address = reader["Address"]?.ToString(),
                                            Landmark = reader["Landmark"]?.ToString(),
                                            CountryId = reader["CountryId"] != DBNull.Value ? Convert.ToInt32(reader["CountryId"]) : (int?)null,
                                            StateId = reader["StateId"] != DBNull.Value ? Convert.ToInt32(reader["StateId"]) : (int?)null,
                                            CityId = reader["CityId"] != DBNull.Value ? Convert.ToInt32(reader["CityId"]) : (int?)null,
                                            Pincode = reader["Pincode"]?.ToString(),
                                            CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : (int?)null,
                                            CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                                            ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                                            ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                                            IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : (bool?)null
                                        };

                                        clientList.Add(clientModel);
                                    }

                                    apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                    apiResult.data = clientList;
                                    return Ok(apiResult);
                                }
                                else
                                {
                                    apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.DataNotFound, "Data Not Found");
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

                    apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                    apiResult.data = resultList;
                    return Ok(apiResult);
                }
            }
            catch (Exception ex)
            {
                ServiceRequestProcessor errorProcessor = new ServiceRequestProcessor();
                return BadRequest(errorProcessor.onError(ex.Message));
            }
        }

        [Route("api/ManageDestination")]
        [HttpPost]
        public IActionResult ManageDestination([FromBody] Destination destination)
        {
            try
            {
                int result;
                ServiceRequestProcessor processor = new ServiceRequestProcessor();
                APIResult apiResult;

                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("usp_commonDestination", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("_DestinationId", destination.DestinationId);
                    command.Parameters.AddWithValue("_DestinationName", destination.DestinationName);
                    command.Parameters.AddWithValue("_CountryId", destination.CountryId);
                    command.Parameters.AddWithValue("_CityId", destination.CityId);
                    command.Parameters.AddWithValue("_TicketPrice", destination.TicketPrice);
                    command.Parameters.AddWithValue("_CreatedBy", destination.CreatedBy);
                    command.Parameters.AddWithValue("_ModifyBy", destination.ModifyBy);
                    command.Parameters.AddWithValue("_CreatedDate", destination.CreatedDate);
                    command.Parameters.AddWithValue("_ModifiedDate", destination.ModifiedDate);
                    command.Parameters.AddWithValue("_IsActive", destination.IsActive);
                    command.Parameters.AddWithValue("_IsDeleted", destination.IsDeleted);
                    command.Parameters.AddWithValue("_SpType", destination.SpType);

                    MySqlParameter resultParam = new MySqlParameter("_Result", MySqlDbType.Int32);
                    resultParam.Direction = System.Data.ParameterDirection.Output;
                    command.Parameters.Add(resultParam);

                    if (destination.SpType == "C" || destination.SpType == "U" || destination.SpType == "D")
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
                    else if (destination.SpType == "E" || destination.SpType == "R")
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<Destination> destinationList = new List<Destination>();

                                while (reader.Read())
                                {
                                    var dest = new Destination
                                    {
                                        DestinationId = Convert.ToInt32(reader["DestinationId"]),
                                        DestinationName = reader["DestinationName"]?.ToString(),
                                        CountryId = Convert.ToInt32(reader["CountryId"]),
                                        CityId = Convert.ToInt32(reader["CityId"]),
                                        TicketPrice = Convert.ToDecimal(reader["TicketPrice"]),
                                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : (int?)null,
                                        ModifyBy = reader["ModifyBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifyBy"]) : (int?)null,
                                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                        ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    };

                                    destinationList.Add(dest);
                                }

                                apiResult = (APIResult)processor.customeMessge((int)ServiceRequestProcessor.StatusCode.Success, ServiceRequestProcessor.StatusCode.Success.ToString());
                                apiResult.data = destinationList;
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

        [Route("api/GetCountries")]
        [HttpGet]
        public IActionResult GetCountries()
        {
            List<CountryModel> countries = new List<CountryModel>();

            using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select CountryId, CountryName from country", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countries.Add(new CountryModel
                        {
                            CountryId = Convert.ToInt32(reader["CountryId"]),
                            CountryName = reader["CountryName"].ToString()
                        });
                    }
                }
            }

            if (countries.Count == 0)
            {
                return NotFound("No countries found.");
            }

            return Ok(countries);
        }

        [Route("api/GetStates/{countryId}")]
        [HttpGet]
        public IActionResult GetStates(int countryId)
        {
            List<StateModel> states = new List<StateModel>();

            using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT StateId, StateName, CountryId FROM tblstate WHERE CountryId = @CountryId", connection);
                command.Parameters.AddWithValue("@CountryId", countryId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        states.Add(new StateModel
                        {
                            StateId = Convert.ToInt32(reader["StateId"]),
                            StateName = reader["StateName"].ToString(),
                             CountryId = Convert.ToInt32(reader["CountryId"]) 
                        });
                    }
                }
            }

            if (states.Count == 0)
            {
                return NotFound("No states found.");
            }

            return Ok(states);
        }

        [Route("api/GetCities/{stateId}")]
        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
            List<CityModel> cities = new List<CityModel>();

            using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select CityId, CityName,StateId FROM tblcity where StateId = @StateId", connection);
                command.Parameters.AddWithValue("@StateId", stateId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cities.Add(new CityModel
                        {
                            CityId = Convert.ToInt32(reader["CityId"]),
                            CityName = reader["CityName"].ToString(),
                            StateId = Convert.ToInt32(reader["StateId"])
                        });
                    }
                }
            }

            if (cities.Count == 0)
            {
                return NotFound("No cities found.");
            }

            return Ok(cities);
        }

        [Route("api/GetCitiesByCountryid/{countryId}")]
        [HttpGet]
        public IActionResult GetCitiesByCountryid(int countryId)
        {
            List<CityModel> cities = new List<CityModel>();

            using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = @"SELECT c.CityId, c.CityName, s.StateId, s.CountryId
                         FROM tblcity c
                         INNER JOIN tblstate s ON c.StateId = s.StateId
                         WHERE s.CountryId = @CountryId";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CountryId", countryId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cities.Add(new CityModel
                        {
                            CityId = Convert.ToInt32(reader["CityId"]),
                            CityName = reader["CityName"].ToString(),
                            StateId = Convert.ToInt32(reader["StateId"])
                        });
                    }
                }
            }

            if (cities.Count == 0)
            {
                return NotFound("No cities found.");
            }

            return Ok(cities);
        }

        [Route("api/GetAllCities")]
        [HttpGet]
        public IActionResult GetAllCities()
        {
            List<CityModel> cities = new List<CityModel>();

            using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = @"SELECT c.CityId, c.CityName, s.StateId
                         FROM tblcity c
                         INNER JOIN tblstate s ON c.StateId = s.StateId";

                MySqlCommand command = new MySqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cities.Add(new CityModel
                        {
                            CityId = Convert.ToInt32(reader["CityId"]),
                            CityName = reader["CityName"].ToString(),
                            StateId = Convert.ToInt32(reader["StateId"])
                        });
                    }
                }
            }

            if (cities.Count == 0)
            {
                return NotFound("No cities found.");
            }

            return Ok(cities);
        }
    }
}
