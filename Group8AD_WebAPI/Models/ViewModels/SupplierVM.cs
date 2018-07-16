using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class SupplierVM
    {
        public string SuppCode { get; set; }
        public string SuppName { get; set; }
        public string SuppCtcName { get; set; }
        public string SuppCtcNo { get; set; }
        public string SuppFaxNo { get; set; }
        public string SuppAddr { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}