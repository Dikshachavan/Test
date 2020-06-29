using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Constants;

namespace DataAccessLayer
{   
    public class IncidentCategories
    {
        public static string MainConn = ConfigurationManager.ConnectionStrings["IncidentManagement"].ConnectionString;
        SqlConnection connection = new SqlConnection(MainConn);
        DBConstants DBConstants = new DBConstants();

        public DataSet GetBusinesFunction()
        {
            try
            {
                SqlCommand command = new SqlCommand(DBConstants.Incident_Business_Function, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }            
        }
        public DataSet GetServices(int Business_id)
        {
            try
            {
                SqlCommand command = new SqlCommand(DBConstants.Inc_Service_Values, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Inc_Business_id", Business_id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }            
        }
        public DataSet GetCategories(int Service_id)
        {
            try
            {
                SqlCommand command = new SqlCommand(DBConstants.Inc_Category_Values, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Inc_Service_id", Service_id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }            
        }
        public DataSet GetEmployeeDetails(int EmployeeID)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(DBConstants.Get_Employee_Details, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_id", EmployeeID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

        }
        

    }
}
