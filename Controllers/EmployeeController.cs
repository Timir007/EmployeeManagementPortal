using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DatabaseConnector _context;
        public EmployeeController()
        {
            _context = new DatabaseConnector();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Jobs = _context.Jobs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (!_context.Departments.Any(d => d.Id == employee.Department_Id))
            {
                ModelState.AddModelError("Department_Id", "The Department doesn't Exists!!!");
            }

            if (!_context.Jobs.Any(j => j.Id == employee.Job_Id))
            {
                ModelState.AddModelError("Job_Id", "The Job_Title doesn't Exists!!!");
            }

            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }

            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Jobs = _context.Jobs.ToList();
            return View(employee);
        }

        [HttpGet]
        public ActionResult Details()
        {
            var employeeDetail = _context.Employees.ToList();
            return View(employeeDetail);
        }

        public ActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Departments = _context.Departments.ToList();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            // Check if the Department_Id exists
            if (!_context.Departments.Any(d => d.Id == employee.Department_Id))
            {
                ModelState.AddModelError("Department_Id", "The selected Department does not exist.");
            }

            // Check if the Job_Id exists
            if (!_context.Jobs.Any(j => j.Id == employee.Job_Id))
            {
                ModelState.AddModelError("Job_Id", "The selected Job does not exist.");
            }

            if (ModelState.IsValid)
            {
                _context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details");
            }

            ViewBag.Departments = _context.Departments.ToList();
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}