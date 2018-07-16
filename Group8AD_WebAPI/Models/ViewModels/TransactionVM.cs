using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class TransactionVM
    {
        public int TranId { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public string ItemCode { get; set; }
        public int QtyChange { get; set; }
        public double UnirPrice { get; set; }
        public string Desc { get; set; }
        public string DeptCode { get; set; }
        public string SuppCode { get; set; }
    }
}