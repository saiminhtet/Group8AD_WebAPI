using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Author: Tang Shenqi: A0114523U

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class RequestVM
    {
        public int ReqId { get; set; }
        public int EmpId { get; set; }
        public Nullable<int> ApproverId { get; set; }
        public string ApproverComment { get; set; }

        public DateTime ReqDateTime { get; set; }
        public DateTime ApprovedDateTime { get; set; }
        public DateTime CancelledDateTime { get; set; }
        public DateTime FulfilledDateTime { get; set; }

        public string Status { get; set; }
    }
}