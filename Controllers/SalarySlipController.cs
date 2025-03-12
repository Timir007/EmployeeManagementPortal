using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementPortal.Models;

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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalarySlip salarySlip)
        {
            if (!_context.Employees.Any(e => e.Id == salarySlip.Employee_Id))
            {
                ModelState.AddModelError("Employee_Id", "Employee Doesn't Exists!!!");
            }

            if (ModelState.IsValid)
            {
                _context.SalarySlips.Add(salarySlip);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(salarySlip);
        }


        [HttpGet]
        public ActionResult Details()
        {
            var salarySlipDetail = _context.SalarySlips.ToList();
            return View(salarySlipDetail);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var salarySlip = _context.SalarySlips.Find(id);
            if (salarySlip == null)
            {
                return HttpNotFound();
            }

            return View(salarySlip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SalarySlip salarySlip)
        {

            var modelSalarySlip = _context.SalarySlips.Find(id);
            if (modelSalarySlip == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                modelSalarySlip.Salary_Date = salarySlip.Salary_Date;
                modelSalarySlip.Amount = salarySlip.Amount;

                _context.Entry(modelSalarySlip).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Detail", "Employee", new { id = modelSalarySlip.Employee_Id });
            }

            return View(modelSalarySlip);
        }

        public ActionResult Delete(int id)
        {
            var salarySlip = _context.SalarySlips.Find(id);
            if (salarySlip == null)
            {
                return HttpNotFound();
            }
            return View(salarySlip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var salarySlip = _context.SalarySlips.Find(id);
            if (salarySlip == null)
            {
                return HttpNotFound();
            }

            _context.SalarySlips.Remove(salarySlip);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
    }
}