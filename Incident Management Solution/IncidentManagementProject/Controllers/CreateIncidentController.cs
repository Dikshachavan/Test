using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Microsoft.Ajax.Utilities;
using NewDemoProject.Models;
using NewDemoProject.ViewModel;

namespace NewDemoProject.Controllers
{
    public class CreateIncidentController : Controller
    {
        IncidentCategories IncidentCategories=new IncidentCategories();
        // GET: CreateIncident
        public ActionResult CreateIncident()
        {
            Incident incident = new Incident();
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
            //SelectList items = new SelectList(list, "Value", "Text");
            ViewBag.BusinessFunctionValues = list;
            //incident.IncidentCategory.BusinessFunctionTypes=list.ToList();
            return View();
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
    }
}