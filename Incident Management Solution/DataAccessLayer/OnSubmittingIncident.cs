using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Constants;

namespace DataAccessLayer
{
     public class OnSubmittingIncident
    {
        public static string MainConn = ConfigurationManager.ConnectionStrings["IncidentManagement"].ConnectionString;
        SqlConnection connection = new SqlConnection(MainConn);
        static DBConstants DBConstants = new DBConstants();

        public DataSet GetIncidents(string mail)
        {
            try
            {
                SqlCommand command = new SqlCommand(DBConstants.List_of_incidents, connection);
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

        public DataSet TrackIncident(int incident_id)
        {
            try
            {
                SqlCommand command = new SqlCommand(DBConstants.Track_Incident, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@incident_id", incident_id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;

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
