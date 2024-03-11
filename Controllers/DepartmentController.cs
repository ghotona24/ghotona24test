using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using MySqlConnectionCRUD.Models;

namespace MySqlConnectionCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId,
                            DepartmentName from Department";

                DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ËmployeeAppCon");
            MySqlDataReader myReader;

            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand mySqlCommand = new MySqlCommand(query,mycon)) 
                {
                    myReader=mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();  
                }

            }

            return new JsonResult(table);      
        }


        [HttpPost]
        public JsonResult Post(department dep)
        {
            string query = @"insert into 
                             Department(DepartmentName) Values (@DepartmentName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ËmployeeAppCon");
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(department dep)
        {
            string query = @"update
                             Department set DepartmentName=@DepartmentName
                               where DepartmentId=@DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ËmployeeAppCon");
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    mySqlCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from
                             Department 
                               where DepartmentId=@DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ËmployeeAppCon");
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@DepartmentId", id);
                
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
