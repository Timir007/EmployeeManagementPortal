using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementPortal.Controllers
{
    public class SalarySlipController : Controller
    {
        private readonly DatabaseConnector _context;

        public SalarySlipController()
        {
            _context = new DatabaseConnector();
        }
        [HttpGet]
        public ActionResult Details()
        {
            var salarySlipDetail = _context.SalarySlips.ToList();
            return View(salarySlipDetail);
        }

        //public ActionResult Edit(int id)
        //{
        //    var employee = _context.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.Departments = _context.Departments.ToList();
        //    return View(employee);
        //}
    }
}