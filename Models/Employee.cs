using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementPortal.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }


        public int Department_Id { get; set; }
        public int Job_Id { get; set; }


        public virtual Department Department { get; set; }
        public virtual Job Job { get; set; }
    }
}