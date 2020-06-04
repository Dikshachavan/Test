using NewDemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDemoProject.ViewModel
{
    public class IncidentCategoriesViewModel
    {
        public List<BusinessFunctionType> BusinessFunctionTypes { get; set; }
        public ServicesType ServicesTypes { get; set; }
        public CategoryType CategoryTypes { get; set; }
    }
}