﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class ItemVM
    {
        public string ItemCode { get; set; }
        public string Cat { get; set; }
        public string Desc { get; set; }
        public string Location { get; set; }
        public string UOM { get; set; }
        public bool IsActive { get; set; }
        public int Balance { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQty { get; set; }
        public Nullable<int> TempQtyDisb { get; set; }
        public Nullable<int> TempQtyCheck { get; set; }
        public string SuppCode1 { get; set; }
        public Nullable<double> Price1 { get; set; }
        public string SuppCode2 { get; set; }
        public Nullable<double> Price2 { get; set; }
        public string SuppCode3 { get; set; }
        public Nullable<double> Price3 { get; set; }
    }
}