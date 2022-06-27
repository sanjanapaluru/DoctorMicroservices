using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public DiagnosisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPut]
        public JsonResult Put(Diagnosis D)
        {
            string query = @"update dbo.patient_details set diagnosis = @DiagnosisReport where patient_id = @PatientId";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("searchappcon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    /* updating the DiagnosisReport*/
                    myCommand.Parameters.AddWithValue("@DiagnosisReport", D.DiagnosisReport);

                    /* PatientId*/
                    myCommand.Parameters.AddWithValue("@PatientId", D.PatientId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Added Successfully");

        }
    }
}
