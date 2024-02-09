using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace FamilyTreeV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewFamilyController : ControllerBase
    {
        /// <summary>
        /// Configuration object
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public NewFamilyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get Descendants by Identity Number of Person
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns> return Family list </returns>
        [HttpGet("GetDescendants")]
        public IActionResult GetDescendants(string identityNumber)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                connection.Open();

                using SqlCommand command = new SqlCommand("GetDescendantsStoredProcedure", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdentityNumber", identityNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get Root Ascendant by Identity Number of Person
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns> return Family list </returns>
        [HttpGet("GetRootAscendant")]
        public IActionResult GetRootAscendant(string identityNumber)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                connection.Open();

                using SqlCommand command = new SqlCommand("GetRootAscendantStoredProcedure", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdentityNumber", identityNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);
                return Ok(dataSet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
