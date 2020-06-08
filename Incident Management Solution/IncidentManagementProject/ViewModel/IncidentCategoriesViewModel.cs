using IncidentManagementProject.Models;
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
        public SR_BusinessFunction SR_BusinessFunctionTypes { get; set; }
        public SR_ServiceType SR_ServiceTypes { get; set; }
        public SR_CategoryType SR_CategoryTypes { get; set; }
        public IncidentCategoriesViewModel()
        {
            BusinessFunctionTypes = new List<BusinessFunctionType>();
            ServicesTypes = new ServicesType();
            CategoryTypes = new CategoryType();
            SR_BusinessFunctionTypes = new SR_BusinessFunction();
            SR_ServiceTypes = new SR_ServiceType();
            SR_CategoryTypes = new SR_CategoryType();
        }
    }
}