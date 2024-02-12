using FamilyTreeV1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using System.Collections.Generic;
using System;
using System.Linq;

namespace FamilyTreeV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatedFamilyController : ControllerBase
    {
        /// <summary>
        /// Configuration object
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public UpdatedFamilyController(IConfiguration configuration)
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

                List<int> ParentsIds = new List<int>();

                //Get Descendants List

                using SqlCommand commandDescendantsList = new SqlCommand("GetDescendantsStoredProcedure", connection);
                commandDescendantsList.CommandType = CommandType.StoredProcedure;
                commandDescendantsList.Parameters.AddWithValue("@IdentityNumber", identityNumber);

                SqlDataAdapter adapterDescendantsList = new SqlDataAdapter(commandDescendantsList);
                DataSet dataSetDescendantsList = new DataSet();
                adapterDescendantsList.Fill(dataSetDescendantsList);

                // Convert the DataSet to a list of MyData objects
                List<Person> dataDescendantsList = ConvertDescendantsDataSetToList(dataSetDescendantsList);
                List<TreeData> finalDescendantsList = new List<TreeData>();


                foreach (Person descendant in dataDescendantsList)
                {
                    TreeData familyMember = new TreeData();

                    familyMember.key = descendant.Id;
                    familyMember.name = descendant.Name;
                    familyMember.gender = descendant.IdentityNumber;

                    if(ParentsIds == null || ParentsIds.Count == 0)
                    {
                        familyMember.parent = 0;
                    }
                    else
                    {
                        if(ParentsIds.Contains((int)descendant.FatherId))
                        {
                            familyMember.parent =  (int)descendant.FatherId;
                        }
                        else if (ParentsIds.Contains((int)descendant.MotherId))
                        {
                            familyMember.parent = (int)descendant.MotherId;
                        }
                        else
                        {
                            familyMember.parent = 0;
                        }
                    }

                    ParentsIds.Add(descendant.Id);

                    finalDescendantsList.Add(familyMember);
                }
                
                string json = JsonConvert.SerializeObject(finalDescendantsList, Formatting.Indented);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Convert Partner DataSet To List
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        private List<Person> ConvertDescendantsDataSetToList(DataSet dataSet)
        {
            // Assuming you have a class named MyData with properties matching your columns
            List<Person> dataList = new List<Person>();

            string format = "MM/dd/yyyy HH:mm:ss";

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Person data = new Person
                {
                    Id = (string.IsNullOrEmpty(Convert.ToString(row["Id"]))) ? 0 : Convert.ToInt32(row["Id"]),
                    FatherId = (string.IsNullOrEmpty(Convert.ToString(row["FatherId"]))) ? 0 : Convert.ToInt32(row["FatherId"]),
                    MotherId = (string.IsNullOrEmpty(Convert.ToString(row["MotherId"]))) ? 0 : Convert.ToInt32(row["MotherId"]),
                    Name = Convert.ToString(row["Name"]),
                    Surname = Convert.ToString(row["Surname"]),
                    BirthDate = (string.IsNullOrEmpty(Convert.ToString(row["FatherId"]))) ? null : DateTime.ParseExact(Convert.ToString(row["BirthDate"]), format, CultureInfo.InvariantCulture),
                    IdentityNumber = Convert.ToString(row["IdentityNumber"])
                };

                dataList.Add(data);
            }

            return dataList;
        }

    }
}
