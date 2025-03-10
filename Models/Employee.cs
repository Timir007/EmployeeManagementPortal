using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPortal.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string Last_Name { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Photo_Url { get; set; }


        public int Department_Id { get; set; }
        public int Job_Id { get; set; }

        [ForeignKey("Department_Id")]
        public virtual Department Department { get; set; }

        [ForeignKey("Job_Id")]
        public virtual Job Job { get; set; }
    }
}