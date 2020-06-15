using NewDemoProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using DataAccessLayer;

namespace NewDemoProject.Controllers
{
    public class ProfileController : Controller
    {
        public static string mainConn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(mainConn);
        public static IncidentCategories IncidentCategories = new IncidentCategories();
        // GET: Profile
        public ActionResult Index()
        {
            EmployeeProjectViewModel employeeProjectViewModel = new EmployeeProjectViewModel();
            employeeProjectViewModel.Employees.Email_ID = Convert.ToString(Session["username"]);
            DataSet ds = IncidentCategories.GetEmployeeDetails(Convert.ToString(Session["username"]));
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                employeeProjectViewModel.Employees.Employee_ID = Convert.ToInt32(dataRow["employee_id"]);
                employeeProjectViewModel.Employees.Name = Convert.ToString(dataRow["employee_name"]);
                employeeProjectViewModel.Employees.Contact_Number = Convert.ToString(dataRow["contact_number"]);
                employeeProjectViewModel.Project.Project_Name = Convert.ToString(dataRow["project_name"]);
                employeeProjectViewModel.Project.Workstation_Number = Convert.ToString(dataRow["workstation_number"]);
                employeeProjectViewModel.Project.Extension_Number = Convert.ToString(dataRow["extension_number"]);
                employeeProjectViewModel.Project.Department_Name = Convert.ToString(dataRow["department_name"]);
                employeeProjectViewModel.Project.Location = Convert.ToString(dataRow["location"]);
            }
            return View(employeeProjectViewModel);
        }
        [HttpPost]
        public ActionResult SaveProfileDetails(EmployeeProjectViewModel employeeProject)
        {
            try
            {
                SqlCommand command = new SqlCommand("save_employee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_mail_id", Session["username"]);
                command.Parameters.AddWithValue("@employee_id", employeeProject.Employees.Employee_ID);
                command.Parameters.AddWithValue("@employee_name", employeeProject.Employees.Name);
                command.Parameters.AddWithValue("@contact_number", employeeProject.Employees.Contact_Number);
                //command.Parameters.AddWithValue("@project_id", employeeProject.Project.Project_ID);
                command.Parameters.AddWithValue("@project_name", employeeProject.Project.Project_Name);
                command.Parameters.AddWithValue("@department_name", employeeProject.Project.Department_Name);
                command.Parameters.AddWithValue("@location", employeeProject.Project.Location);
                command.Parameters.AddWithValue("@workstation_number", employeeProject.Project.Workstation_Number);
                command.Parameters.AddWithValue("@extention_number", employeeProject.Project.Extension_Number);

                connection.Open();
                command.ExecuteNonQuery();
                Index();
                return View("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            
        }
    }
}