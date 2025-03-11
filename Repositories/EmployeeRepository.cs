using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseConnector _context;

        public EmployeeRepository(DatabaseConnector context)
        {
            _context = context;
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public IEnumerable<Leave> GetLeavesByEmployeeId(int employeeId)
        {
            return _context.Leaves.Where(l => l.Employee_Id == employeeId).ToList();
        }

        public IEnumerable<SalarySlip> GetSalariesByEmployeeId(int employeeId)
        {
            return _context.SalarySlips.Where(s => s.Employee_Id == employeeId).ToList();
        }

    }
}