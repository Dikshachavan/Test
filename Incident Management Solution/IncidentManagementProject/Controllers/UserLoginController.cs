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
        public static string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(mainconn);
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginClass lc)
        {
            try
            {
                
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
                    Session["employeeName"] = Convert.ToString(sdr["employee_name"]);
                    return RedirectToAction("Index", "LandingPage");
                }
                else
                {
                    ViewData["message"] = "Opps Login failed, Invalid User details !!";
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlconn.Close();
            }

        }
        public ActionResult welcome()
        {
            return View();
        }
    }
}
