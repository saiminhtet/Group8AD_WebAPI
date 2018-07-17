﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models.ViewModels
{
    public class NotificationVM
    {
        public int NotificationId { get; set; }
        public System.DateTime NotificationDateTime { get; set; }
        public int FromEmp { get; set; }
        public int ToEmp { get; set; }
        public string RouteUri { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}