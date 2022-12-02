using CumulutiveProject1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CumulutiveProject1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MyQL Database 
        private SchoolDbContext School = new SchoolDbContext();

        //This controller will access the teachers table of our school database 
        /// <summary>
        /// Contacts the database and Returns a list of Teachers in the system. If a search key
        /// is included we filter where the teachername is like the key 
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first and last names)
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower('%@key%') " +
                "or lower(teacherlname) like lower('%@key%') or lower(concat(teacherfname, ' ', teacherlname)) " +
                "like lower('%@key%')";

            cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers (type Teacher -under the models folder-)
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through each row of the Result Set
            while (ResultSet.Read())
            {
                //Access column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"].ToString());


                Teacher newTeacher = new Teacher();

                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.EmployeeNumber = EmployeeNumber;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

                //Add the author name to the list 
                Teachers.Add(newTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Teachers;


        }


        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection 
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Teachers where teacherid="+id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            
            while (ResultSet.Read())
            {
                //Access column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"].ToString());

                //Teacher newTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            return NewTeacher;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/6</example>
        

        [HttpPost]

        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between server and database 
            Conn.Open();

            //Establish a new command for our database 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

            
        }

    }
}
