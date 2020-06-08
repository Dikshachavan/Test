﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewDemoProject.Models
{
    public class BusinessFunctionType
    {
        public int Business_ID { get; set; }
        [Required(ErrorMessage = "Please select Business Function")]
        public string Name { get; set; }
    }
}