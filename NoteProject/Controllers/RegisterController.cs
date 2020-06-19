using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteProject.Models;
namespace NoteProject.Controllers
{
    public class RegisterController : Controller
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader dr;

        // GET: User
        public ActionResult Register(int id = 0)
        {
            TblUser user = new TblUser();
            return View(user);
        }
        [HttpPost]
        public ActionResult Register(TblUser user)
        {
      
            connection.ConnectionString = cParameter.cnnstring;
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * from TblUser where UserName ='" + user.UserName + "' ";

            dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                ViewBag.DuplicateMessage = "User Name Already Exists.";
                return View("Register", user);
            }
            else
            {
                addUser(user);
                ModelState.Clear();
                ViewBag.SuccessMessage = "Registration Successful.";
                return View("Register", new TblUser());
            }
            
        }

        void addUser(TblUser user)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataReader dr;
            connection.ConnectionString = cParameter.cnnstring;
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "insert into TblUser (UserName, Password) Values ('" + user.UserName + "','" + user.Password + "')";

            command.Connection = connection;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}