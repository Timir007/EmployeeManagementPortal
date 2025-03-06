using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementPortal.Models
{
    public class SalarySlip
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Salary_Date { get; set; }


        public int Employee_Id { get; set; }


        public virtual Employee Employee { get; set; }
    }
}