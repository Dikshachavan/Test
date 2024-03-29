﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Microsoft.Ajax.Utilities;
using NewDemoProject.Models;
using NewDemoProject.ViewModel;
using System.Text;
using IncidentManagementProject.Common;
using DataAccessLayer.Constants;
using System.IO;

namespace NewDemoProject.Controllers
{
    
    public class CreateIncidentController : Controller
    {
        public static string MainConn = ConfigurationManager.ConnectionStrings["IncidentManagement"].ConnectionString;
        SqlConnection connection = new SqlConnection(MainConn);
        IncidentCategories IncidentCategories=new IncidentCategories();
        Incident incident = new Incident();
        static DBConstants DBConstants = new DBConstants();

        [LogExceptions]
        // GET: CreateIncident
        public ActionResult CreateIncident()
        {
            DataSet dataSet= IncidentCategories.GetBusinesFunction();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                list.Add(new SelectListItem
                {
                    Text=dr["business_name"].ToString(),
                    Value=dr["business_id"].ToString()
                });
            }
            ViewBag.BusinessFunctionValues = list;
            FetchUserDetails(Convert.ToInt32(Session["employeeID"]));
            return View(incident);
        }

        [LogExceptions]
        public JsonResult GetIncidentServices(int Business_id)
        {
            DataSet dataSet = IncidentCategories.GetServices(Business_id);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                list.Add(new SelectListItem
                {
                    Text=dr["service_type_name"].ToString(),
                    Value=dr["service_id"].ToString()
                });
            }
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        [LogExceptions]
        public JsonResult GetIncidentCategories(int Service_id)
        {
            DataSet dataSet = IncidentCategories.GetCategories(Service_id);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                list.Add(new SelectListItem
                {
                    Text = dr["category_name"].ToString(),
                    Value = dr["category_id"].ToString()
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [LogExceptions]
        public Incident FetchUserDetails(int EmployeeID)
        {
            DataSet ds = IncidentCategories.GetEmployeeDetails(EmployeeID);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                incident.Employee.Employee_ID = Convert.ToInt32(dataRow["employee_id"]);
                incident.Employee.First_Level_Manager_Name = Convert.ToString(dataRow["first_level_manager_name"]);
                incident.Employee.Second_Level_Manager_Name = Convert.ToString(dataRow["second_level_manager_name"]);
                incident.Employee.Project.Project_Name = Convert.ToString(dataRow["project_name"]);
                incident.Employee.Project.Workstation_Number = Convert.ToString(dataRow["workstation_number"]);
                incident.Employee.Project.Extension_Number = Convert.ToString(dataRow["extension_number"]);
            }
            return incident;

        }

        [ValidateInput(false)]
        [LogExceptions]
        public ActionResult SaveCreatedIncidents(Incident incident,FormCollection formdata)
        {
            
            try
            {
                //string SR_categoryId =null;
                string handlerID=null;
                SqlCommand command = new SqlCommand(DBConstants.Save_Incident, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@incident_title", incident.Title);
                command.Parameters.AddWithValue("@incident_description", incident.Description);
                command.Parameters.AddWithValue("@category_id", formdata["Category"]);
                //command.Parameters.AddWithValue("@category_id", SR_categoryId);
                command.Parameters.AddWithValue("@handler_id", handlerID);
                command.Parameters.AddWithValue("@raised_by", Session["employeeID"]);
                command.Parameters.AddWithValue("@attachment", incident.Attachment);
                if (incident.created_on == null)
                {
                    incident.created_on = System.DateTime.Now;
                    command.Parameters.AddWithValue("@created_on", incident.created_on);
                }
                else
                {
                    command.Parameters.AddWithValue("@created_on", incident.created_on);
                }
                if (incident.altered_on == null)
                {
                    incident.altered_on = System.DateTime.Now;
                    command.Parameters.AddWithValue("@altered_on", incident.altered_on);
                }
                else
                {
                    command.Parameters.AddWithValue("@altered_on", incident.altered_on);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return RedirectToAction("OnCreatingIncident","Track");
        }
        [HttpPost]
        public void FileUpload()
        {
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    string fileName = Path.GetFileName(Request.Files[i].FileName);
                    fileName = Path.Combine(Server.MapPath("~/File Upload/"), fileName);
                    file.SaveAs(fileName);
                }
            }
        }
        
    }
}