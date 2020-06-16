using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace IncidentManagementProject.Common
{
    public class LogExceptions : ActionFilterAttribute,IExceptionFilter
    {
        public void OnException (ExceptionContext filterContext)
        {
            string message = "\n" + filterContext.RouteData.Values["controller"].ToString() + "->" +
                filterContext.RouteData.Values["action"].ToString() + "-> "+ filterContext.Exception.Message +"\t -" + DateTime.Now.ToString() + "\n";
            logexceptions(message);
            logexceptions("-----------------");
        }

        private void logexceptions(string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Data/Data.txt"), data);

        }
    }
}