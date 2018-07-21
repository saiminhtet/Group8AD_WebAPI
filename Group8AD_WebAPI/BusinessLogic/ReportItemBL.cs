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
                List<DateTime> monthList = GetMonthList(fromDate, toDate);
                List<ReportItemVM> riList = new List<ReportItemVM>();
                List<Transaction> transList = entities.Transactions.ToList();
                for (int i = 0; i < monthList.Count; i++)
                {
                    double chargeBack = 0;
                    for (int j = 0; j < transList.Count; j++)
                    {
                        if (transList[j].UnitPrice != null && transList[j].DeptCode == deptCode
                            && DateTime.Compare(transList[j].TranDateTime, monthList[i]) >= 0
                            && DateTime.Compare(transList[j].TranDateTime, monthList[i].AddMonths(1)) < 0)
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
                List<Transaction> transList = entities.Transactions.ToList();
                for (int i = 0; i < weekList.Count; i++)
                {
                    double chargeBack = 0;
                    for (int j = 0; j < transList.Count; j++)
                    {
                        if (transList[j].UnitPrice != null && transList[j].DeptCode == deptCode
                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
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
        // done
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
                            DateTime.Compare(translist[j].TranDateTime, startDate) >= 0 &&
                            DateTime.Compare(translist[j].TranDateTime, endDate) < 0)
                        {
                            chargeBack = chargeBack + translist[j].QtyChange * (double)translist[j].UnitPrice;
                        }
                    }
                    ReportItemVM ri = new ReportItemVM();
                    chargeBack = Math.Round(chargeBack, 2);
                    ri.Period = startDate;
                    ri.Label = deptlist[i].DeptName;
                    ri.Val1 = chargeBack;
                    ri.Val2 = 0;
                    rilist.Add(ri);
                }
                return rilist;
            }
        }

        // get annual volume
        // done
        public static List<ReportItemVM> GetVolMonthly(DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                int year = toDate.Year;
                int month = toDate.Month;
                DateTime startDate = new DateTime(year, month, 01, 00, 00, 00);
                DateTime endDate = startDate.AddMonths(1);
                List<ReportItemVM> rilist = new List<ReportItemVM>();
                List<Request> rlist = entities.Requests.ToList();
                List<Item> ilist = entities.Items.ToList();
                for (int i = 0; i < ilist.Count; i++)
                {
                    string itemName = ilist[i].Desc;
                    int reqQty = 0;
                    for (int j = 0; j < rlist.Count; j++)
                    {
                        if (rlist[j].ReqDateTime != null && DateTime.Compare((DateTime)rlist[j].ReqDateTime, startDate) >= 0 &&
                            DateTime.Compare((DateTime)rlist[j].ReqDateTime, endDate) < 0)
                        {
                            List<RequestDetail> rdlist = entities.RequestDetails.ToList();
                            for (int k = 0; k < rdlist.Count; k++)
                            {
                                if (rdlist[k].ReqId == rlist[j].ReqId && rdlist[k].ItemCode == ilist[i].ItemCode)
                                {
                                    reqQty = reqQty + rdlist[k].ReqQty;
                                }
                            }
                            //List<RequestDetail> rdlist = entities.RequestDetails.Where(r => r.ItemCode == ilist[i].ItemCode).ToList();
                            //for (int k = 0; k < rdlist.Count; k++)
                            //{
                            //    if (rdlist[k].ReqId == rlist[j].ReqId)
                            //    {
                            //        reqQty = reqQty + rdlist[k].ReqQty;
                            //    }
                            //}
                        }
                    }
                    ReportItemVM ri = new ReportItemVM();
                    ri.Period = startDate;
                    ri.Label = itemName;
                    ri.Val1 = reqQty;
                    ri.Val2 = 0;
                    rilist.Add(ri);
                }
                return rilist;
            }
        }

        // show cost report
        public static List<ReportItemVM> ShowCostReport(string dept1, string dept2, string supp1, string supp2,
            string cat, List<DateTime> dates, bool byMonth)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<ReportItemVM> riList = new List<ReportItemVM>();
                if (dept1 != null && dept2 != null && (supp1 == null || supp2 == null))
                {
                    if (byMonth == true)
                    {
                        List<DateTime> dtList = GetMonthList(dates);
                        List<Transaction> transList = entities.Transactions.ToList();
                        if (cat == "All")
                        {
                            for (int i = 0; i < dtList.Count; i++)
                            {
                                double chargeBack1 = 0;
                                double chargeBack2 = 0;
                                for (int j = 0; j < transList.Count; j++)
                                {
                                    if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                    {
                                        chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                    if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                    {
                                        chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                }
                                chargeBack1 = Math.Round(chargeBack1, 2);
                                chargeBack2 = Math.Round(chargeBack2, 2);
                                string format = "yyyy MMM";
                                string label = dtList[i].ToString(format);
                                ReportItemVM ri = new ReportItemVM();
                                ri.Period = dtList[i];
                                ri.Label = label;
                                ri.Val1 = chargeBack1;
                                ri.Val2 = chargeBack2;
                                riList.Add(ri);
                            }
                        }
                        else
                        {
                            List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                            for (int i = 0; i < dtList.Count; i++)
                            {
                                double chargeBack1 = 0;
                                double chargeBack2 = 0;
                                for (int j = 0; j < transList.Count; j++)
                                {
                                    if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                        && InItemList(transList[j].ItemCode, iList) == true)
                                    {
                                        chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                    if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                        && InItemList(transList[j].ItemCode, iList) == true)
                                    {
                                        chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                }
                                chargeBack1 = Math.Round(chargeBack1, 2);
                                chargeBack2 = Math.Round(chargeBack2, 2);
                                string format = "yyyy MMM";
                                string label = dtList[i].ToString(format);
                                ReportItemVM ri = new ReportItemVM();
                                ri.Period = dtList[i];
                                ri.Label = label;
                                ri.Val1 = chargeBack1;
                                ri.Val2 = chargeBack2;
                                riList.Add(ri);
                            }
                        }
                    }
                    else
                    {
                        if (dates.Count >= 2)
                        {
                            DateTime dt1 = dates[0];
                            DateTime dt2 = dates[1];
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                DateTime temp = dt1;
                                dt1 = dt2;
                                dt2 = temp;
                            }
                            List<DateTime> weekList = GetWeekList(dt1, dt2);
                            List<Transaction> transList = entities.Transactions.ToList();
                            if (cat == "All")
                            {
                                for (int i = 0; i < weekList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM d";
                                    string label = weekList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = weekList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1;
                                    ri.Val2 = chargeBack2;
                                    riList.Add(ri);
                                }
                            }
                            else
                            {
                                List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                for (int i = 0; i < weekList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM d";
                                    string label = weekList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = weekList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1;
                                    ri.Val2 = chargeBack2;
                                    riList.Add(ri);
                                }
                            }
                        } 
                    }
                }
                else if (supp1 != null && supp2 != null && (dept1 == null || dept2 == null))
                {
                    if (byMonth == true)
                    {
                        List<DateTime> dtList = GetMonthList(dates);
                        List<Transaction> transList = entities.Transactions.ToList();
                        if (cat == "All")
                        {
                            for (int i = 0; i < dtList.Count; i++)
                            {
                                double chargeBack1 = 0;
                                double chargeBack2 = 0;
                                for (int j = 0; j < transList.Count; j++)
                                {
                                    if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                    {
                                        chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                    if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                    {
                                        chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                }
                                chargeBack1 = Math.Round(chargeBack1, 2);
                                chargeBack2 = Math.Round(chargeBack2, 2);
                                string format = "yyyy MMM";
                                string label = dtList[i].ToString(format);
                                ReportItemVM ri = new ReportItemVM();
                                ri.Period = dtList[i];
                                ri.Label = label;
                                ri.Val1 = chargeBack1;
                                ri.Val2 = chargeBack2;
                                riList.Add(ri);
                            }
                        }
                        else
                        {
                            List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                            for (int i = 0; i < dtList.Count; i++)
                            {
                                double chargeBack1 = 0;
                                double chargeBack2 = 0;
                                for (int j = 0; j < transList.Count; j++)
                                {
                                    if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                        && InItemList(transList[j].ItemCode, iList) == true)
                                    {
                                        chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                    if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                        && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                        && InItemList(transList[j].ItemCode, iList) == true)
                                    {
                                        chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                    }
                                }
                                chargeBack1 = Math.Round(chargeBack1, 2);
                                chargeBack2 = Math.Round(chargeBack2, 2);
                                string format = "yyyy MMM";
                                string label = dtList[i].ToString(format);
                                ReportItemVM ri = new ReportItemVM();
                                ri.Period = dtList[i];
                                ri.Label = label;
                                ri.Val1 = chargeBack1;
                                ri.Val2 = chargeBack2;
                                riList.Add(ri);
                            }
                        }
                    }
                    else
                    {
                        if (dates.Count >= 2)
                        {
                            DateTime dt1 = dates[0];
                            DateTime dt2 = dates[1];
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                DateTime temp = dt1;
                                dt1 = dt2;
                                dt2 = temp;
                            }
                            List<DateTime> weekList = GetWeekList(dt1, dt2);
                            List<Transaction> transList = entities.Transactions.ToList();
                            if (cat == "All")
                            {
                                for (int i = 0; i < weekList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM d";
                                    string label = weekList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = weekList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1;
                                    ri.Val2 = chargeBack2;
                                    riList.Add(ri);
                                }
                            }
                            else
                            {
                                List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                for (int i = 0; i < weekList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM d";
                                    string label = weekList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = weekList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1;
                                    ri.Val2 = chargeBack2;
                                    riList.Add(ri);
                                }
                            }
                        }
                    }
                }
                return riList;
            }     
        }

        // show volume report
        public static void ShowVolumeReport(string dept1, string dept2, string supp1, string supp2, 
            string cat, List<DateTime> dates, bool byMonth)
        {
            return;
        }

        public static bool InItemList(string itemCode, List<Item> iList)
        {
            bool isIn = false;
            for (int i = 0; i < iList.Count; i++)
            {
                if (iList[i].ItemCode == itemCode)
                    isIn = true;
            }
            return isIn;
        }

        public static List<DateTime> GetMonthList(List<DateTime> dates)
        {
            List<DateTime> dtList = new List<DateTime>();
            for (int i = 0; i < dates.Count; i++)
            {
                int year = dates[i].Year;
                int month = dates[i].Month;
                DateTime tempDate = new DateTime(year, month, 01, 00, 00, 00);
                dtList.Add(tempDate);
            }
            dtList.Sort();
            List<DateTime> distinct = dtList.Distinct().ToList();
            return distinct;
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

            monthList.Add(startMonth);
            while (DateTime.Compare(startMonth, endMonth) < 0)
            {
                startMonth = startMonth.AddMonths(1);
                monthList.Add(startMonth);

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

            weekList.Add(startWeek);
            while (DateTime.Compare(startWeek, endWeek) < 0)
            {
                startWeek = startWeek.AddDays(7);
                weekList.Add(startWeek);

            }
            //do
            //{
            //    weekList.Add(startWeek);
            //    startWeek = startWeek.AddDays(7);
            //}
            //while (DateTime.Compare(startWeek, endWeek) < 0);
            return weekList;
        }
    }
}