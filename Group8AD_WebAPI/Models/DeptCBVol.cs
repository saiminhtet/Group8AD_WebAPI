using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models
{
    public class DeptCBVol
    {
        public string DeptCode { get; set; }
        public double ChargeBack { get; set; }
        public int Volume { get; set; }
    }
}