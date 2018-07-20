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
                //    chargeBack = Math.Round(chargeBack, 2);
                //    string format = "yyyy MMM";
                //    string label = monthList[i].ToString(format);
                //    ReportItemVM ri = new ReportItemVM();
                //    ri.Period = monthList[i];
                //    ri.Label = label;
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
                    chargeBack = Math.Round(chargeBack, 2);
                    string format = "yyyy MMM";
                    string label = monthList[i].ToString(format);
                    ReportItemVM ri = new ReportItemVM();
                    ri.Period = monthList[i];
                    ri.Label = label;
                    ri.Val1 = chargeBack;
                    ri.Val2 = 0;
                    riList.Add(ri);
                }
                return riList;
            }
        }

        // get chargeback by date range
        // done
        public static List<ReportItemVM> GetCBByRng(string deptCode, DateTime fromDate, DateTime toDate)
        {
            
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<DateTime> weekList = GetWeekList(fromDate, toDate);
                List<ReportItemVM> riList = new List<ReportItemVM>();
                for (int i = 0; i < weekList.Count; i++)
                {
                    List<Transaction> transList = entities.Transactions.ToList();
                    double chargeBack = 0;
                    for (int j = 0; j < transList.Count; j++)
                    {
                        if (transList[j].UnitPrice != null && transList[j].DeptCode == deptCode
                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) >= 0)
                        {
                            chargeBack = chargeBack + transList[j].QtyChange * (double)transList[j].UnitPrice;
                        }
                    }
                    chargeBack = Math.Round(chargeBack, 2);
                    string format = "yyyy MMM d";
                    string label = weekList[i].ToString(format);
                    ReportItemVM ri = new ReportItemVM();
                    ri.Period = weekList[i];
                    ri.Label = label;
                    ri.Val1 = chargeBack;
                    ri.Val2 = 0;
                    riList.Add(ri);
                }
                return riList;
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

        // get monthly chargeback
        public static List<ReportItemVM> GetCBMonthly(DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                int year = toDate.Year;
                int month = toDate.Month;
                DateTime startDate = new DateTime(year, month, 01, 00, 00, 00);
                DateTime endDate = startDate.AddMonths(1);
                List<ReportItemVM> rilist = new List<ReportItemVM>();
                List<Transaction> translist = entities.Transactions.ToList();
                List<Department> deptlist = entities.Departments.Where(d => d.DeptCode != "STOR").ToList();
                for (int i = 0; i < deptlist.Count; i++)
                {
                    double chargeBack = 0;
                    for (int j = 0; j < translist.Count; j++)
                    {
                        if (translist[j].UnitPrice != null && deptlist[i].DeptCode == translist[j].DeptCode &&
                            DateTime.Compare(translist[i].TranDateTime, startDate) >= 0 &&
                            DateTime.Compare(translist[i].TranDateTime, endDate) < 0)
                        {
                            chargeBack = chargeBack + chargeBack + translist[i].QtyChange * (double)translist[i].UnitPrice;
                        }
                    }
                    ReportItemVM ri = new ReportItemVM();
                    ri.Label = deptlist[i].DeptName;
                    ri.Val1 = chargeBack;
                    ri.Val2 = 0;
                    rilist.Add(ri);
                }
                return rilist;
            }
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

        public static List<DateTime> GetWeekList(DateTime fromDate, DateTime toDate)
        {
            // make sure fromDate is before toDate, will add in validation later
            List<DateTime> weekList = new List<DateTime>();
            int fromYear = fromDate.Year;
            int fromMonth = fromDate.Month;
            int fromDay = fromDate.Day;
            DateTime startWeek = new DateTime(fromYear, fromMonth, fromDay, 00, 00, 00);
            if (fromDate.DayOfWeek == DayOfWeek.Tuesday) startWeek = startWeek.AddDays(-1);
            else if (fromDate.DayOfWeek == DayOfWeek.Wednesday) startWeek = startWeek.AddDays(-2);
            else if (fromDate.DayOfWeek == DayOfWeek.Thursday) startWeek = startWeek.AddDays(-3);
            else if (fromDate.DayOfWeek == DayOfWeek.Friday) startWeek = startWeek.AddDays(-4);
            else if (fromDate.DayOfWeek == DayOfWeek.Saturday) startWeek = startWeek.AddDays(-5);
            else if (fromDate.DayOfWeek == DayOfWeek.Sunday) startWeek = startWeek.AddDays(-6);
            else startWeek = new DateTime(fromYear, fromMonth, fromDay, 00, 00, 00);

            int toYear = toDate.Year;
            int toMonth = toDate.Month;
            int toDay = toDate.Day;
            DateTime endWeek = new DateTime(toYear, toMonth, toDay, 00, 00, 00);
            if (toDate.DayOfWeek == DayOfWeek.Tuesday) endWeek = endWeek.AddDays(-1);
            else if (toDate.DayOfWeek == DayOfWeek.Wednesday) endWeek = endWeek.AddDays(-2);
            else if (toDate.DayOfWeek == DayOfWeek.Thursday) endWeek = endWeek.AddDays(-3);
            else if (toDate.DayOfWeek == DayOfWeek.Friday) endWeek = endWeek.AddDays(-4);
            else if (toDate.DayOfWeek == DayOfWeek.Saturday) endWeek = endWeek.AddDays(-5);
            else if (toDate.DayOfWeek == DayOfWeek.Sunday) endWeek = endWeek.AddDays(-6);
            else endWeek = new DateTime(toYear, toMonth, toDay, 00, 00, 00);

            while (DateTime.Compare(startWeek, endWeek) < 0)
            {
                weekList.Add(startWeek);
                startWeek = startWeek.AddDays(7);

            }
            return weekList;
        }
    }
}