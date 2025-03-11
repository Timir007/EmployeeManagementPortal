using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementPortal.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public DateTime Leave_Date { get; set; }
        public string Reason { get; set; }


        public int Employee_Id { get; set; }

        [ForeignKey("Employee_Id")]
        public virtual Employee Employee { get; set; }
    }
}