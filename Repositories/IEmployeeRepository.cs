using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int id);
        IEnumerable<Leave> GetLeavesByEmployeeId(int employeeId);
        IEnumerable<SalarySlip> GetSalariesByEmployeeId(int employeeId);
    }
}