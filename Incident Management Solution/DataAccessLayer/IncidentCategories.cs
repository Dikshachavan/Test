using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{   
    public class IncidentCategories
    {
        public static string MainConn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(MainConn);

        public DataSet GetBusinesFunction()
        {
            try
            {
                SqlCommand command = new SqlCommand("Inc_Business_Function_Values", connection);
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
                SqlCommand command = new SqlCommand("Inc_Service_Values", connection);
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
                SqlCommand command = new SqlCommand("Inc_Category_Values", connection);
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
        public DataSet GetEmployeeDetails(string Email_id)
        {
            try
            {
                SqlCommand command = new SqlCommand("Get_Employee_Details", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@email_id", Email_id);
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
