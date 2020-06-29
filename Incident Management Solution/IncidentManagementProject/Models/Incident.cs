using IncidentManagementProject.Models;
using NewDemoProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewDemoProject.Models
{
    public class Incident
    {
        public int Incident_ID { get; set; }
        [Required(ErrorMessage ="Please Provide Incident Title")]
        [Display(Name ="Incident Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Provide Incident Description")]
        [Display(Name = "Incident Description")]
        [AllowHtml]
        public string Description { get; set; }
        public string raised_by { get; set; }

        public DateTime? created_on { get; set; }

        public DateTime? altered_on { get; set; }

        public string status { get; set; }

        public string SupportedBy { get; set; }
        public IncidentCategoriesViewModel IncidentCategory { get; set; }
        public Employee Employee { get; set; }

        public List<Incident> incidentarray { get; set; }
        public string Attachment { get; set; }
        public IncidentHandler IncidentHandler { get; set; }
        public Incident()
        {
            Employee = new Employee();
            Employee.Project = new Project();
            IncidentCategory = new IncidentCategoriesViewModel();
            IncidentHandler = new IncidentHandler();
        }

    }
}