using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.Models;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class SupplierBL
    {
        //get AllSupplier list 
        public static List<SupplierList> GetAllSupp(string itemCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<SupplierList> suppList = entities.SupplierList.ToList<SupplierList>();
                return suppList;
            }
        }
    }
}