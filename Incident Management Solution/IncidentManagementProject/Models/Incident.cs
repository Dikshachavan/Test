using NewDemoProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewDemoProject.Models
{
    public class Incident
    {
        public int Incident_ID { get; set; }
        [Required]
        [Display(Name ="Incident Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Incident Description")]
        public string Description { get; set; }
        public string raised_by { get; set; }

        public DateTime? created_on { get; set; }

        public DateTime? altered_on { get; set; }

        public string status { get; set; }

        public string SupportedBy { get; set; }
        public IncidentCategoriesViewModel IncidentCategory { get; set; }
        public Employee Employee { get; set; }

        public List<Incident> incidentarray { get; set; }

    }
}