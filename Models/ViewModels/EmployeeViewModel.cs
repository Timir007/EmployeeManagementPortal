using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementPortal.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int Employee_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Photo_Url { get; set; }
        public string Email { get; set; }
        public List<SalarySlip> Salaries { get; set; }
        public List<Leave> Leaves { get; set; }
    }
}