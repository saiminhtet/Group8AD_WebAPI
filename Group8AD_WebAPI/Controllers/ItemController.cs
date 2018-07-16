using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.Models;

namespace Group8AD_WebAPI.Controllers
{
    public class ItemController : ApiController
    {
        [HttpPost]
        public Item GetItems(string cat, string desc)
        {
            return Item.GetItems(cat, desc);
        }

        [HttpPost]
        public RequestDetail GetCurrReqDets(int empId)
        {
            return GetCurrReqDets(empId);
        }

    }

    
}