using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Cosmos;
using StuudentEntry.Models;
using Newtonsoft.Json;

namespace StuudentEntry.Controllers
{
    public class StudentController : Controller
    {
        public string EndpointUrl = "https://studententry.documents.azure.com:443/";

        /// The primary key for the Azure DocumentDB account.
        public string PrimaryKey = "yKZkUFZylIA7Z44PO2eMKj9G54CqdtZjNAFRYSrFk60lEibmB3OhjaNSrwiEtha4OXH0tmq8tWNEBhqeaRoxDw==";

        // GET: Student
        //public ActionResult Index()
        //{

        //    return View();
        //}

        [HttpGet]
        public ActionResult Register()
        {


            return View();
        }
        [HttpPost]
        public async Task<string> Register(Student s)
        {

            //SqlConnection sqlConnection = new SqlConnection();
            //string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            //SqlCommand sqlCommand = new SqlCommand();
            //sqlConnection.ConnectionString = connectionString;
            //sqlCommand.CommandType = CommandType.Text;
            //sqlCommand.CommandText = "insert into student values ("+s.Id+','+"'"+s.Name+"'"+","+"'"+s.Status+"'"+")";
            //sqlCommand.Connection = sqlConnection;
            //sqlConnection.Open();
            //sqlCommand.ExecuteNonQuery();
            //sqlConnection.Close();

            CosmosDBConnection dbconn = new CosmosDBConnection();
            await dbconn.GetStartedDemoAsync(s,EndpointUrl,PrimaryKey);

            return "Student Registered successfully";
        }

       
        [HttpGet]
        public ActionResult RegisterMarks()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterMarks(Marks s)
        {
            double student_percent;
            int average_marks;
           int marks1= (int)Convert.ToInt64(s.marks1);
            int marks2= (int)Convert.ToInt64(s.marks2);
            int marks3 = (int)Convert.ToInt64(s.marks3);

            average_marks = marks1 + marks2 + marks3;
            student_percent = average_marks*100/300;
           string finalpercent= Convert.ToString(student_percent);
            s.percent = finalpercent;

            CosmosDBConnection dbconn = new CosmosDBConnection();
            await dbconn.MarksAdder(EndpointUrl, PrimaryKey, s);

            //SqlConnection sqlConnection = new SqlConnection();
            //string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            //SqlCommand sqlCommand = new SqlCommand();
            //sqlConnection.ConnectionString = connectionString;
            //sqlCommand.CommandType = CommandType.Text;
            //sqlCommand.CommandText = "insert into marks values (" + s.Id + ',' +  student_percent + ")";
            //sqlCommand.Connection = sqlConnection;
            //sqlConnection.Open();
            //sqlCommand.ExecuteNonQuery();
            //sqlConnection.Close();
            return View();
        }
    }
}