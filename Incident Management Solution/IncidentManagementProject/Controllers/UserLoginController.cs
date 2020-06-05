using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncidentManagementProject.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace IncidentManagementProject.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginClass lc)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand sqlcomm = new SqlCommand("user_login");
            sqlconn.Open();
            sqlcomm.Connection = sqlconn;
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddWithValue("@employee_mail_id", lc.UserName);
            sqlcomm.Parameters.AddWithValue("@password", lc.Password);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                FormsAuthentication.SetAuthCookie(lc.UserName, true);
                Session["username"] = lc.UserName.ToString();
                return RedirectToAction("welcome");
            }
            else
            {

                ViewData["message"] = "Login failed, Invalid User details";
            }
            sqlconn.Close();
            return View();
        }
        public ActionResult welcome()
        {
            return View();
        }
    }
}
