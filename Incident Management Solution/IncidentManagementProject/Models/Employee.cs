using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewDemoProject.Models
{
    public class Employee
    {
        [Display(Name = "Email ID")]
        public string Email_ID { get; set; }
        [Display(Name ="Employee ID")]
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        [Display(Name = "Contact Number")]
        public string Contact_Number { get; set; }
        public string Password { get; set; }
        public int Project_ID { get; set; }
        [Display(Name ="First Level Manager Name")]
        public string First_Level_Manager_Name { get; set; }
        [Display(Name ="Second Level Manager")]
        public string Second_Level_Manager_Name { get; set; }
        public Project Project { get; set; }
    }
}