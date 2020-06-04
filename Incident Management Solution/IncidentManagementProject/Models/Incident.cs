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
        public IncidentCategoriesViewModel IncidentCategory { get; set; }
        public Employee Employee { get; set; }

    }
}