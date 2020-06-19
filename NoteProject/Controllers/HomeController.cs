using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using NoteProject.Models;
using System.Data;
using System.Configuration;
namespace NoteProject.Controllers
{
    public class HomeController : Controller
    {
        DatabaseModels db = new DatabaseModels();
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader dr;
        int userID = cParameter.userID;
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TblNote note)
        {


            connection.ConnectionString = cParameter.cnnstring;
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "insert into TblNote (Title, Description, CreationTime, REF_TblUser) Values ('" + note.Title + "','" + note.Description + "' ,'" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + userID + "' )";

            command.Connection = connection;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            ViewBag.Message = "Saved Successfully.";

            return View("Index", new TblNote());

        }

        [HttpGet]
        public ActionResult Note(List<TblNote> note)
        {

            using (DatabaseModels db = new DatabaseModels())
            {

                connection.ConnectionString = cParameter.cnnstring;

                command.Connection = connection;
                int userID = cParameter.userID;
                command.CommandText = "select * from  TblNote where REF_TblUser='" + userID + "' ";
                connection.Open();
                SqlDataReader sdr = command.ExecuteReader();



                List<TblNote> objModel = new List<TblNote>();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        var details = new TblNote();
                        details.Title = sdr["Title"].ToString();
                        details.Description = sdr["Description"].ToString();
                        details.CreationTime = (DateTime)sdr["CreationTime"];
                        details.ID = Convert.ToInt32(sdr["ID"]);
                        objModel.Add(details);
                    }
                    note = objModel;

                    connection.Close();
                }
                return View("Note", note);



            }
        }

        public ActionResult Delete(int id)
        {
            connection.ConnectionString = cParameter.cnnstring;
            connection.Open();
            command.Connection = connection;
            command.CommandText = "delete from  TblNote  WHERE ID= '"+id+"' ";
            command.ExecuteNonQuery();
            connection.Close();


            return RedirectToAction("Note");





        }

        
    }
}