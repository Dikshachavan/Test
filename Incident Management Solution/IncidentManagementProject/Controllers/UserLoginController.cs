﻿using System;
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
                    Session["employeeID"] = Convert.ToInt32(sdr["employee_id"]);
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

        public ActionResult getQuestion(int EmployeeID)
        {
            PwReset pw = new PwReset();
            SqlCommand command = new SqlCommand(DBConstants.get_question, sqlconn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@employee_id", EmployeeID);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                pw.question =Convert.ToString(dataSet.Tables[0].Rows[i]["secret_question"]);

            }
            return View(pw);
        }

        public ActionResult getAnswer(string answer, int EmployeeID)
        {
            PwReset pw = new PwReset();
            SqlCommand command = new SqlCommand(DBConstants.get_answer, sqlconn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@employee_id", EmployeeID);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                pw.answer =Convert.ToString(dataSet.Tables[0].Rows[i]["answer"]);
                pw.password =Convert.ToString(dataSet.Tables[0].Rows[i]["employee_password"]);

            }
            if (pw.answer == answer)
            {
                return View(pw);
            }
            else return View("wrongAnswer");



        }
    }

}
