using NewDemoProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace NewDemoProject.Controllers
{
    public class ProfileController : Controller
    {
        public static string mainConn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(mainConn);
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveProfileDetails(EmployeeProjectViewModel employeeProject)
        {
            try
            {
                SqlCommand command = new SqlCommand("save_employee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_mail_id", employeeProject.Employees.Email_ID);
                command.Parameters.AddWithValue("@employee_id", employeeProject.Employees.Employee_ID);
                command.Parameters.AddWithValue("@employee_name", employeeProject.Employees.Name);
                command.Parameters.AddWithValue("@contact_number", employeeProject.Employees.Contact_Number);
                //command.Parameters.AddWithValue();
                command.Parameters.AddWithValue("@project_id", employeeProject.Project.Project_ID);
                command.Parameters.AddWithValue("@project_name", employeeProject.Project.Project_Name);
                command.Parameters.AddWithValue("@department_name", employeeProject.Project.Department_Name);
                command.Parameters.AddWithValue("@location", employeeProject.Project.Location);
                command.Parameters.AddWithValue("@workstation_number", employeeProject.Project.Workstation_Number);
                command.Parameters.AddWithValue("@extention_number", employeeProject.Project.Extension_Number);

                connection.Open();
                command.ExecuteNonQuery();
                return View();
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