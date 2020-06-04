using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Razor.Text;

namespace NewDemoProject.Models
{
    public class Project
    {
        public int Project_ID { get; set; }
        [Display(Name ="Project")]
        public string Project_Name { get; set; }
        public string Department_Name { get; set; }
        public string  Location { get; set; }
        [Display(Name ="Workstation")]
        public string Workstation_Number { get; set; }
        [Display(Name = "Extension Number")]
        public string Extension_Number { get; set; }
    }
}