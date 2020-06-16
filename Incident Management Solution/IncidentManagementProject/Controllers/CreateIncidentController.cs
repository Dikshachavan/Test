using System;
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

namespace NewDemoProject.Controllers
{
    public class CreateIncidentController : Controller
    {
        public static string MainConn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(MainConn);
        IncidentCategories IncidentCategories=new IncidentCategories();
        Incident incident = new Incident();

        // GET: CreateIncident
        public ActionResult CreateIncident()
        {
            DataSet dataSet= IncidentCategories.GetBusinesFunction();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                list.Add(new SelectListItem
                {
                    Text=dr["name"].ToString(),
                    Value=dr["incident_business_id"].ToString()
                });
            }
            ViewBag.BusinessFunctionValues = list;
            FetchUserDetails(Session["username"].ToString());
            return View(incident);
        }
        public JsonResult GetIncidentServices(int Business_id)
        {
            DataSet dataSet = IncidentCategories.GetServices(Business_id);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                list.Add(new SelectListItem
                {
                    Text=dr["name"].ToString(),
                    Value=dr["incident_service_id"].ToString()
                });
            }
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIncidentCategories(int Service_id)
        {
            DataSet dataSet = IncidentCategories.GetCategories(Service_id);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                list.Add(new SelectListItem
                {
                    Text = dr["name"].ToString(),
                    Value = dr["incident_category_id"].ToString()
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public Incident FetchUserDetails(string Email_id)
        {
            DataSet ds = IncidentCategories.GetEmployeeDetails(Email_id);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                incident.Employee.Employee_ID = Convert.ToInt32(dataRow["employee_id"]);
                incident.Employee.First_Level_Manager_Name = Convert.ToString(dataRow["First_Level_Manager_Name"]);
                incident.Employee.Second_Level_Manager_Name = Convert.ToString(dataRow["Second_Level_Manager_Name"]);
                incident.Employee.Project.Project_Name = Convert.ToString(dataRow["project_name"]);
                incident.Employee.Project.Workstation_Number = Convert.ToString(dataRow["workstation_number"]);
                incident.Employee.Project.Extension_Number = Convert.ToString(dataRow["extension_number"]);
            }
            return incident;

        }

        [ValidateInput(false)]
        public ActionResult SaveCreatedIncidents(Incident incident,FormCollection formdata)
        {
            
            try
            {
                
                string SR_categoryId =null;
                string handlerID=null;
                SqlCommand command = new SqlCommand("save_incident", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@incident_title", incident.Title);
                command.Parameters.AddWithValue("@incident_description", incident.Description);
                command.Parameters.AddWithValue("@incident_category_id", formdata["Category"]);
                command.Parameters.AddWithValue("@category_id", SR_categoryId);
                command.Parameters.AddWithValue("@handler_id", handlerID);
                command.Parameters.AddWithValue("@raised_by", Session["username"]);
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
        
    }
}