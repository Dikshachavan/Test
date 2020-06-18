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
                    Session["employeeName"] = Convert.ToString(sdr["employee_mail_id"]);
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


        public ActionResult ResetPassword()
        {
            PwReset pw = new PwReset();
            return View(pw);
        }

        public ActionResult getQuestion(int employee_id)
        {
            PwReset pw = new PwReset();
            SqlCommand command = new SqlCommand(DBConstants.get_question, sqlconn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@employee_id", employee_id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                pw.question = dataSet.Tables[0].Rows[i]["secret_question"].ToString();
                
            }
                return View(pw);
        }

        public string getAnswer(string answer,int employee_id)
        {
            PwReset pw = new PwReset();
            SqlCommand command = new SqlCommand(DBConstants.get_answer, sqlconn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@employee_id", employee_id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                pw.answer = dataSet.Tables[0].Rows[i]["answer"].ToString();
                pw.password = dataSet.Tables[0].Rows[i]["password"].ToString();
               
            }
            if (pw.answer == answer)
            {
                return "true";
            }
            else return "false";

            
            
        }
    }
}
