using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IncidentManagementProject.Models
{
    public class LoginClass
    {
        [EmailAddress(ErrorMessage = "Email Address not valid !")]
        [Required(ErrorMessage = "Please enter your Username !")]
        [Display(Name = "Enter Username :")]

        public string UserName
        {
            get; set;
        }

        [Required(ErrorMessage = "Please enter your Password !")]
        [Display(Name = "Enter Password :")]
        [MinLength(5)]
        [DataType(DataType.Password)]

        public string Password
        {
            get; set;
        }
        public string EmployeeName { get; set; }

    }
}
    