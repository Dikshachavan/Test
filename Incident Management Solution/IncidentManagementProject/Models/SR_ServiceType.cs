using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncidentManagementProject.Models
{
    public class SR_ServiceType
    {
        public int Service_ID { get; set; }
        [Required(ErrorMessage = "Please select Service")]
        public string Name { get; set; }
    }
}