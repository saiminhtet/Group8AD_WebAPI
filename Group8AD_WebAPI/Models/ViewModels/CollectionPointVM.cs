using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Group8AD_WebAPI.Models.ViewModels
{
    public class CollectionPointVM
    {
        public int ColPtId { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public int ClerkId { get; set; }
    }
}