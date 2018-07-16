using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class AdjustmentVM
    {
        public string VoucherNo { get; set; }
        public int EmpId { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public string ItemCode { get; set; }
        public string Reason { get; set; }
        public int QtyChange { get; set; }
        public string Status { get; set; }
        public int ApproverId { get; set; }
        public string ApproverComment { get; set; }
    }
}