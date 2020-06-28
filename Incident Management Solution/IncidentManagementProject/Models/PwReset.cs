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

        [Display(Name ="Employee Mail ID")]
        [Required(ErrorMessage = "Please Provide Employee ID before submitting")]
        public string employee_mail_id { get; set; }

        public string password { get; set; }
        [Display(Name = "Emloyee ID")]
        [Required(ErrorMessage = "Please Provide Employee ID before submitting")]
        public int EmployeeID { get; set; }
    }
}