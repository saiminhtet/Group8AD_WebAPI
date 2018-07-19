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
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Transaction> translist = entities.Transactions.Where(t => t.DeptCode == deptCode).ToList();
                List<TransactionVM> list = new List<TransactionVM>();
                for (int i = 0; i < translist.Count; i++)
                {
                    if (DateTime.Compare(translist[i].TranDateTime, fromDate) >= 0 && DateTime.Compare(translist[i].TranDateTime, toDate) <= 0)
                    {
                        TransactionVM trans = new TransactionVM();
                        trans.TranId = translist[i].TranId;
                        trans.TranDateTime = translist[i].TranDateTime;
                        trans.ItemCode = translist[i].ItemCode;
                        trans.QtyChange = translist[i].QtyChange;
                        trans.UnitPrice = (double)translist[i].UnitPrice;
                        trans.Desc = translist[i].Desc;
                        trans.DeptCode = translist[i].DeptCode;
                        trans.SuppCode = translist[i].SuppCode;
                        trans.VoucherNo = translist[i].VoucherNo;
                        list.Add(trans);
                    }
                }
                return list;
            }
                //// dummy
                //List<TransactionVM> translist = new List<TransactionVM>();
                //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                //{
                //    translist = entities.Transactions.Select(t => new TransactionVM()
                //    {
                //        TranId = t.TranId,
                //        TranDateTime = t.TranDateTime,
                //        ItemCode = t.ItemCode,
                //        QtyChange = t.QtyChange,
                //        //UnitPrice = t.UnitPrice,
                //        Desc = t.Desc,
                //        DeptCode = t.DeptCode,
                //        SuppCode = t.SuppCode
                //    }).ToList<TransactionVM>();
                //}
                //return translist;

            }

        // get chargeback by date range
        public static List<TransactionVM> GetCBByRng(string deptCode, DateTime fromDate, DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Transaction> translist = entities.Transactions.Where(t => t.DeptCode == deptCode).ToList();
                List<TransactionVM> list = new List<TransactionVM>();
                for (int i = 0; i < translist.Count; i++)
                {
                    if (DateTime.Compare(translist[i].TranDateTime, fromDate) >= 0 && DateTime.Compare(translist[i].TranDateTime, toDate) <= 0)
                    {
                        TransactionVM trans = new TransactionVM();
                        trans.TranId = translist[i].TranId;
                        trans.TranDateTime = translist[i].TranDateTime;
                        trans.ItemCode = translist[i].ItemCode;
                        trans.QtyChange = translist[i].QtyChange;
                        trans.UnitPrice = (double)translist[i].UnitPrice;
                        trans.Desc = translist[i].Desc;
                        trans.DeptCode = translist[i].DeptCode;
                        trans.SuppCode = translist[i].SuppCode;
                        trans.VoucherNo = translist[i].VoucherNo;
                        list.Add(trans);
                    }
                }
                return list;
            }
            //List<TransactionVM> translist = new List<TransactionVM>();
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    translist = entities.Transactions.Select(t => new TransactionVM()
            //    {
            //        TranId = t.TranId,
            //        TranDateTime = t.TranDateTime,
            //        ItemCode = t.ItemCode,
            //        QtyChange = t.QtyChange,
            //        //UnitPrice = t.UnitPrice,
            //        Desc = t.Desc,
            //        DeptCode = t.DeptCode,
            //        SuppCode = t.SuppCode
            //    }).ToList<TransactionVM>();
            //}
            //return translist;
        }

        // get last ten transactions by itemCode
        // done
        public static List<TransactionVM> GetLastTenTrans(string itemCode)
        {
            List<TransactionVM> translist = new List<TransactionVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Transaction> list = entities.Transactions.Where(t => t.ItemCode == itemCode).
                    OrderByDescending(t => t.TranDateTime).ToList();
                if (list != null)
                {
                    for (int i = 0; i < 10 && i < list.Count; i++)
                    {
                        TransactionVM t = new TransactionVM();
                        t.TranId = list[i].TranId;
                        t.TranDateTime = list[i].TranDateTime;
                        t.ItemCode = list[i].ItemCode;
                        t.QtyChange = list[i].QtyChange;
                        if (list[i].UnitPrice != null)
                            t.UnitPrice = (double)list[i].UnitPrice;
                        else
                            t.UnitPrice = 0;
                        t.Desc = list[i].Desc;
                        t.DeptCode = list[i].DeptCode;
                        t.SuppCode = list[i].SuppCode;
                        t.VoucherNo = list[i].VoucherNo;
                        translist.Add(t);
                    }
                }
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
                    TranDateTime = t.TranDateTime,
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
                    TranDateTime = t.TranDateTime,
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
        public static void ShowCostReport(string dept1, string dept2, string supp1, string supp2,
            string cat, string type, List<DateTime> dates, bool byMonth)
        {
            return;
        }

        // show volume report
        public static void ShowVolumeReport(string dept1, string dept2, string supp1, string supp2, 
            string cat, string type, List<DateTime> dates, bool byMonth)
        {
            return;
        }
    }
}