using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class SupplierBL
    {
        //get AllSupplier list 
        public static List<SupplierVM> GetAllSupp(string itemCode)
        {
            List<SupplierVM> supplists = new List<SupplierVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                supplists = (from a in entities.Suppliers
                            from b in entities.Items.Where(b => b.ItemCode == itemCode && a.SuppCode == b.SuppCode1 
                            || b.ItemCode == itemCode && a.SuppCode == b.SuppCode2 
                            || b.ItemCode == itemCode && a.SuppCode == b.SuppCode3)
                            select new SupplierVM
                            {
                                SuppCode = a.SuppCode,
                                SuppName = a.SuppName
                            }).ToList<SupplierVM>();

            }
            return supplists;
        }
        //SupplierList GetAllSupp
        public static List<SupplierVM> GetAllSupp()
        {
            List<SupplierVM> supplists = new List<SupplierVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                supplists = entities.Suppliers.Select(s => new SupplierVM()
                {
                    SuppCode = s.SuppCode,
                    SuppName = s.SuppName,
                    SuppCtcName = s.SuppCtcName,
                    SuppCtcNo = s.SuppCtcNo,
                    SuppFaxNo = s.SuppFaxNo,
                    SuppAddr = s.SuppAddr
                }).ToList<SupplierVM>();
            }
            return supplists;
        }
    }
}