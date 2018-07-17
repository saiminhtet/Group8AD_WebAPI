using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class SetRepVM
    {
        public string DeptCode { get; set; }
        public int FromEmpId { get; set; }
        public int ToEmpId { get; set; }
    }
}