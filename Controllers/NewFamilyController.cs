﻿using FamilyTreeV1.Model;
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

                // Get Partner List

                using SqlCommand commandPartnerList = new SqlCommand("GetPartnerStoredProcedure", connection);
                commandPartnerList.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapterPartnerList = new SqlDataAdapter(commandPartnerList);
                DataSet dataSetPartnerList = new DataSet();
                adapterPartnerList.Fill(dataSetPartnerList);

                // Convert the DataSet to a list of MyData objects
                List<Partner> dataPartnerList = ConvertPartnerDataSetToList(dataSetPartnerList);

                //Get Descendants List

                using SqlCommand commandDescendantsList = new SqlCommand("GetDescendantsStoredProcedure", connection);
                commandDescendantsList.CommandType = CommandType.StoredProcedure;
                commandDescendantsList.Parameters.AddWithValue("@IdentityNumber", identityNumber);

                SqlDataAdapter adapterDescendantsList = new SqlDataAdapter(commandDescendantsList);
                DataSet dataSetDescendantsList = new DataSet();
                adapterDescendantsList.Fill(dataSetDescendantsList);

                // Convert the DataSet to a list of MyData objects
                List<Person> dataDescendantsList = ConvertDescendantsDataSetToList(dataSetDescendantsList);
                List<Person> finalDescendantsList = new List<Person>();


                foreach (Person descendant in dataDescendantsList)
                {
                    int descendantId = descendant.Id;

                    var WomenId = dataPartnerList.Where(x => x.FatherId == descendantId).Select(y => y.MotherId).FirstOrDefault();

                    List<int> partnerIdsList = new List<int>();

                    if (WomenId != null)
                    {
                        // Add the new PartnerId to the list
                        partnerIdsList.Add((int)WomenId);
                    }

                    var ManId = dataPartnerList.Where(x => x.MotherId == descendantId).Select(y => y.FatherId).FirstOrDefault();

                    if (ManId != null)
                    {
                        // Add the new PartnerId to the list
                        partnerIdsList.Add((int)ManId);
                    }

                    if(partnerIdsList != null && partnerIdsList.Count > 0)
                    {
                        // Convert the list back to an array and update the Person instance
                        descendant.PartnerIds = partnerIdsList.ToArray();
                    }
                    

                    finalDescendantsList.Add(descendant);
                }


                /*

                //////////////////////////////
                foreach (var dp in dataPartnerList)
                {
                    int? ManId = dp.FatherId;
                    int? WomenId = dp.MotherId;

                    var DescendantItem1 = dataDescendantsList.Where(x => x.Id == ManId).FirstOrDefault();

                    if (DescendantItem1 != null)
                    {
                        DescendantItem1.PartnerIds[0] = WomenId;

                        finalDescendantsList.Add(DescendantItem1);
                    }

                    var DescendantItem2 = dataDescendantsList.Where(x => x.Id == WomenId).FirstOrDefault();

                    if (DescendantItem2 != null)
                    {
                        DescendantItem2.PartnerIds[0] = ManId;

                        finalDescendantsList.Add(DescendantItem2);
                    }
                }

                */
                
                string json = JsonConvert.SerializeObject(finalDescendantsList, Formatting.Indented);
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

        /// <summary>
        /// Convert Partner DataSet To List
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        private List<Partner> ConvertPartnerDataSetToList(DataSet dataSet)
        {
            // Assuming you have a class named MyData with properties matching your columns
            List<Partner> dataList = new List<Partner>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Partner data = new Partner
                {
                    FatherId = (string.IsNullOrEmpty(Convert.ToString(row["FatherId"]))) ? 0 : Convert.ToInt32(row["FatherId"]),
                    MotherId = (string.IsNullOrEmpty(Convert.ToString(row["MotherId"]))) ? 0 : Convert.ToInt32(row["MotherId"])
                };

                dataList.Add(data);
            }

            return dataList;
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
