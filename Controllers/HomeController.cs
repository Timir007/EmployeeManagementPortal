using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal.Controllers
{
    public class HomeController : Controller
    {
        readonly DatabaseConnector connector = new DatabaseConnector();
        public ActionResult Index()
        {
            var employeeDetails = connector.Employees.FirstOrDefault();
            return View(employeeDetails);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}