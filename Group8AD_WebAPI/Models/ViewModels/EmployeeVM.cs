using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class EmployeeVM
    {
        public int EmpId { get; set; }
        public string DeptCode { get; set; }
        public string EmpName { get; set; }
        public string EmpAddr { get; set; }
        public string EmpEmail { get; set; }
        public string EmpCtcNo { get; set; }
        public string Role { get; set; }
    }
}