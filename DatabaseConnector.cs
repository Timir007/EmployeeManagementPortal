using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal
{
    public class DatabaseConnector : DbContext
    {
        public DatabaseConnector() : base("name=DefaultConnection")
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<SalarySlip> SalarySlips { get; set; }
    }
}