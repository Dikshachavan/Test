using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncidentManagementProject.Models
{
    public class PwReset
    {
        [Display(Name = "Secret Question :")]
        public string question { get; set; }
        [Display(Name = "Answer :"  )]
        public string answer { get; set; }

        [Display(Name ="Employee ID")]
        [Required(ErrorMessage = "Please Provide Employee ID before submitting")]
        public int employee_id { get; set; }

        public string password { get; set; }
    }
}