﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Microsoft.Ajax.Utilities;
using NewDemoProject.Models;
using NewDemoProject.ViewModel;
using System.Security;
using IncidentManagementProject.Common;

namespace IncidentManagementProject.Controllers
{
    public  class TrackController : Controller
    {
        OnSubmittingIncident onSubmittingIncident = new OnSubmittingIncident();
        // GET: Track
        [LogExceptions]
        public ActionResult OnCreatingIncident()
        {
            
            int employeeID =Convert.ToInt32(Session["employeeID"]);
            Incident incident = new Incident();
            DataSet ds = onSubmittingIncident.GetIncidents(employeeID);
            List<Incident> incidentlists = new List<Incident>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Incident incidentlist = new Incident();
                incidentlist.Incident_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["incident_id"]);
                incidentlist.Title =Convert.ToString(ds.Tables[0].Rows[i]["incident_title"]);
                incidentlist.Description =Convert.ToString(ds.Tables[0].Rows[i]["incident_description"]);
                incidentlist.raised_by =Convert.ToString(ds.Tables[0].Rows[i]["raised_by"]);
                incidentlist.status =Convert.ToString(ds.Tables[0].Rows[i]["stage"]);
                incidentlist.SupportedBy =Convert.ToString(ds.Tables[0].Rows[i]["handler_name"]);
                if (!Convert.IsDBNull(ds.Tables[0].Rows[i]["created_on"]))
                    incidentlist.created_on = Convert.ToDateTime(ds.Tables[0].Rows[i]["created_on"]);
                if (!Convert.IsDBNull(ds.Tables[0].Rows[i]["altered_on"]))
                    incidentlist.altered_on = Convert.ToDateTime(ds.Tables[0].Rows[i]["altered_on"]);
                incidentlist.Employee.Name = Convert.ToString(ds.Tables[0].Rows[i]["employee_name"]);
                incidentlists.Add(incidentlist);
            }
            incident.incidentarray = incidentlists;
            return View("DisplayIncidents", incident);
        }

        [LogExceptions]
        public ActionResult TrackStatus(int incident_id)
        {
            Incident incident = new Incident();
            DataSet ds = onSubmittingIncident.TrackIncident(incident_id);
            List<Incident> incidentlists = new List<Incident>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Incident incidentlist = new Incident();
                incidentlist.Title = ds.Tables[0].Rows[i]["incident_title"].ToString();
                incidentlist.SupportedBy = ds.Tables[0].Rows[i]["handler_name"].ToString();
                incidentlist.status = ds.Tables[0].Rows[i]["current status"].ToString();
                incidentlists.Add(incidentlist);

            }
            incident.incidentarray = incidentlists;
                return View(incident);
        }
    }
}