using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class ItemBL
    {

        //get itemlist by category and description
        public static List<ItemVM> GetItems(string cat, string desc)
        {
            List<ItemVM> itemlist = new List<ItemVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                if (desc != null && cat == null)
                {
                    List<Item> ilist1 = entities.Items.Where(i => i.Desc.ToUpper().Contains(desc.ToUpper()) == true).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist1));
                    return itemlist;
                }
                else if (cat != null && desc == null)
                {
                    List<Item> ilist2 = entities.Items.Where(i => i.Cat == cat).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist2));
                    return itemlist;
                }
                else if (cat != null && desc != null)
                {
                    List<Item> ilist = entities.Items.Where(i => i.Cat == cat && i.Desc.ToUpper().Contains(desc.ToUpper())).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist));
                    return itemlist;
                }
                else
                {
                    List<Item> ilist = entities.Items.ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist));
                    return itemlist;
                }
            }
        }

        //get Frequent Item list by Employee ID
        public static List<ItemVM> GetFrequentList(int empId)
        {

            List<ItemVM> frequent_itemlists = new List<ItemVM>();

            List<ItemVM> itemcodes = new List<ItemVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                List<Request> emp_rList = entities.Requests.Where(r => r.EmpId == empId).ToList<Request>();

                List<RequestDetail> reqdetails = new List<RequestDetail>();
                foreach (Request req in emp_rList)
                {
                    var emp_rdetailList = entities.RequestDetails.Where(rd => rd.ReqId == req.ReqId)
                                                            .GroupBy(rd => rd.ItemCode)
                                                            .Select(rd => new { ItemCode = rd.Key, ReqQty = rd.Sum(r => r.ReqQty) }).OrderByDescending(x => x.ReqQty).ToList();

                    foreach (var item in emp_rdetailList)
                    {
                        ItemVM i = new ItemVM();
                        i.ItemCode = item.ItemCode;
                        i.TempQtyReq = item.ReqQty;
                        itemcodes.Add(i);
                    }
                }

                List<ItemVM> itemgrouplist = itemcodes.GroupBy(i => i.ItemCode).Select(i => new ItemVM { ItemCode = i.Key, TempQtyReq = i.Sum(it => it.TempQtyReq) }).OrderByDescending(i => i.TempQtyReq).ToList<ItemVM>();

                foreach (ItemVM item in itemgrouplist)
                {
                    ItemVM freqitem = entities.Items.Where(i => i.ItemCode.Equals(item.ItemCode)).Select(i => new ItemVM()
                    {
                        ItemCode = i.ItemCode,
                        Cat = i.Cat,
                        Desc = i.Desc,
                        Location = i.Location,
                        UOM = i.UOM,
                        IsActive = i.IsActive,
                        Balance = i.Balance,
                        ReorderLevel = i.ReorderLevel,
                        ReorderQty = i.ReorderQty,
                        TempQtyDisb = i.TempQtyDisb,
                        TempQtyCheck = i.TempQtyCheck,
                        SuppCode1 = i.SuppCode1,
                        SuppCode2 = i.SuppCode2,
                        SuppCode3 = i.SuppCode3,
                        Price1 = i.Price1 ?? default(double),
                        Price2 = i.Price2 ?? default(double),
                        Price3 = i.Price3 ?? default(double)
                    }).FirstOrDefault();

                    frequent_itemlists.Add(freqitem);

                }
            }
            return frequent_itemlists.Take(5).ToList();
        }



        //Get Dept DisbList by Employee ID
        public static List<ItemVM> GetDeptDisbList(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptcode = EmployeeBL.GetDeptCode(empId);

                List<RequestDetailVM> requestDetailLists = entities.Employees.Where(e => e.DeptCode.Equals(deptcode))
                                                         .Join(entities.Requests.Where(r => r.Status.Equals("Approved")), e => e.EmpId, r => r.EmpId, (e, r) => new { e, r })
                                                         .Join(entities.RequestDetails.Where(x => x.AwaitQty > 0), x => x.r.ReqId, rd => rd.ReqId, (x, rd) => new { x, rd })
                                                         .Select(rd => new RequestDetailVM
                                                         {
                                                             ReqId = rd.rd.ReqId,
                                                             ReqLineNo = rd.rd.ReqLineNo,
                                                             ItemCode = rd.rd.ItemCode,
                                                             ReqQty = rd.rd.ReqQty,
                                                             AwaitQty = rd.rd.AwaitQty,
                                                             FulfilledQty = rd.rd.FulfilledQty
                                                         }).ToList();

                List<ItemVM> iList = GetAllItems();
                List<ItemVM> ritemlist = new List<ItemVM>();
                foreach (ItemVM item in iList)
                {
                    foreach (RequestDetailVM rd in requestDetailLists)
                    {

                        if (rd.AwaitQty > 0 && ritemlist.Where(x => x.ItemCode.Equals(rd.ItemCode)).ToList().Count == 0 && item.ItemCode.Equals(rd.ItemCode))
                        {
                            ItemVM i = new ItemVM();
                            i.ItemCode = rd.ItemCode;
                            ItemVM io = GetItem(i.ItemCode);
                            i.Balance = io.Balance;
                            i.Location = io.Location;
                            i.ReorderLevel = io.ReorderLevel;
                            i.ReorderQty = io.ReorderQty;
                            i.Cat = io.Cat;
                            i.Desc = io.Desc;
                            i.UOM = io.UOM;
                            i.SuppCode1 = io.SuppCode1;
                            i.Price1 = io.Price1;
                            i.TempQtyDisb = io.TempQtyDisb;
                            i.TempQtyCheck = io.TempQtyCheck;
                            i.TempQtyAcpt = io.TempQtyAcpt;
                            i.TempQtyReq = io.TempQtyReq;
                            ritemlist.Add(i);
                        }
                    }

                    foreach (RequestDetailVM rd in requestDetailLists)
                    {
                        if (rd.AwaitQty > 0 && iList.Count > 0 && item.ItemCode.Equals(rd.ItemCode))
                        {
                            foreach (ItemVM i in iList.Where(x => x.ItemCode.Equals(rd.ItemCode)))
                            {
                                ritemlist.ToList().Find(x => x.ItemCode.Equals(rd.ItemCode)).TempQtyReq += rd.AwaitQty;
                            }
                        }
                    }
                }

                return ritemlist;
            }
        }


        //AcceptDisbursement
        public static void AcceptDisbursement(int empId, List<ItemVM> iList)
        {

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string vNum = AdjustmentBL.GenerateVoucherNo();
                foreach (ItemVM i in iList)
                {
                    if (i.TempQtyReq - i.TempQtyAcpt > 0)
                    {
                        Adjustment a = new Adjustment();
                        a.VoucherNo = vNum;
                        a.EmpId = empId;
                        a.DateTimeIssued = DateTime.Now;
                        a.ItemCode = i.ItemCode;

                        int index = iList.FindIndex(x => x.ItemCode.Equals(i.ItemCode));



                        a.QtyChange = i.TempQtyAcpt - i.TempQtyReq ?? default(int);

                        a.Reason = i.TempReason;
                        //   a.QtyChange = i.TempQtyAcpt - i.TempQtyReq ?? default(int);

                        a.Status = "Submitted";
                        a.Reason = i.TempReason;
                        entities.Adjustments.Add(a);
                        entities.SaveChanges();

                        double chgVal = a.QtyChange * i.Price1;

                        if (chgVal >= 250)
                        {
                            //Notify Manager
                            NotificationBL.AddNewNotification(empId, 104, "Adjustment Request", vNum + " has been raised");
                        }
                        else
                        {
                            //Notify Supervisor
                            NotificationBL.AddNewNotification(empId, 105, "Adjustment Request", vNum + " has been raised");
                        }

                    }

                    string deptcode = EmployeeBL.GetDeptCode(empId);

                    var EmpIds = entities.Employees.Where(e => e.DeptCode.Equals(deptcode)).Select(e => e.EmpId).ToList();


                    List<int> rList = new List<int>();
                    List<RequestDetail> rdList = new List<RequestDetail>();



                    foreach (var empid in EmpIds)
                    {
                        var reqList = entities.Requests.Where(x => x.EmpId == empid && x.Status.Equals("Approved")).Select(x => x.ReqId).ToList<int>();
                        rList.AddRange(reqList);
                    }

                    foreach (int reqId in rList)
                    {
                        List<RequestDetail> reqdList = entities.RequestDetails.Where(x => x.ReqId == reqId).ToList();
                        rdList.AddRange(reqdList);
                    }


                    int count = i.TempQtyAcpt;

                    foreach (int r in rList)
                    {
                        int cntFulfilled = 0;
                        if (count > 0)
                        {
                            List<RequestDetail> reqdList = rdList.Where(x => x.ItemCode.Equals(i.ItemCode) && x.ReqId == r).ToList();
                            if (reqdList.Count > 0)
                            {

                                foreach (RequestDetail rd in reqdList) //rdList.Where(rd => rd.ReqId == r.ReqId)
                                {
                                    if (count > 0)
                                    {
                                        if (rd.AwaitQty > 0 && rd.AwaitQty <= count)
                                        {

                                            int QtyCount = count;
                                            rd.FulfilledQty += rd.AwaitQty;
                                            count -= rd.AwaitQty;
                                            rd.AwaitQty = 0;

                                            //Save Changes for rd.AwaitQty, Rd.FulfilledQty
                                            UpdateAwait(rd.ReqId, rd.ItemCode, rd.AwaitQty);
                                            UpdateFulfilled(rd.ReqId, rd.ItemCode, rd.FulfilledQty);

                                            TransactionVM t = new TransactionVM();
                                            //t.VoucherNo = vNum;
                                            t.TranDateTime = DateTime.Now;
                                            t.ItemCode = rd.ItemCode;
                                            t.QtyChange = count - QtyCount;     //rd.AwaitQty;
                                            t.UnitPrice = i.Price1;
                                            t.Desc = "Disbursement";
                                            t.DeptCode = deptcode;

                                            TransactionBL.AddTran(t);
                                        }

                                        else if (rd.AwaitQty > 0 && rd.AwaitQty > count)
                                        {
                                            rd.FulfilledQty += count;
                                            rd.AwaitQty -= count;


                                            //Save Changes for rd.AwaitQty, Rd.FulfilledQty
                                            UpdateAwait(rd.ReqId, rd.ItemCode, rd.AwaitQty);
                                            UpdateFulfilled(rd.ReqId, rd.ItemCode, rd.FulfilledQty);


                                            TransactionVM t = new TransactionVM();
                                            t.TranDateTime = DateTime.Now;
                                            t.ItemCode = rd.ItemCode;
                                            t.QtyChange = rd.AwaitQty * -1;
                                            t.UnitPrice = i.Price1;
                                            t.Desc = "Disbursement";
                                            t.DeptCode = deptcode;


                                            TransactionBL.AddTran(t);

                                            count = 0;
                                        }

                                    }
                                    cntFulfilled += (rd.ReqQty - rd.FulfilledQty);
                                }


                                //Check if Request Fulfilled
                                if (cntFulfilled == 0)
                                {
                                    // r.Status = "Fulfilled";
                                    RequestVM rvm = RequestBL.GetReq(r);
                                    rvm.FulfilledDateTime = DateTime.Now;
                                    rvm.Status = "Fulfilled";
                                    //rvm.EmpId = r.EmpId;
                                    //rvm.ApproverId = r.ApproverId;
                                    //rvm.ApproverComment = r.ApproverComment;
                                    //rvm.ReqDateTime = r.ReqDateTime ?? default(DateTime);
                                    //rvm.CancelledDateTime = r.CancelledDateTime ?? default(DateTime);
                                    //rvm.Status = r.Status;
                                    //rvm.FulfilledDateTime = r.FulfilledDateTime ?? default(DateTime);
                                    RequestBL.UpdateReq(rvm); //save changes for this request object
                                }
                            }

                        }

                        //Check Low Stock item
                        bool status = CheckLowStk(i);

                        if (status)
                        {
                            Item item = Utility.ItemUtility.Convert_ItemVMObj_To_ItemObj(i);
                            NotificationBL.AddLowStkNotification(empId, item);
                        }

                        //send email acknowledgement to rep, specific head and all clerks
                        // NotificationBL.AddAcptNotification(r.ReqId); Noti throw exception need to fix
                    }
                    // }


                }
            }
        }




        //AcceptDisbursement to rcvEmpID
        public static void AcceptDisbursement(int empId, int rcvEmpId, List<ItemVM> iList)
        {

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string vNum = AdjustmentBL.GenerateVoucherNo();
                foreach (ItemVM i in iList)
                {
                    if (i.TempQtyReq - i.TempQtyAcpt > 0)
                    {
                        Adjustment a = new Adjustment();
                        a.VoucherNo = vNum;
                        a.EmpId = empId;
                        a.DateTimeIssued = DateTime.Now;
                        a.ItemCode = i.ItemCode;

                        int index = iList.FindIndex(x => x.ItemCode.Equals(i.ItemCode));



                        a.QtyChange = i.TempQtyAcpt - i.TempQtyReq ?? default(int);

                        a.Reason = i.TempReason;
                        //   a.QtyChange = i.TempQtyAcpt - i.TempQtyReq ?? default(int);

                        a.Status = "Submitted";
                        a.Reason = i.TempReason;
                        entities.Adjustments.Add(a);
                        entities.SaveChanges();

                        double chgVal = a.QtyChange * i.Price1;

                        if (chgVal >= 250)
                        {
                            //Notify Manager
                            NotificationBL.AddNewNotification(empId, 104, "Adjustment Request", vNum + " has been raised");
                        }
                        else
                        {
                            //Notify Supervisor
                            NotificationBL.AddNewNotification(empId, 105, "Adjustment Request", vNum + " has been raised");
                        }

                    }

                    string deptcode = EmployeeBL.GetDeptCode(empId);

                    var EmpIds = entities.Employees.Where(e => e.DeptCode.Equals(deptcode)).Select(e => e.EmpId).ToList();


                    List<int> rList = new List<int>();
                    List<RequestDetail> rdList = new List<RequestDetail>();



                    foreach (var empid in EmpIds)
                    {
                        var reqList = entities.Requests.Where(x => x.EmpId == empid && x.Status.Equals("Approved")).Select(x => x.ReqId).ToList<int>();
                        rList.AddRange(reqList);
                    }

                    foreach (int reqId in rList)
                    {
                        List<RequestDetail> reqdList = entities.RequestDetails.Where(x => x.ReqId == reqId).ToList();
                        rdList.AddRange(reqdList);
                    }


                    int count = i.TempQtyAcpt;

                    foreach (int r in rList)
                    {
                        int cntFulfilled = 0;
                        if (count > 0)
                        {
                            List<RequestDetail> reqdList = rdList.Where(x => x.ItemCode.Equals(i.ItemCode) && x.ReqId == r).ToList();
                            if (reqdList.Count > 0)
                            {
                                foreach (RequestDetail rd in reqdList) //rdList.Where(rd => rd.ReqId == r.ReqId)
                                {
                                    if (count > 0)
                                    {
                                        if (rd.AwaitQty > 0 && rd.AwaitQty <= count)
                                        {

                                            int QtyCount = count;
                                            rd.FulfilledQty += rd.AwaitQty;
                                            count -= rd.AwaitQty;
                                            rd.AwaitQty = 0;

                                            //Save Changes for rd.AwaitQty, Rd.FulfilledQty
                                            UpdateAwait(rd.ReqId, rd.ItemCode, rd.AwaitQty);
                                            UpdateFulfilled(rd.ReqId, rd.ItemCode, rd.FulfilledQty);

                                            TransactionVM t = new TransactionVM();
                                            //t.VoucherNo = vNum;
                                            t.TranDateTime = DateTime.Now;
                                            t.ItemCode = rd.ItemCode;
                                            t.QtyChange = count - QtyCount;     //rd.AwaitQty;
                                            t.UnitPrice = i.Price1;
                                            t.Desc = "Disbursement";
                                            t.DeptCode = deptcode;

                                            TransactionBL.AddTran(t);
                                        }

                                        else if (rd.AwaitQty > 0 && rd.AwaitQty > count)
                                        {
                                            rd.FulfilledQty += count;
                                            rd.AwaitQty -= count;


                                            //Save Changes for rd.AwaitQty, Rd.FulfilledQty
                                            UpdateAwait(rd.ReqId, rd.ItemCode, rd.AwaitQty);
                                            UpdateFulfilled(rd.ReqId, rd.ItemCode, rd.FulfilledQty);


                                            TransactionVM t = new TransactionVM();
                                            t.TranDateTime = DateTime.Now;
                                            t.ItemCode = rd.ItemCode;
                                            t.QtyChange = rd.AwaitQty * -1;
                                            t.UnitPrice = i.Price1;
                                            t.Desc = "Disbursement";
                                            t.DeptCode = deptcode;


                                            TransactionBL.AddTran(t);

                                            count = 0;
                                        }

                                    }
                                    cntFulfilled += (rd.ReqQty - rd.FulfilledQty);
                                }

                                //Check if Request Fulfilled
                                if (cntFulfilled == 0)
                                {
                                    // r.Status = "Fulfilled";
                                    RequestVM rvm = RequestBL.GetReq(r);
                                    rvm.FulfilledDateTime = DateTime.Now;
                                    rvm.Status = "Fulfilled";
                                    //rvm.EmpId = r.EmpId;
                                    //rvm.ApproverId = r.ApproverId;
                                    //rvm.ApproverComment = r.ApproverComment;
                                    //rvm.ReqDateTime = r.ReqDateTime ?? default(DateTime);
                                    //rvm.CancelledDateTime = r.CancelledDateTime ?? default(DateTime);
                                    //rvm.Status = r.Status;
                                    //rvm.FulfilledDateTime = r.FulfilledDateTime ?? default(DateTime);
                                    RequestBL.UpdateReq(rvm); //save changes for this request object
                                }
                            }
                        }

                        //Check Low Stock item
                        bool status = CheckLowStk(i);

                        if (status)
                        {
                            Item item = Utility.ItemUtility.Convert_ItemVMObj_To_ItemObj(i);
                            NotificationBL.AddLowStkNotification(empId, item);
                        }

                        //send email acknowledgement to rep, specific head and all clerks
                        // NotificationBL.AddAcptNotification(r.ReqId); 
                        NotificationBL.AddNewNotification(empId, rcvEmpId, "Stationery Request", "A new stationery request has been submitted");
                    }
                    // }


                }
            }
        }


        //get All Category list
        public static List<String> GetCatList()
        {
            List<String> category = new List<string>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                category = entities.Items
                            .Select(c => c.Cat).Distinct()
                            .ToList<String>();
            }

            return category;
        }


        //get low stock Items
        public static List<ItemVM> GetLowStockItems()
        {
            List<ItemVM> itemlist = new List<ItemVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                itemlist = entities.Items
                            .Where(i => i.Balance < i.ReorderLevel)
                            .Select(i => new ItemVM()
                            {
                                ItemCode = i.ItemCode,
                                Cat = i.Cat,
                                Desc = i.Desc,
                                Location = i.Location,
                                UOM = i.UOM,
                                IsActive = i.IsActive,
                                Balance = i.Balance,
                                ReorderLevel = i.ReorderLevel,
                                ReorderQty = i.ReorderQty,
                                TempQtyDisb = i.TempQtyDisb,
                                TempQtyCheck = i.TempQtyCheck,
                                SuppCode1 = i.SuppCode1,
                                SuppCode2 = i.SuppCode2,
                                SuppCode3 = i.SuppCode3,
                                Price1 = i.Price1 ?? default(double),
                                Price2 = i.Price2 ?? default(double),
                                Price3 = i.Price3 ?? default(double)
                            }).ToList<ItemVM>();

                foreach (ItemVM item in itemlist)
                {
                    item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                    item.ReccReorderLvl = item.ReccReorderQty * 2;
                }
            }
            return itemlist;
        }

        //Get Recommand ReOrderQty
        public static int Get3monthReqandOutstandingReqs(string iCode)
        {
            int recReorderqty = 0;
            double threeMthReqQty = 0;
            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddMonths(-3);
            double outReqQty = 0;


            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                List<RequestDetailVM> rdList = entities.Requests.Where(x => x.ReqDateTime <= d1 && x.ReqDateTime >= d2 && x.Status.Equals("Approved"))
                                       .Join(entities.RequestDetails, r => r.ReqId, rd => rd.ReqId,
                                       (r, rd) => new { r, rd }).Select(x => new RequestDetailVM
                                       {
                                           ReqId = x.rd.ReqId,
                                           ItemCode = x.rd.ItemCode,
                                           ReqQty = x.rd.ReqQty,
                                           AwaitQty = x.rd.AwaitQty,
                                           FulfilledQty = x.rd.FulfilledQty

                                       }).ToList();


                foreach (RequestDetailVM rd in rdList)
                {
                    if (rd.ItemCode.Equals(iCode))
                    {
                        threeMthReqQty += rd.ReqQty / 6;
                        outReqQty += (rd.ReqQty - rd.AwaitQty - rd.FulfilledQty);
                        recReorderqty = Convert.ToInt16(threeMthReqQty + outReqQty);
                    }
                }

            }
            return recReorderqty;
        }

        //Get ThreeMonthReqQty
        private static double GetThreeMonthReqQty(string iCode)
        {
            double threeMthReqQty = 0;
            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddMonths(-3);


            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                List<Request> rList = entities.Requests.Where(x => x.ReqDateTime <= d1 && x.ReqDateTime >= d2).ToList();

                foreach (Request r in rList)
                {
                    foreach (RequestDetailVM rd in RequestDetailBL.GetReqDetList(r.ReqId))
                    {
                        if (rd.ItemCode.Equals(iCode))
                        {
                            threeMthReqQty += rd.ReqQty;
                        }
                    }

                }
            }
            return threeMthReqQty;
        }

        //Get OutstandingReqQty
        private static double GetOutstandingReqQty(string iCode)
        {
            double outReqQty = 0;

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Request> rList = entities.Requests.Where(x => x.Status.Equals("Approved")).ToList();

                foreach (Request r in rList)
                {
                    foreach (RequestDetailVM rd in RequestDetailBL.GetReqDetList(r.ReqId))
                    {
                        if (rd.ItemCode.Equals(iCode))
                        {
                            outReqQty += (rd.ReqQty - rd.AwaitQty - rd.FulfilledQty);
                        }
                    }
                }
            }
            return outReqQty;
        }


        //Get Retrieve Items
        public static List<ItemVM> GetRetrieveItems()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<ItemVM> iList = GetAllItems();
                    for (int i = 0; i < iList.Count; i++)
                    {
                        iList[i].TempQtyReq = 0;
                    }
                    List<RequestVM> rList = RequestBL.GetReq("Approved");
                    for (int j = 0; j < rList.Count; j++)
                    {
                        List<RequestDetailVM> rdList = RequestDetailBL.GetReqDetList(rList[j].ReqId);
                        for (int k = 0; k < rdList.Count; k++)
                        {
                            int shortQty = rdList[k].ReqQty - rdList[k].AwaitQty - rdList[k].FulfilledQty;
                            if (shortQty > 0)
                                iList.Find(x => x.ItemCode.Equals(rdList[k].ItemCode)).TempQtyReq += shortQty;
                        }
                    }
                    return iList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #region
                //List<RequestDetailVM> reqdList = entities.Requests.Where(r => r.Status.Equals("Approved"))
                //    .Join(entities.RequestDetails, r => r.ReqId, rd => rd.ReqId, (r, rd) => new { r, rd })
                //    .Select(rd => new RequestDetailVM
                //    {
                //        ReqId = rd.rd.ReqId,
                //        ReqLineNo = rd.rd.ReqLineNo,
                //        ItemCode = rd.rd.ItemCode,
                //        ReqQty = rd.rd.ReqQty,
                //        AwaitQty = rd.rd.AwaitQty,
                //        FulfilledQty = rd.rd.FulfilledQty
                //    }).ToList();

                //List<ItemVM> iList = GetAllItems();
                //List<ItemVM> ritemlist = new List<ItemVM>();
                //foreach (ItemVM item in iList)
                //{
                //    item.TempQtyReq = 0;
                //    foreach (RequestDetailVM rd in reqdList)
                //    {
                //        if (rd.ReqQty - rd.FulfilledQty - rd.AwaitQty > 0 && item.ItemCode.Equals(rd.ItemCode))
                //        {
                //            //   item.TempQtyReq += rd.ReqQty - rd.AwaitQty - rd.FulfilledQty;
                //            iList.ToList().Find(x => x.ItemCode.Equals(rd.ItemCode)).TempQtyReq += rd.ReqQty - rd.AwaitQty - rd.FulfilledQty;

                //        }
                //    }
                //}

                //ritemlist = iList.Where(x => x.TempQtyReq > 0).ToList();

                //List<RequestVM> rlist = BusinessLogic.RequestBL.GetReq("Approved");
                //foreach (RequestVM r in rlist)
                //{
                //    List<RequestDetail> rdlist = entities.RequestDetails.Where(rd => rd.ReqId == r.ReqId).ToList();

                //    foreach (RequestDetail rd in rdlist)
                //    {
                //        if (rd.ReqQty - rd.FulfilledQty > 0)
                //        {
                //            iList.ToList().Find(x => x.ItemCode.Equals(rd.ItemCode)).TempQtyReq += rd.ReqQty - rd.AwaitQty - rd.FulfilledQty;



                //        }
                //    }
                //}
                //return ritemlist;
                #endregion
            }
        }

        //get All Items
        public static List<ItemVM> GetAllItems()
        {
            List<ItemVM> itemlist = new List<ItemVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                itemlist = entities.Items
                           .Select(i => new ItemVM()
                           {
                               ItemCode = i.ItemCode,
                               Cat = i.Cat,
                               Desc = i.Desc,
                               Location = i.Location,
                               UOM = i.UOM,
                               IsActive = i.IsActive,
                               Balance = i.Balance,
                               ReorderLevel = i.ReorderLevel,
                               ReorderQty = i.ReorderQty,
                               TempQtyDisb = i.TempQtyDisb,
                               TempQtyCheck = i.TempQtyCheck,
                               SuppCode1 = i.SuppCode1,
                               SuppCode2 = i.SuppCode2,
                               SuppCode3 = i.SuppCode3,
                               Price1 = i.Price1 ?? default(double),
                               Price2 = i.Price2 ?? default(double),
                               Price3 = i.Price3 ?? default(double)
                           }).ToList<ItemVM>();
            }
            return itemlist;
        }


        //ResetQtyDisb
        public static List<ItemVM> ResetQtyDisb()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Item> itemlist = entities.Items.ToList<Item>();
                List<Item> updateditemlist = new List<Item>();
                List<ItemVM> itemlistvm = new List<ItemVM>();
                foreach (Item item in itemlist)
                {
                    Item updateitem = entities.Items.Where(i => i.ItemCode == item.ItemCode).First<Item>();
                    updateitem.TempQtyDisb = 0;
                    entities.SaveChanges();

                    updateditemlist.Add(updateitem);
                }

                itemlistvm = Utility.ItemUtility.Convert_Item_To_ItemVM(updateditemlist);
                return itemlistvm;
            }
        }

        //GetQtyDisb
        public static List<ItemVM> GetQtyDisb()
        {
            List<ItemVM> itemvmlist = new List<ItemVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Item> items = entities.Items.ToList<Item>();

                itemvmlist = Utility.ItemUtility.Convert_Item_To_ItemVM(items);
                return itemvmlist;
            }
        }


        //FulfillRequest
        public static List<ItemVM> FulfillRequest(List<ItemVM> items)
        {
            List<RequestDetailVM> fulfilledList = new List<RequestDetailVM>();
            List<DepartmentVM> deptList = DepartmentBL.GetAllDept();
            for (int i = 0; i < deptList.Count; i++) deptList[i].FulfilledQty = 0;
            for (int i = 0; i < items.Count; i++)
            {
                int count = 0;
                if (items[i].TempQtyDisb > items[i].Balance) count = items[i].Balance;
                else count = (int)items[i].TempQtyDisb;
                List<RequestVM> rvmList = RequestBL.GetReq("Approved");
                for (int j = 0; j < rvmList.Count; j++)
                {
                    if (count > 0)
                    {
                        string deptCode = EmployeeBL.GetEmp(rvmList[j].EmpId).DeptCode;
                        List<RequestDetailVM> rdvmList = RequestDetailBL.GetReqDetList(rvmList[j].ReqId);
                        for (int k = 0; k < rdvmList.Count; k++)
                        {
                            if (items[i].ItemCode.Equals(rdvmList[k].ItemCode))
                            {
                                int shortQty = rdvmList[k].ReqQty - rdvmList[k].AwaitQty - rdvmList[k].FulfilledQty;
                                if (shortQty <= count)
                                {
                                    count = count - shortQty;
                                    items[i].Balance = items[i].Balance - shortQty;
                                    rdvmList[k].AwaitQty = rdvmList[k].AwaitQty + shortQty;
                                }
                                else
                                {
                                    items[i].Balance = items[i].Balance - count;
                                    rdvmList[k].AwaitQty = rdvmList[k].AwaitQty + count;
                                    count = 0;
                                }
                                fulfilledList.Add(rdvmList[k]);
                                UpdateBal(items[i].ItemCode, items[i].Balance);
                                UpdateAwait(rdvmList[k].ReqId, rdvmList[k].ItemCode, rdvmList[k].AwaitQty);
                                deptList.Find(x => x.DeptCode == deptCode).FulfilledQty += shortQty;
                            }
                        }
                    }
                }
            }

            #region
            //SA46Team08ADProjectContext ctx = new SA46Team08ADProjectContext();
            //List<RequestDetail> fulfilledList = new List<RequestDetail>();
            //List<RequestDetail> rdList = RequestDetailBL.GetReqDetList("Approved");
            //List<DepartmentVM> deptList = DepartmentBL.GetAllDept();
            //for (int i = 0; i < deptList.Count; i++) deptList[i].FulfilledQty = 0;
            //for (int i = 0; i < items.Count; i++)
            //{
            //    int count = 0;
            //    if (items[i].TempQtyDisb > items[i].Balance) count = items[i].Balance;
            //    else count = (int)items[i].TempQtyDisb;
            //    for (int j = 0; j < rdList.Count; j++)
            //    {
            //        int reqId = rdList[j].ReqId;
            //        Request req = ctx.Requests.Where(x => x.ReqId == reqId).First();
            //        Employee emp = ctx.Employees.Where(x => x.EmpId == req.EmpId).First();
            //        Department dept = ctx.Departments.Where(x => x.DeptCode.Equals(emp.DeptCode)).First();
            //        if (count > 0)
            //        {                     
            //            if (items[i].ItemCode.Equals(rdList[j].ItemCode))
            //            {
            //                int shortQty = rdList[j].ReqQty - rdList[j].AwaitQty - rdList[j].FulfilledQty;
            //                if (shortQty <= count)
            //                {
            //                    count = count - shortQty;
            //                    items[i].Balance = items[i].Balance - shortQty;
            //                    rdList[j].AwaitQty = rdList[j].AwaitQty + shortQty;
            //                }
            //                else
            //                {
            //                    items[i].Balance = items[i].Balance - count;
            //                    rdList[j].AwaitQty = rdList[j].AwaitQty + count;
            //                    count = 0;
            //                }
            //                fulfilledList.Add(rdList[j]);
            //                UpdateBal(items[i].ItemCode, items[i].Balance);
            //                UpdateAwait(rdList[j].ReqId, rdList[j].ItemCode, rdList[j].AwaitQty);
            //                deptList.Find(x => x.DeptCode == dept.DeptCode).FulfilledQty += shortQty;
            //            }
            //        }

            //    }
            //}
            #endregion

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                for (int i = 0; i < deptList.Count; i++)
                {
                    int fromEmpId = entities.Employees.Where(x => x.Role == "Store Clerk").First().EmpId;
                    int toEmpId = (int)deptList[i].DeptRepId;
                    string type = "Weekly Disbursement";
                    string content;
                    if (!deptList[i].DeptCode.Equals("STOR"))
                    {
                        if (deptList[i].FulfilledQty == 0)
                        {
                            content = "Disbursement Notification: There is no stationery disbursed for your department this week. Have a nice day.";
                        }
                        else
                        {
                            int cId = (int)deptList[i].ColPtId;
                            CollectionPoint cp = entities.CollectionPoints.Where(x => x.ColPtId == cId).FirstOrDefault();
                            DateTime today = DateTime.Now;
                            DateTime colDay = DateTime.Now;
                            if (today.DayOfWeek.Equals(DayOfWeek.Monday)) colDay = today.AddDays(7);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Tuesday)) colDay = today.AddDays(6);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Wednesday)) colDay = today.AddDays(5);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Thursday)) colDay = today.AddDays(4);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Friday)) colDay = today.AddDays(3);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Saturday)) colDay = today.AddDays(2);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Sunday)) colDay = today.AddDays(1);
                            string format = "dd-MMM-yyyy";
                            string date = colDay.ToString(format);
                            content = "Please collect stationery for your department at " + cp.Location + " on " + date + ", " + cp.Time;
                        }
                        NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);
                    }
                }

                List<Employee> clerkList = entities.Employees.Where(x => x.Role.Equals("Store Clerk")).ToList();
                for (int i = 0; i < clerkList.Count; i++)
                {
                    int fromEmpId = entities.Employees.Where(x => x.Role == "Store Clerk").First().EmpId;
                    int toEmpId = clerkList[i].EmpId;
                    string type = "Weekly Disbursement";
                    string empName = entities.Employees.Where(x => x.Role == "Store Clerk").First().EmpName;
                    string content = "Disbursement Notification: Disbursement has recently been conducted by " + empName;
                    NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);
                }
            }

            ////Making PDF Reports
            ////Group By Department then By Item
            SA46Team08ADProjectContext ctx = new SA46Team08ADProjectContext();
            List<RequestDetailVM> rdList = new List<RequestDetailVM>();

            List<DisbursementDetailVM> dListDept = new List<DisbursementDetailVM>();
            List<DisbursementDetailVM> dListEmployee = new List<DisbursementDetailVM>();

            for (int i = 0; i < deptList.Count; i++)
            {
                if (!deptList[i].DeptCode.Equals("STOR"))
                {
                    for (int j = 0; j < fulfilledList.Count; j++)
                    {
                        if (GetDeptCode(fulfilledList[j].ReqId).Equals(deptList[i].DeptCode) && !rdList.Contains(fulfilledList[j]))
                        {
                            rdList.Add(fulfilledList[j]);

                            DisbursementDetailVM disDet = new DisbursementDetailVM();
                            disDet.DeptCode = deptList[i].DeptCode;
                            string itemCode = fulfilledList[j].ItemCode;
                            disDet.ItemCode = itemCode;
                            Item item = ctx.Items.Where(x => x.ItemCode.Equals(itemCode)).FirstOrDefault();
                            disDet.Category = item.Cat;
                            disDet.Description = item.Desc;
                            disDet.ReqQty = fulfilledList[i].ReqQty;
                            disDet.AwaitQty = fulfilledList[i].AwaitQty;
                            disDet.FulfilledQty = fulfilledList[i].FulfilledQty;
                            disDet.EmpId = 0;
                            disDet.ReqId = 0;
                            dListDept.Add(disDet);
                        }
                        else if (GetDeptCode(fulfilledList[j].ReqId).Equals(deptList[i].DeptCode) && rdList.Contains(fulfilledList[j]))
                        {
                            rdList.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).ReqQty += fulfilledList[j].ReqQty;
                            rdList.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).AwaitQty += fulfilledList[j].AwaitQty;
                            rdList.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).FulfilledQty += fulfilledList[j].FulfilledQty;
                            dListDept.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).ReqQty += fulfilledList[j].ReqQty;
                            dListDept.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).AwaitQty += fulfilledList[j].AwaitQty;
                            dListDept.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).FulfilledQty += fulfilledList[j].FulfilledQty;
                        }
                    }
                }                  
            }
            List<DisbursementDetailVM> disbursementListDept = dListDept.OrderBy(x => x.ItemCode).OrderBy(x => x.DeptCode).ToList();
            // disbursementListDept, list of disbursement sorted by deptCode and then itemCode, to be used for pdf export
            string filename = "DisbursementListByDepartment_" + DateTime.Now.ToString("yyyMMddHHmmss") + ".pdf";
            PdfBL.GenerateDisbursementListbyDept(disbursementListDept, filename);

            ////Group By Department then By Item
            for (int i = 0; i < fulfilledList.Count; i++)
            {
                DisbursementDetailVM disDet = new DisbursementDetailVM();
                int reqId = fulfilledList[i].ReqId;
                Request req = ctx.Requests.Where(x => x.ReqId == reqId).FirstOrDefault();
                Employee emp = ctx.Employees.Where(x => x.EmpId == req.EmpId).FirstOrDefault();
                string itemCode = fulfilledList[i].ItemCode;
                Item item = ctx.Items.Where(x => x.ItemCode.Equals(itemCode)).FirstOrDefault();
                disDet.DeptCode = emp.DeptCode;
                disDet.ItemCode = fulfilledList[i].ItemCode;
                disDet.Category = item.Cat;
                disDet.Description = item.Desc;
                disDet.ReqQty = fulfilledList[i].ReqQty;
                disDet.AwaitQty = fulfilledList[i].AwaitQty;
                disDet.FulfilledQty = fulfilledList[i].FulfilledQty;
                disDet.EmpId = emp.EmpId;
                disDet.ReqId = req.ReqId;
                dListEmployee.Add(disDet);
            }
            List<DisbursementDetailVM> disbursementListEmployee = dListEmployee.OrderBy(x => x.ItemCode).OrderBy(x => x.ReqId).OrderBy(x => x.EmpId).OrderBy(x => x.DeptCode).ToList();
            // disbursementListEmployee, list of disbursement sorted by deptCode, empId, reqId, and then itemCode, to be used for pdf export

            // call make PDF method

            // for email
            List<Employee> clerklist = ctx.Employees.Where(x => x.Role.Equals("Store Clerk")).ToList();
            for (int i = 0; i < clerklist.Count; i++)
            {
                int empId = clerklist[i].EmpId;
                EmailBL.SendDisbEmailForClerk(empId, dListDept, dListEmployee);
            }

            List<DepartmentVM> deptlist = DepartmentBL.GetAllDept();
            for (int i = 0; i < deptlist.Count; i++)
            {
                if (!deptlist[i].DeptCode.Equals("STOR"))
                {
                    int empId = (int)deptlist[i].DeptRepId;
                    string deptCode = deptlist[i].DeptCode;
                    EmailBL.SendDisbEmailForRep(empId, deptCode, disbursementListDept, disbursementListEmployee);
                }
            }

            return items;
        }


        //FulfillRequestUrgent
        public static List<ItemVM> FulfillRequestUrgent(int empId, List<ItemVM> items)
        {
            List<RequestDetailVM> fulfilledList = new List<RequestDetailVM>();
            List<DepartmentVM> deptList = DepartmentBL.GetAllDept();
            for (int i = 0; i < deptList.Count; i++) deptList[i].FulfilledQty = 0;
            for (int i = 0; i < items.Count; i++)
            {
                int count = 0;
                if (items[i].TempQtyDisb > items[i].Balance) count = items[i].Balance;
                else count = (int)items[i].TempQtyDisb;
                List<RequestVM> rvmList = RequestBL.GetReq("Approved");
                for (int j = 0; j < rvmList.Count; j++)
                {
                    if (count > 0)
                    {
                        string deptCode = EmployeeBL.GetEmp(rvmList[j].EmpId).DeptCode;
                        List<RequestDetailVM> rdvmList = RequestDetailBL.GetReqDetList(rvmList[j].ReqId);
                        for (int k = 0; k < rdvmList.Count; k++)
                        {
                            if (items[i].ItemCode.Equals(rdvmList[k].ItemCode))
                            {
                                int shortQty = rdvmList[k].ReqQty - rdvmList[k].AwaitQty - rdvmList[k].FulfilledQty;
                                if (shortQty <= count)
                                {
                                    count = count - shortQty;
                                    items[i].Balance = items[i].Balance - shortQty;
                                    rdvmList[k].AwaitQty = rdvmList[k].AwaitQty + shortQty;
                                }
                                else
                                {
                                    items[i].Balance = items[i].Balance - count;
                                    rdvmList[k].AwaitQty = rdvmList[k].AwaitQty + count;
                                    count = 0;
                                }
                                fulfilledList.Add(rdvmList[k]);
                                UpdateBal(items[i].ItemCode, items[i].Balance);
                                UpdateAwait(rdvmList[k].ReqId, rdvmList[k].ItemCode, rdvmList[k].AwaitQty);
                                deptList.Find(x => x.DeptCode == deptCode).FulfilledQty += shortQty;
                            }
                        }
                    }
                }
            }

            #region
            //SA46Team08ADProjectContext ctx = new SA46Team08ADProjectContext();
            //List<RequestDetail> fulfilledList = new List<RequestDetail>();
            //List<RequestDetail> rdList = RequestDetailBL.GetReqDetList("Approved");
            //List<DepartmentVM> deptList = DepartmentBL.GetAllDept();
            //for (int i = 0; i < deptList.Count; i++) deptList[i].FulfilledQty = 0;
            //for (int i = 0; i < items.Count; i++)
            //{
            //    int count = 0;
            //    if (items[i].TempQtyDisb > items[i].Balance) count = items[i].Balance;
            //    else count = (int)items[i].TempQtyDisb;
            //    for (int j = 0; j < rdList.Count; j++)
            //    {
            //        int reqId = rdList[j].ReqId;
            //        Request req = ctx.Requests.Where(x => x.ReqId == reqId).First();
            //        Employee emp = ctx.Employees.Where(x => x.EmpId == req.EmpId).First();
            //        Department dept = ctx.Departments.Where(x => x.DeptCode.Equals(emp.DeptCode)).First();
            //        if (count > 0)
            //        {                     
            //            if (items[i].ItemCode.Equals(rdList[j].ItemCode))
            //            {
            //                int shortQty = rdList[j].ReqQty - rdList[j].AwaitQty - rdList[j].FulfilledQty;
            //                if (shortQty <= count)
            //                {
            //                    count = count - shortQty;
            //                    items[i].Balance = items[i].Balance - shortQty;
            //                    rdList[j].AwaitQty = rdList[j].AwaitQty + shortQty;
            //                }
            //                else
            //                {
            //                    items[i].Balance = items[i].Balance - count;
            //                    rdList[j].AwaitQty = rdList[j].AwaitQty + count;
            //                    count = 0;
            //                }
            //                fulfilledList.Add(rdList[j]);
            //                UpdateBal(items[i].ItemCode, items[i].Balance);
            //                UpdateAwait(rdList[j].ReqId, rdList[j].ItemCode, rdList[j].AwaitQty);
            //                deptList.Find(x => x.DeptCode == dept.DeptCode).FulfilledQty += shortQty;
            //            }
            //        }

            //    }
            //}
            #endregion

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                for (int i = 0; i < deptList.Count; i++)
                {
                    int fromEmpId = entities.Employees.Where(x => x.Role == "Store Clerk").First().EmpId;
                    int toEmpId = (int)deptList[i].DeptRepId;
                    string type = "Weekly Disbursement";
                    string content;
                    if (!deptList[i].DeptCode.Equals("STOR"))
                    {
                        if (deptList[i].FulfilledQty == 0)
                        {
                            content = "Disbursement Notification: There is no stationery disbursed for your department this week. Have a nice day.";
                        }
                        else
                        {
                            int cId = (int)deptList[i].ColPtId;
                            CollectionPoint cp = entities.CollectionPoints.Where(x => x.ColPtId == cId).FirstOrDefault();
                            DateTime today = DateTime.Now;
                            DateTime colDay = DateTime.Now;
                            if (today.DayOfWeek.Equals(DayOfWeek.Monday)) colDay = today.AddDays(7);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Tuesday)) colDay = today.AddDays(6);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Wednesday)) colDay = today.AddDays(5);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Thursday)) colDay = today.AddDays(4);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Friday)) colDay = today.AddDays(3);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Saturday)) colDay = today.AddDays(2);
                            else if (today.DayOfWeek.Equals(DayOfWeek.Sunday)) colDay = today.AddDays(1);
                            string format = "dd-MMM-yyyy";
                            string date = colDay.ToString(format);
                            content = "Please collect stationery for your department at " + cp.Location + " on " + date + ", " + cp.Time;
                        }
                        NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);
                    }
                }

                List<Employee> clerkList = entities.Employees.Where(x => x.Role.Equals("Store Clerk")).ToList();
                for (int i = 0; i < clerkList.Count; i++)
                {
                    int fromEmpId = entities.Employees.Where(x => x.Role == "Store Clerk").First().EmpId;
                    int toEmpId = clerkList[i].EmpId;
                    string type = "Weekly Disbursement";
                    string empName = entities.Employees.Where(x => x.Role == "Store Clerk").First().EmpName;
                    string content = "Disbursement Notification: Disbursement has recently been conducted by " + empName;
                    NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);
                }
            }

            SA46Team08ADProjectContext ctx = new SA46Team08ADProjectContext();
            int urgentFromId = ctx.Employees.Where(x => x.Role == "Store Clerk").First().EmpId;
            int urgentToId = empId;
            string urgentType = "Urgent Request";
            string urgentContent = "Your urgent request has been fulfilled, please wait for disbursement";
            NotificationBL.AddNewNotification(urgentFromId, urgentToId, urgentType, urgentContent);

            ////Making PDF Reports
            ////Group By Department then By Item
            List<RequestDetailVM> rdList = new List<RequestDetailVM>();

            List<DisbursementDetailVM> dListDept = new List<DisbursementDetailVM>();
            List<DisbursementDetailVM> dListEmployee = new List<DisbursementDetailVM>();

            for (int i = 0; i < deptList.Count; i++)
            {
                if (!deptList[i].DeptCode.Equals("STOR"))
                {
                    for (int j = 0; j < fulfilledList.Count; j++)
                    {
                        if (GetDeptCode(fulfilledList[j].ReqId).Equals(deptList[i].DeptCode) && !rdList.Contains(fulfilledList[j]))
                        {
                            rdList.Add(fulfilledList[j]);

                            DisbursementDetailVM disDet = new DisbursementDetailVM();
                            disDet.DeptCode = deptList[i].DeptCode;
                            string itemCode = fulfilledList[j].ItemCode;
                            disDet.ItemCode = itemCode;
                            Item item = ctx.Items.Where(x => x.ItemCode.Equals(itemCode)).FirstOrDefault();
                            disDet.Category = item.Cat;
                            disDet.Description = item.Desc;
                            disDet.ReqQty = fulfilledList[i].ReqQty;
                            disDet.AwaitQty = fulfilledList[i].AwaitQty;
                            disDet.FulfilledQty = fulfilledList[i].FulfilledQty;
                            disDet.EmpId = 0;
                            disDet.ReqId = 0;
                            dListDept.Add(disDet);
                        }
                        else if (GetDeptCode(fulfilledList[j].ReqId).Equals(deptList[i].DeptCode) && rdList.Contains(fulfilledList[j]))
                        {
                            rdList.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).ReqQty += fulfilledList[j].ReqQty;
                            rdList.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).AwaitQty += fulfilledList[j].AwaitQty;
                            rdList.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).FulfilledQty += fulfilledList[j].FulfilledQty;
                            dListDept.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).ReqQty += fulfilledList[j].ReqQty;
                            dListDept.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).AwaitQty += fulfilledList[j].AwaitQty;
                            dListDept.Find(x => x.ItemCode.Equals(fulfilledList[j].ItemCode)).FulfilledQty += fulfilledList[j].FulfilledQty;
                        }
                    }
                }
            }
            List<DisbursementDetailVM> disbursementListDept = dListDept.OrderBy(x => x.ItemCode).OrderBy(x => x.DeptCode).ToList();
            // disbursementListDept, list of disbursement sorted by deptCode and then itemCode, to be used for pdf export
            string filename = "DisbursementListByDepartment_" + DateTime.Now.ToString("yyyMMddHHmmss") + ".pdf";
            PdfBL.GenerateDisbursementListbyDept(disbursementListDept, filename);

            ////Group By Department then By Item
            for (int i = 0; i < fulfilledList.Count; i++)
            {
                DisbursementDetailVM disDet = new DisbursementDetailVM();
                Request req = ctx.Requests.Where(x => x.ReqId == fulfilledList[i].ReqId).FirstOrDefault();
                Employee emp = ctx.Employees.Where(x => x.EmpId == req.EmpId).FirstOrDefault();
                Item item = ctx.Items.Where(x => x.ItemCode.Equals(fulfilledList[i].ItemCode)).FirstOrDefault();
                disDet.DeptCode = emp.DeptCode;
                disDet.ItemCode = fulfilledList[i].ItemCode;
                disDet.Category = item.Cat;
                disDet.Description = item.Desc;
                disDet.ReqQty = fulfilledList[i].ReqQty;
                disDet.AwaitQty = fulfilledList[i].AwaitQty;
                disDet.FulfilledQty = fulfilledList[i].FulfilledQty;
                disDet.EmpId = emp.EmpId;
                disDet.ReqId = req.ReqId;
                dListEmployee.Add(disDet);
            }
            List<DisbursementDetailVM> disbursementListEmployee = dListEmployee.OrderBy(x => x.ItemCode).OrderBy(x => x.ReqId).OrderBy(x => x.EmpId).OrderBy(x => x.DeptCode).ToList();
            // disbursementListEmployee, list of disbursement sorted by deptCode, empId, reqId, and then itemCode, to be used for pdf export

            return items;
        }


        #region
        //FulfillRequestUrgent
        //public static void FulfillRequestUrgent(int empId, List<ItemVM> items, DateTime D1, int Collpt)
        //{
        //    List<RequestDetailVM> fulfilledList = new List<RequestDetailVM>();

        //    foreach (ItemVM i in items)
        //    {
        //        int count = (i.TempQtyDisb > i.Balance) ? i.Balance : i.TempQtyDisb ?? default(int);

        //        foreach (RequestVM r in RequestBL.GetReq(empId, "Approved"))
        //        {
        //            if (count > 0)
        //            {
        //                string deptCode = EmployeeBL.GetEmp(r.EmpId).DeptCode;

        //                foreach (RequestDetailVM rd in RequestDetailBL.GetReqDetList(r.ReqId))
        //                {
        //                    if (count > 0)
        //                    {
        //                        if (i.ItemCode.Equals(rd.ItemCode))
        //                        {
        //                            int shortQty = (rd.ReqQty - rd.FulfilledQty);

        //                            if (shortQty <= count)
        //                            {
        //                                shortQty = 0;
        //                                count -= shortQty;
        //                                i.Balance -= shortQty;
        //                                rd.AwaitQty += shortQty;
        //                                try
        //                                {
        //                                    UpdateBal(rd.ItemCode, i.Balance);
        //                                    fulfilledList.Add(rd);
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    break;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                shortQty -= count;
        //                                count = 0;
        //                                i.Balance -= count;
        //                                rd.AwaitQty += count;
        //                                try
        //                                {
        //                                    UpdateBal(rd.ItemCode, i.Balance);
        //                                    fulfilledList.Add(rd);
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    break;
        //                                }
        //                            }
        //                        }

        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    //Making PDF Reports
        //    //Group By Department then By Item
        //    List<RequestDetailVM> ListByDept = new List<RequestDetailVM>();

        //    foreach (string d in DepartmentBL.GetDeptCodes())
        //    {
        //        List<RequestDetailVM> rdList = new List<RequestDetailVM>();

        //        foreach (RequestDetailVM rd in fulfilledList)
        //        {
        //            if (GetDeptCode(rd.ReqId).Equals(d))
        //            {
        //                if (rdList.Contains(rd))
        //                {
        //                    rdList.Find(x => x.ItemCode.Equals(rd.ItemCode)).AwaitQty += rd.AwaitQty;
        //                }
        //                else
        //                {
        //                    rdList.Add(rd);
        //                }

        //                ListByDept.AddRange(rdList);
        //            }
        //        }
        //    }


        //}

        #endregion

        //Get DeptCode by request id
        public static string GetDeptCode(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string DeptCode = entities.Requests.Where(r => r.ReqId == reqId)
                                   .Join(entities.Employees, r => r.EmpId, e => e.EmpId, (r, e) => new { r, e })
                                   .Select(d => d.e.DeptCode).ToString();
                return DeptCode;
            }

        }
       
        //Update Balnce
        public static void UpdateBal(string iCode, int bal)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Item item = entities.Items.Where(i => i.ItemCode.Equals(iCode)).First();
                item.Balance = bal;
                entities.SaveChanges();
            }
        }



        //Check Low Stock By Item
        public static bool CheckLowStk(ItemVM i)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Item item = entities.Items.Where(x => x.ItemCode.Equals(i.ItemCode) && x.Balance < x.ReorderLevel).FirstOrDefault<Item>();

                if (item == null)
                {
                    return false;
                }
                else
                    return true;
            }

        }


        //GetEmpItems by empId
        public static List<ItemVM> GetEmpItems(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                List<RequestDetailVM> reqdList = entities.Requests.Where(r => r.Status.Equals("Approved") && r.EmpId == empId)
                   .Join(entities.RequestDetails, r => r.ReqId, rd => rd.ReqId, (r, rd) => new { r, rd })
                   .Select(rd => new RequestDetailVM
                   {
                       ReqId = rd.rd.ReqId,
                       ReqLineNo = rd.rd.ReqLineNo,
                       ItemCode = rd.rd.ItemCode,
                       ReqQty = rd.rd.ReqQty,
                       AwaitQty = rd.rd.AwaitQty,
                       FulfilledQty = rd.rd.FulfilledQty
                   }).ToList();

                List<ItemVM> iList = GetAllItems();
                List<ItemVM> ritemlist = new List<ItemVM>();
                foreach (ItemVM item in iList)
                {
                    item.TempQtyReq = 0;
                    foreach (RequestDetailVM rd in reqdList)
                    {
                        if (rd.ReqQty - rd.FulfilledQty > 0 && item.ItemCode.Equals(rd.ItemCode))
                        {
                            //   item.TempQtyReq += rd.ReqQty - rd.AwaitQty - rd.FulfilledQty;
                            iList.ToList().Find(x => x.ItemCode.Equals(rd.ItemCode)).TempQtyReq += rd.ReqQty - rd.AwaitQty - rd.FulfilledQty;

                        }
                    }
                }

                ritemlist = iList.Where(x => x.TempQtyReq > 0).ToList();

                //List<Request> rList = entities.Requests.Where(r => r.EmpId == empId && r.Status.Equals("Approved")).ToList();

                //List<RequestDetail> rdList = new List<RequestDetail>();

                //List<ItemVM> iList = new List<ItemVM>();

                //foreach (Request r in rList)
                //{
                //    List<RequestDetail> reqdetailLists = entities.RequestDetails.Where(rd => rd.ReqId == r.ReqId).ToList();
                //    rdList.AddRange(reqdetailLists);
                //}

                //foreach (RequestDetail rd in rdList)
                //{
                //    List<Item> itemlists = entities.Items.Where(i => i.ItemCode.Equals(rd.ItemCode)).ToList(); //need to refer to retrieve item method *************************************************************************

                //    iList.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(itemlists));
                //}

                return ritemlist;

            }
        }
        //get Items by ItemCode
        public static ItemVM GetItem(string itemCode)
        {
            ItemVM item = new ItemVM();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                item = entities.Items
                            .Where(i => i.ItemCode == itemCode)
                            .Select(i => new ItemVM()
                            {
                                ItemCode = i.ItemCode,
                                Cat = i.Cat,
                                Desc = i.Desc,
                                Location = i.Location,
                                UOM = i.UOM,
                                IsActive = i.IsActive,
                                Balance = i.Balance,
                                ReorderLevel = i.ReorderLevel,
                                ReorderQty = i.ReorderQty,
                                TempQtyDisb = i.TempQtyDisb,
                                TempQtyCheck = i.TempQtyCheck,
                                TempQtyReq = 0,
                                SuppCode1 = i.SuppCode1,
                                SuppCode2 = i.SuppCode2,
                                SuppCode3 = i.SuppCode3,
                                Price1 = i.Price1 ?? default(double),
                                Price2 = i.Price2 ?? default(double),
                                Price3 = i.Price3 ?? default(double)
                            }).SingleOrDefault<ItemVM>();
            }
            return item;
        }


        //ReceiveItem
        public static void ReceiveItem(string suppCode, int qty, string ItemCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Item item = entities.Items.Where(i => i.ItemCode.Equals(ItemCode)).First();

                item.Balance += qty;
                entities.SaveChanges();

                double price = 0;
                if (suppCode.Equals(item.SuppCode1))
                {
                    price = item.Price1 ?? default(double);
                }
                if (suppCode.Equals(item.SuppCode2))
                {
                    price = item.Price2 ?? default(double);
                }
                if (suppCode.Equals(item.SuppCode3))
                {
                    price = item.Price3 ?? default(double);
                }

                TransactionVM t = new TransactionVM();
                t.TranDateTime = DateTime.Now;
                t.ItemCode = item.ItemCode;
                t.QtyChange = qty;
                t.UnitPrice = price;
                t.Chargeback = price * qty;
                t.Balance = item.Balance;
                t.Desc = "Purchased";
                t.SuppCode = suppCode;

                TransactionBL.AddTran(t);
            }
        }

        //SaveQtyDisb
        public static void SaveQtyDisb(string ItemCode, int qtyDisb)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Item item = entities.Items.Where(i => i.ItemCode.Equals(ItemCode)).First();
                item.TempQtyDisb = qtyDisb;
                entities.SaveChanges();
            }
        }

        //ResetQtyChk
        public static List<ItemVM> ResetQtyChk()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Item> itemlist = entities.Items.ToList<Item>();
                List<Item> updateditemlist = new List<Item>();
                List<ItemVM> itemlistvm = new List<ItemVM>();
                foreach (Item item in itemlist)
                {
                    Item updateitem = entities.Items.Where(i => i.ItemCode == item.ItemCode).First<Item>();
                    updateitem.TempQtyCheck = 0;
                    entities.SaveChanges();

                    updateditemlist.Add(updateitem);
                }

                itemlistvm = Utility.ItemUtility.Convert_Item_To_ItemVM(updateditemlist);
                return itemlistvm;
            }
        }

        //GetQtyChk
        public static List<ItemVM> GetQtyChk()
        {
            List<ItemVM> itemvmlist = new List<ItemVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Item> items = entities.Items.ToList<Item>();

                itemvmlist = Utility.ItemUtility.Convert_Item_To_ItemVM(items);
                return itemvmlist;
            }
        }

        //SaveQtyChk
        public static void SaveQtyChk(string ItemCode, int qtyChk)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Item item = entities.Items.Where(i => i.ItemCode.Equals(ItemCode)).First();
                item.TempQtyCheck = qtyChk;
                entities.SaveChanges();
            }
        }


        //UpdateItem
        public static void UpdateItem(string itemCode, int reorderLvl, int reorderQty, string s1, double p1, string s2, double p2, string s3, double p3)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Item item = entities.Items.Where(i => i.ItemCode.Equals(itemCode)).First();
                item.ReorderLevel = reorderLvl;
                item.ReorderQty = reorderQty;
                item.SuppCode1 = s1;
                item.SuppCode2 = s2;
                item.SuppCode3 = s3;
                item.Price1 = p1;
                item.Price2 = p2;
                item.Price3 = p3;
                entities.SaveChanges();
            }
        }

        //UpdateItemLists
        public static void UpdateItemLists(List<ItemVM> iList)
        {
            foreach (ItemVM i in iList)
            {

                UpdateItem(i.ItemCode, i.ReorderLevel, i.ReorderQty, i.SuppCode1, i.Price1, i.SuppCode2, i.Price2, i.SuppCode3, i.Price3);

            }
        }

        //GetItems by  Threshold
        public static List<ItemVM> GetAllItemsbyThreshold()
        {
            List<ItemVM> itemlist = new List<ItemVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Item> iList = entities.Items.ToList();
                itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                foreach (ItemVM item in itemlist)
                {
                    item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                    item.ReccReorderLvl = item.ReccReorderQty * 2;
                    item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                    item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                }
                double threshold = 0.3;
                itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                return itemlist;
            }

        }
        //GetItems by Cat, Desc and Threshold
        public static List<ItemVM> GetItems(string cat, string desc, double threshold)
        {
            List<ItemVM> itemlist = new List<ItemVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                if (cat == null && desc == null && threshold > 0)  //1
                {

                    List<Item> iList = entities.Items.ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }

                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (desc != null && cat == null && threshold > 0) //2
                {
                    List<Item> iList = entities.Items.Where(i => i.Desc.Contains(desc)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }

                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (desc == null && cat != null && threshold > 0) //3
                {
                    List<Item> iList = entities.Items.Where(i => i.Cat.Contains(cat)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }

                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (desc != null && cat != null && threshold > 0) //3
                {
                    List<Item> iList = entities.Items.Where(i => i.Cat.Contains(cat) && i.Desc.Contains(desc)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }

                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }

                else if (cat != null && desc != null && threshold == 0) //4
                {
                    List<Item> iList = entities.Items.Where(i => i.Cat.Contains(cat) && i.Desc.Contains(desc)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }
                    threshold = 0.3;
                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (cat == null && desc == null && threshold == 0) //5
                {
                    List<Item> iList = entities.Items.ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }
                    threshold = 0.3;
                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (cat != null && desc == null && threshold == 0) //6
                {
                    List<Item> iList = entities.Items.Where(i => i.Cat.Contains(cat)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }
                    threshold = 0.3;
                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (cat == null && desc != null && threshold == 0) //7
                {
                    List<Item> iList = entities.Items.Where(i => i.Desc.Contains(desc)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }
                    threshold = 0.3;
                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else
                {
                    List<Item> iList = entities.Items.ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(iList));
                    foreach (ItemVM item in itemlist)
                    {
                        item.ReccReorderQty = Math.Max(item.ReorderQty, Get3monthReqandOutstandingReqs(item.ItemCode));
                        item.ReccReorderLvl = item.ReccReorderQty * 2;
                        item.lvlDiff = (item.ReccReorderLvl - item.ReccReorderQty) / item.ReccReorderQty;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }
                    threshold = 0.3;
                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
            }
        }



        //Update Await
        public static void UpdateAwait(int reqId, string iCode, int AwaitQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                RequestDetail requestDetail = entities.RequestDetails.Where(rd => rd.ReqId == reqId && rd.ItemCode == iCode).First();
                requestDetail.AwaitQty = AwaitQty;
                entities.SaveChanges();
            }
        }

        //Update FulFilled
        public static void UpdateFulfilled(int reqId, string iCode, int fulfilledQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                RequestDetail requestDetail = entities.RequestDetails.Where(rd => rd.ReqId == reqId && rd.ItemCode == iCode).First();
                requestDetail.FulfilledQty = fulfilledQty;
                entities.SaveChanges();
            }
        }
    }
}

