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


namespace IncidentManagementProject.Controllers
{
    public  class TrackController : Controller
    {
        OnSubmittingIncident onSubmittingIncident = new OnSubmittingIncident();
        // GET: Track
        public ActionResult OnCreatingIncident()
        {
            string mail = Session["username"].ToString();
            Incident incident = new Incident();
            DataSet ds = onSubmittingIncident.GetIncidents(mail);
            List<Incident> incidentlists = new List<Incident>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Incident incidentlist = new Incident();
                incidentlist.Incident_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["incident_id"]);
                incidentlist.Title = ds.Tables[0].Rows[i]["incident_title"].ToString();
                incidentlist.Description = ds.Tables[0].Rows[i]["incident_descripton"].ToString();
                incidentlist.raised_by = ds.Tables[0].Rows[i]["raised_by"].ToString();
                incidentlist.status = ds.Tables[0].Rows[i]["stage"].ToString();
                incidentlist.SupportedBy = ds.Tables[0].Rows[i]["handler_name"].ToString();
                if (!Convert.IsDBNull(ds.Tables[0].Rows[i]["created_on"]))
                    incidentlist.created_on = Convert.ToDateTime(ds.Tables[0].Rows[i]["created_on"]);
                if (!Convert.IsDBNull(ds.Tables[0].Rows[i]["altered_on"]))
                    incidentlist.altered_on = Convert.ToDateTime(ds.Tables[0].Rows[i]["altered_on"]);
                incidentlists.Add(incidentlist);
            }
            incident.incidentarray = incidentlists;
            return View(incident);
        }

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