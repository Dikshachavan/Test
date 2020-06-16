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
using IncidentManagementProject.Common;
using DataAccessLayer.Constants;

namespace IncidentManagementProject.Controllers
{
    public class UserLoginController : Controller
    {
        public static string mainconn = ConfigurationManager.ConnectionStrings["IncidentManagement"].ConnectionString;
        SqlConnection sqlconn = new SqlConnection(mainconn);
        DBConstants DBConstant = new DBConstants();
        // GET: UserLogin
        [LogExceptions]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult Index(LoginClass lc)
        {
            try
            {
                
                SqlCommand sqlcomm = new SqlCommand(DBConstants.User_Login);
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
                    ViewData["message"] = DBConstants.Login_Error_Message;
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

        [LogExceptions]
        public ActionResult welcome()
        {
            return View();
        }
    }
}
