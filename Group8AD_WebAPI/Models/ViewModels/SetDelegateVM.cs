using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class SetDelegateVM
    {
        public string DeptCode { get; set; }
        public DateTime DelegateFromDate { get; set; }
        public DateTime DelegateToDate { get; set; }
        public int EmpId { get; set; }
    }
}