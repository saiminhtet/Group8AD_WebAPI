﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class ReportItemVM
    {
        public DateTime Period { get; set; }
        public string Label { get; set; }
        public double Val1 { get; set; }
        public double Val2 { get; set; }
        public string color { get; set; }
    }
}