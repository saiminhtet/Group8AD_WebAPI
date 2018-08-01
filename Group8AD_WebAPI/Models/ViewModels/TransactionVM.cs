using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Author: Tang Shenqi: A0114523U

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class TransactionVM
    {
        public int TranId { get; set; }
        public DateTime TranDateTime { get; set; }
        public string ItemCode { get; set; }
        public int QtyChange { get; set; }
        public double UnitPrice { get; set; }
        public string Desc { get; set; }
        public string DeptCode { get; set; }
        public string SuppCode { get; set; }
        public string VoucherNo { get; set; }

        public double Chargeback { get; set; }
        public double Balance { get; set; }
    }
}