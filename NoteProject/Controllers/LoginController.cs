using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteProject.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace NoteProject.Controllers
{
    public class LoginController : Controller
    {
   
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader dr;


        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(TblUser user)
        {
            connection.ConnectionString = cParameter.cnnstring;
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * from TblUser where UserName ='" + user.UserName + "' and Password='" + user.Password + "' ";
            


            dr = command.ExecuteReader();
            if (dr.Read())
            {
               
                cParameter.userID = Convert.ToInt32(dr["UserID"]);
                connection.Close();

                return Redirect("/Home/Index");
            }
            else
            {
                connection.Close();
                ViewBag.Message = "Incorrect username or password.";
                return View("Login", user);

            }


        }
        


        public ActionResult Logout()
        {
            cParameter.userID = 0;

            return Redirect("/Login/Login");
        }



    }
}