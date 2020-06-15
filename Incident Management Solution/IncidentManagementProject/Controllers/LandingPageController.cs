using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncidentManagementProject.Common;

namespace IncidentManagementProject.Controllers
{
    public class LandingPageController : Controller
    {
        // GET: LandingPage
        [LogExceptions]
        public ActionResult Index()
        {

            return View();
        }
    }
}