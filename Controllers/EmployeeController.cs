using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementPortal.Models;
using EmployeeManagementPortal.Models.ViewModels;
using EmployeeManagementPortal.Repositories;

namespace EmployeeManagementPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DatabaseConnector _context;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController()
        {
            _context = new DatabaseConnector();
            _employeeRepository = new EmployeeRepository(_context);
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
        public ActionResult Create(Employee employee, HttpPostedFileBase Photo_Url)
        {
            if (!_context.Departments.Any(d => d.Id == employee.Department_Id))
            {
                ModelState.AddModelError("Department_Id", "The Department doesn't Exist!!!");
            }

            if (!_context.Jobs.Any(j => j.Id == employee.Job_Id))
            {
                ModelState.AddModelError("Job_Id", "The Job_Title doesn't Exist!!!");
            }

            if (ModelState.IsValid)
            {
                if (Photo_Url != null && Photo_Url.ContentLength > 0)
                {
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(Photo_Url.FileName)}";
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    Photo_Url.SaveAs(path);
                    employee.Photo_Url = $"/Images/{fileName}";
                }

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
        public ActionResult Edit(Employee employee, HttpPostedFileBase Photo_Url)
        {
            if (!_context.Departments.Any(d => d.Id == employee.Department_Id))
            {
                ModelState.AddModelError("Department_Id", "The selected Department does not exist.");
            }

            if (!_context.Jobs.Any(j => j.Id == employee.Job_Id))
            {
                ModelState.AddModelError("Job_Id", "The selected Job does not exist.");
            }
            var modelEmployee = _context.Employees.Find(employee.Id);
            if (modelEmployee == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if (Photo_Url != null && Photo_Url.ContentLength > 0)
                {
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(Photo_Url.FileName)}";
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    Photo_Url.SaveAs(path);
                    employee.Photo_Url = $"/Images/{fileName}";
                }
                else
                {
                    employee.Photo_Url = modelEmployee.Photo_Url;
                }
                modelEmployee.First_Name = employee.First_Name;
                modelEmployee.Last_Name = employee.Last_Name;
                modelEmployee.Email = employee.Email;
                modelEmployee.Gender = employee.Gender;
                modelEmployee.Department_Id = employee.Department_Id;
                modelEmployee.Job_Id = employee.Job_Id;

                _context.Entry(modelEmployee).State = System.Data.Entity.EntityState.Modified;
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


        public ActionResult Detail(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var leaves = _employeeRepository.GetLeavesByEmployeeId(id);
            var salaries = _employeeRepository.GetSalariesByEmployeeId(id);


            var viewModel = new EmployeeViewModel
            {
                Employee_Id = employee.Id,
                First_Name = employee.First_Name,
                Last_Name = employee.Last_Name,
                Email = employee.Email,
                Photo_Url = employee.Photo_Url,
                Salaries = salaries.ToList(),
                Leaves = leaves.ToList()
            };

            return View("Detail", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var uploadPath = Server.MapPath("~/UploadedImages");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                // Generate a unique file name to avoid conflicts
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);

                file.SaveAs(path);

            }

            return View();
        }
    }
}






//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Create(Employee employee)
//{
//    if (!_context.Departments.Any(d => d.Id == employee.Department_Id))
//    {
//        ModelState.AddModelError("Department_Id", "The Department doesn't Exists!!!");
//    }

//    if (!_context.Jobs.Any(j => j.Id == employee.Job_Id))
//    {
//        ModelState.AddModelError("Job_Id", "The Job_Title doesn't Exists!!!");
//    }

//    if (ModelState.IsValid)
//    {
//        _context.Employees.Add(employee);
//        _context.SaveChanges();
//        return RedirectToAction("Details");
//    }

//    ViewBag.Departments = _context.Departments.ToList();
//    ViewBag.Jobs = _context.Jobs.ToList();
//    return View(employee);
//}



//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Edit(Employee employee)
//{
//    if (!_context.Departments.Any(d => d.Id == employee.Department_Id))
//    {
//        ModelState.AddModelError("Department_Id", "The selected Department does not exist.");
//    }

//    if (!_context.Jobs.Any(j => j.Id == employee.Job_Id))
//    {
//        ModelState.AddModelError("Job_Id", "The selected Job does not exist.");
//    }

//    if (ModelState.IsValid)
//    {
//        _context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
//        _context.SaveChanges();
//        return RedirectToAction("Details");
//    }

//    ViewBag.Departments = _context.Departments.ToList();
//    return View(employee);
//}
