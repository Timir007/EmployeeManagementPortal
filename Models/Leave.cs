using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementPortal.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public string Leave_Date { get; set; }
        public string Reason { get; set; }


        public int Employee_Id { get; set; }


        public virtual Employee Employee { get; set; }
    }
}