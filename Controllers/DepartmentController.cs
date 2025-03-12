using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DatabaseConnector _context;

        public DepartmentController()
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
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(department);
        }


        [HttpGet]
        public ActionResult Details()
        {
            var departmentDetail = _context.Departments.ToList();
            return View(departmentDetail);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department department)
        {

            var modelSalarySlip = _context.Departments.Find(id);
            if (modelSalarySlip == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                modelSalarySlip.Department_Name = department.Department_Name;
                _context.Entry(modelSalarySlip).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(modelSalarySlip);
        }

        public ActionResult Delete(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
    }
}