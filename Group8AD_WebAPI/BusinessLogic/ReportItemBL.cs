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
        // done
        public static List<ReportItemVM> GetCBByMth(string deptCode, DateTime fromDate, DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                //List<DateTime> monthList = GetMonthList(fromDate, toDate);
                //List<ReportItemVM> riList = new List<ReportItemVM>();
                //for (int i = 0; i < monthList.Count; i++)
                //{
                //    List<Transaction> transList = entities.Transactions.
                //        Where(t => DateTime.Compare(t.TranDateTime, monthList[i]) >= 0 && DateTime.Compare(t.TranDateTime, monthList[i].AddMonths(1)) < 0 
                //        && t.DeptCode == deptCode).ToList();
                //    double chargeBack = 0;
                //    for (int j = 0; j < transList.Count; j++)
                //    {
                //        if (transList[j].UnitPrice != null)
                //        {
                //            chargeBack = chargeBack + transList[j].QtyChange * (double)transList[j].UnitPrice;
                //        }
                //    }
                //    ReportItemVM ri = new ReportItemVM();
                //    ri.Period = monthList[i];
                //    ri.Val1 = chargeBack;
                //    ri.Val2 = 0;
                //    riList.Add(ri);
                //}
                //return riList;
                List<DateTime> monthList = GetMonthList(fromDate, toDate);
                List<ReportItemVM> riList = new List<ReportItemVM>();
                for (int i = 0; i < monthList.Count; i++)
                {
                    List<Transaction> transList = entities.Transactions.ToList();
                    double chargeBack = 0;
                    for (int j = 0; j < transList.Count; j++)
                    {
                        if (transList[j].UnitPrice != null && transList[j].DeptCode == deptCode
                            && DateTime.Compare(transList[j].TranDateTime, monthList[i]) >= 0 
                            && DateTime.Compare(transList[j].TranDateTime, monthList[i].AddMonths(1)) >= 0)
                        {
                            chargeBack = chargeBack + transList[j].QtyChange * (double)transList[j].UnitPrice;
                        }
                    }
                    ReportItemVM ri = new ReportItemVM();
                    ri.Period = monthList[i];
                    ri.Val1 = chargeBack;
                    ri.Val2 = 0;
                    riList.Add(ri);
                }
                return riList;
            }
        }

        // get chargeback by date range
        public static List<TransactionVM> GetCBByRng(string deptCode, DateTime fromDate, DateTime toDate)
        {
            //List<DateTime> monthList = GetMonthList(fromDate, toDate);
            //List<ReportItemVM> riList = new List<ReportItemVM>();
            //for (int i = 0; i < monthList.Count; i++)
            //{
            //    List<Transaction> transList = entities.Transactions.ToList();
            //    double chargeBack = 0;
            //    for (int j = 0; j < transList.Count; j++)
            //    {
            //        if (transList[j].UnitPrice != null && transList[j].DeptCode == deptCode
            //            && DateTime.Compare(transList[j].TranDateTime, monthList[i]) >= 0
            //            && DateTime.Compare(transList[j].TranDateTime, monthList[i].AddMonths(1)) >= 0)
            //        {
            //            chargeBack = chargeBack + transList[j].QtyChange * (double)transList[j].UnitPrice;
            //        }
            //    }
            //    ReportItemVM ri = new ReportItemVM();
            //    ri.Period = monthList[i];
            //    ri.Val1 = chargeBack;
            //    ri.Val2 = 0;
            //    riList.Add(ri);
            //}
            //return riList;
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

        public static List<DateTime> GetMonthList(DateTime fromDate, DateTime toDate)
        {
            // make sure fromDate is before toDate, will add in validation later
            List<DateTime> monthList = new List<DateTime>();
            int fromYear = fromDate.Year;
            int fromMonth = fromDate.Month;
            DateTime startMonth = new DateTime(fromYear, fromMonth, 01, 00, 00, 00);
            int toYear = toDate.Year;
            int toMonth = toDate.Month;
            DateTime endMonth = new DateTime(toYear, toMonth, 01, 00, 00, 00);
            while(DateTime.Compare(startMonth, endMonth) < 0)
            {
                monthList.Add(startMonth);
                startMonth = startMonth.AddMonths(1);

            }
            return monthList;
        }
    }
}