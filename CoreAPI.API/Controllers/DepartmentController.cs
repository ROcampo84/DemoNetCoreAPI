using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using CoreAPI.API.Models;

namespace CoreAPI.API.Controllers
{
    /// <summary>
    /// DepartmentController's summary
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor's summary
        /// </summary>
        /// <param name="configuration"></param>
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get operation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppConnection");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCom = new SqlCommand(query, myCon))
                {
                    myReader = myCom.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        /// <summary>
        /// Post operation
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(Department dep)
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"insert into dbo.Department values ('" + dep.DepartmentName + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCom = new SqlCommand(query, myCon))
                {
                    myReader = myCom.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        /// <summary>
        /// Put operation
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult Put(Department dep)
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"update dbo.Department set DepartmentName  = '" + dep.DepartmentName + @"' where DepartmentId = " + dep.DepartmentId;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCom = new SqlCommand(query, myCon))
                {
                    myReader = myCom.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        /// <summary>
        /// Delete operation
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"delete from dbo.Department where DepartmentId = " + Id;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCom = new SqlCommand(query, myCon))
                {
                    myReader = myCom.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
