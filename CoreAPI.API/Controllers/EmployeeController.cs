using CoreAPI.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.API.Controllers
{
    /// <summary>
    /// EmployeeController's summary
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnviroment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="webHostEnviroment"></param>
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment webHostEnviroment)
        {
            _configuration = configuration;
            _webHostEnviroment = webHostEnviroment;
        }

        /// <summary>
        /// Get operation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"select EmployeeId, EmployeeName, Department,
                                    convert(varchar(10), DateOfJoining, 120) DataOfJoining,
                                    PhotoFileName
                               from dbo.Employee";
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

            return new JsonResult(table);
        }

        /// <summary>
        /// Post operation
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"insert into dbo.Employee (EmployeeName, Department, DateOfJoining, PhotoFileName)
                             values ('" + emp.EmployeeName + @"', '" + emp.Department + @"', '" + emp.DateOfJoining + @"', '" + emp.PhotoFileName + @"')";
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
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"update dbo.Employee 
                                set EmployeeName = '" + emp.EmployeeName + @"', 
                                    Department = '" + emp.Department + @"', 
                                    DateOfJoining = '" + emp.DateOfJoining + @"'
                              where EmployeeId = " + emp.EmployeeId;
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
            string query = @"delete from dbo.Employee where EmployeeId = " + Id;
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

        /// <summary>
        /// Save file operation
        /// </summary>
        /// <returns></returns>
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _webHostEnviroment.ContentRootPath + "/Photos/" + fileName;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        /// <summary>
        /// Custom operation
        /// </summary>
        /// <returns></returns>
        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            //To get all details avoid next code, better, use entity framework o stored procedure with parameters.
            string query = @"select DepartmentName from dbo.Department";
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

            return new JsonResult(table);
        }
    }
}
