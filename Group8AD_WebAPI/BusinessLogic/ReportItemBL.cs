using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public class ReportItemBL
    {
        // dummy code

        // get chargeback by month
        public static List<TransactionVM> GetCBByMth(string deptCode, DateTime fromDate, DateTime toDate)
        {
            List<TransactionVM> translist = new List<TransactionVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                translist = entities.Transactions.Select(t => new TransactionVM()
                {
                    TranId = t.TranId,
                    TransDateTime = t.TranDateTime,
                    ItemCode = t.ItemCode,
                    QtyChange = t.QtyChange,
                    //UnitPrice = t.UnitPrice,
                    Desc = t.Desc,
                    DeptCode = t.DeptCode,
                    SuppCode = t.SuppCode
                }).ToList<TransactionVM>();
            }
            return translist;
        }

        // get chargeback by date range
        public static List<TransactionVM> GetCBByRng(string deptCode, DateTime fromDate, DateTime toDate)
        {
            List<TransactionVM> translist = new List<TransactionVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                translist = entities.Transactions.Select(t => new TransactionVM()
                {
                    TranId = t.TranId,
                    TransDateTime = t.TranDateTime,
                    ItemCode = t.ItemCode,
                    QtyChange = t.QtyChange,
                    //UnitPrice = t.UnitPrice,
                    Desc = t.Desc,
                    DeptCode = t.DeptCode,
                    SuppCode = t.SuppCode
                }).ToList<TransactionVM>();
            }
            return translist;
        }

        // get last ten transactions by itemCode
        public static List<TransactionVM> GetLastTenTrans(string itemCode)
        {
            List<TransactionVM> translist = new List<TransactionVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                translist = entities.Transactions.Select(t => new TransactionVM()
                {
                    TranId = t.TranId,
                    TransDateTime = t.TranDateTime,
                    ItemCode = t.ItemCode,
                    QtyChange = t.QtyChange,
                    //UnitPrice = t.UnitPrice,
                    Desc = t.Desc,
                    DeptCode = t.DeptCode,
                    SuppCode = t.SuppCode
                }).ToList<TransactionVM>();
            }
            return translist;
        }

        // get annual chargeback
        public static List<TransactionVM> GetCBAnnual(DateTime toDate)
        {
            List<TransactionVM> translist = new List<TransactionVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                translist = entities.Transactions.Select(t => new TransactionVM()
                {
                    TranId = t.TranId,
                    TransDateTime = t.TranDateTime,
                    ItemCode = t.ItemCode,
                    QtyChange = t.QtyChange,
                    //UnitPrice = t.UnitPrice,
                    Desc = t.Desc,
                    DeptCode = t.DeptCode,
                    SuppCode = t.SuppCode
                }).ToList<TransactionVM>();
            }
            return translist;
        }

        // get annual volume
        public static List<TransactionVM> GetVolAnnual(DateTime toDate)
        {
            List<TransactionVM> translist = new List<TransactionVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                translist = entities.Transactions.Select(t => new TransactionVM()
                {
                    TranId = t.TranId,
                    TransDateTime = t.TranDateTime,
                    ItemCode = t.ItemCode,
                    QtyChange = t.QtyChange,
                    //UnitPrice = t.UnitPrice,
                    Desc = t.Desc,
                    DeptCode = t.DeptCode,
                    SuppCode = t.SuppCode
                }).ToList<TransactionVM>();
            }
            return translist;
        }

        // show cost report
        public void ShowCostReport(string dept1, string dept2, string supp1, string supp2,
            string cat, string type, List<DateTime> dates, bool byMonth)
        {
            return;
        }

        // show volume report
        void ShowVolumeReport(string dept1, string dept2, string supp1, string supp2, 
            string cat, string type, List<DateTime> dates, bool byMonth)
        {
            return;
        }
    }
}