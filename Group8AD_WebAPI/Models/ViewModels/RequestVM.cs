using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class RequestVM
    {
        public int ReqId { get; set; }
        public int EmpId { get; set; }
        public Nullable<int> ApproverId { get; set; }
        public string ApproverComment { get; set; }
        public Nullable<System.DateTime> ReqDateTime { get; set; }
        public Nullable<System.DateTime> ApprovedDateTime { get; set; }
        public Nullable<System.DateTime> CancelledDateTime { get; set; }
        public Nullable<System.DateTime> FulfilledDateTime { get; set; }
        public string Status { get; set; }
    }
}