﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class RequestDetailVM
    {
        public int ReqId { get; set; }
        public int ReqLineNo { get; set; }
        public string ItemCode { get; set; }
        public int ReqQty { get; set; }
        public int AwaitQty { get; set; }
        public int FulfilledQty { get; set; }
        public virtual Item Item { get; set; }
        public virtual Request Request { get; set; }
        public string Status { get; set; }
        public int EmpId { get; set; }
    }
}