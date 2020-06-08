using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncidentManagementProject.Models
{
    public class SR_CategoryType
    {
        public int Category_ID { get; set; }
        [Required(ErrorMessage = "Please select Category")]
        public string Name { get; set; }
    }
}