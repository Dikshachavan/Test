using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
     public class OnSubmittingIncident
    {
        public static string MainConn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(MainConn);

        public DataSet GetIncidents(string mail)
        {
            try
            {
                SqlCommand command = new SqlCommand("listincidents", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_mail_id", mail);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch(Exception ex)
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
