using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Constants
{
    public class DBConstants
    {
        public const string Incident_Business_Function = "sp_Get_Business_Type_Values";
        public const string Inc_Service_Values = "sp_Get_Service_Values";
        public const string Inc_Category_Values = "sp_Get_Category_Values";
        public const string Get_Employee_Details = "sp_Get_Employee_Details";
        public const string List_of_incidents = "sp_Fetch_Incidents";
        public const string Track_Incident="track_incident";
        public const string Save_Employee= "sp_Save_Employee_Details";
        public const string Save_Incident= "sp_Save_Incidents";
        public const string User_Login = "sp_User_Login";
        public const string Login_Error_Message = "Opps Login failed, Invalid User details !!";
        public const string get_question = "sp_Get_Questions";
        public const string get_answer = "sp_Get_Answers";
    }
}
