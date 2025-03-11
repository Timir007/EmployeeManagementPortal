using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementPortal.Models;
using EmployeeManagementPortal.Repositories;

namespace EmployeeManagementPortal.Controllers
{
    public class LeaveController : Controller
    {
        private readonly DatabaseConnector _context;

        public LeaveController()
        {
            _context = new DatabaseConnector();
        }

        int id;
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Leave leave)
        {
            ViewBag.id = id;

            var item = new Leave
            {
                Leave_Date = leave.Leave_Date,
                Reason = leave.Reason,
                Employee_Id = ViewBag.id,
            };
            _context.Leaves.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Detail");


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var leave = _context.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }

            return View(leave);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Leave leave)
        {

            var modelLeave = _context.Leaves.Find(id);
            if (modelLeave == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                modelLeave.Reason = leave.Reason;
                modelLeave.Leave_Date = leave.Leave_Date;

                _context.Entry(modelLeave).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Detail", "Employee", new { id = modelLeave.Employee_Id });
            }

            return View(modelLeave);
        }

        public ActionResult Delete(int id)
        {
            var leave = _context.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var leave = _context.Leaves.Find(id);
            int emp_Id = leave.Employee_Id;
            if (leave == null)
            {
                return HttpNotFound();
            }

            _context.Leaves.Remove(leave);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Employee", new { id = emp_Id });
        }
    }
}