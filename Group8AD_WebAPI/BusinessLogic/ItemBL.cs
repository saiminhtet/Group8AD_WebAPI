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
                    List<Item> ilist1 = entities.Items.Where(i => i.Desc == desc).ToList();
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
                    List<Item> ilist = entities.Items.Where(i => i.Cat == cat && i.Desc.Contains(desc)).ToList();
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
                        Price1 = i.Price1,
                        Price2 = i.Price2,
                        Price3 = i.Price3
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

                var EmpIds = entities.Employees.Where(e => e.DeptCode.Equals(deptcode)).ToList();


                List<Request> rList = new List<Request>();
                List<RequestDetail> rdList = new List<RequestDetail>();
                List<ItemVM> iList = new List<ItemVM>();
                foreach (var e in EmpIds)
                {
                    List<Request> reqList = entities.Requests.Where(x => x.EmpId == e.EmpId && x.Status.Equals("Approved")).ToList();
                    rList.AddRange(reqList);
                }

                foreach (Request r in rList)
                {
                    List<RequestDetail> reqdList = entities.RequestDetails.Where(x => x.ReqId == r.ReqId).ToList();
                    rdList.AddRange(reqdList);
                }


                foreach (Request r in rList)
                {
                    foreach (RequestDetail rd in rdList.Where(rqd => rqd.ReqId == r.ReqId))
                    {

                        if (rd.AwaitQty > 0 && iList.Where(x => x.ItemCode.Equals(rd.ItemCode)).ToList().Count == 0)
                        {
                            ItemVM i = new ItemVM();
                            i.ItemCode = rd.ItemCode;
                            ItemVM io = GetItem(i.ItemCode);
                            i.Cat = io.Cat;
                            i.Desc = io.Desc;
                            i.UOM = io.UOM;
                            i.Price1 = io.Price1;
                            i.TempQtyAcpt = io.TempQtyAcpt;
                            i.TempQtyReq = io.TempQtyReq;

                            iList.Add(i);
                        }

                    }

                    foreach (RequestDetail rd in rdList.Where(rqd => rqd.ReqId == r.ReqId))
                    {
                        if (rd.AwaitQty > 0 && iList.Count > 0)                   //iList.Where(x => x.ItemCode.Equals(rd.ItemCode)).ToList().Count
                        {
                            foreach (ItemVM item in iList.Where(x => x.ItemCode.Equals(rd.ItemCode)))
                            {
                                iList.ToList().Find(x => x.ItemCode.Equals(rd.ItemCode)).TempQtyReq += rd.AwaitQty;
                            }
                        }
                    }
                }
                return iList;
            }
        }


        //AcceptDisbursement
        public static void AcceptDisbursement(int empId, List<ItemVM> iList)
        {
            //List<Item> itemsList = Utility.ItemUtility.Convert_ItemVM_To_Item(iList);
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

                        a.Reason = i.TempReason;
                        a.QtyChange = i.TempQtyAcpt - i.TempQtyReq;
                        a.Status = "Submitted";

                        entities.Adjustments.Add(a);
                        entities.SaveChanges();

                        double chgVal = a.QtyChange * i.Price1 ?? default(double);

                        if (chgVal >= 250)
                        {
                            //Notify Manager
                        }
                        else
                        {
                            //Notify Manager
                        }

                        //3. Distribute i.TempQtyAcpt across RequestDetails ******************UnClear Method******************



                        string deptcode = EmployeeBL.GetDeptCode(empId);

                        var EmpIds = entities.Employees.Where(e => e.DeptCode.Equals(deptcode)).ToList();


                        List<Request> rList = new List<Request>();
                        List<RequestDetail> rdList = new List<RequestDetail>();

                        foreach (var e in EmpIds)
                        {
                            List<Request> reqList = entities.Requests.Where(x => x.EmpId == e.EmpId && x.Status.Equals("Approved")).ToList();
                            rList.AddRange(reqList);
                        }

                        foreach (Request r in rList)
                        {
                            List<RequestDetail> reqdList = entities.RequestDetails.Where(x => x.ReqId == r.ReqId).ToList();
                            rdList.AddRange(reqdList);
                        }


                        int count = i.TempQtyAcpt;

                        foreach (Request r in rList)
                        {
                            int cntFulfilled = 0;
                            if (count > 0)
                            {
                                foreach (RequestDetail rd in rdList.Where(rd => rd.ReqId == r.ReqId))
                                {
                                    if (count > 0)
                                    {
                                        if (rd.AwaitQty > 0 && rd.AwaitQty <= count)
                                        {
                                            rd.FulfilledQty += rd.AwaitQty;
                                            count -= rd.AwaitQty;
                                            rd.AwaitQty = 0;

                                            //Save Changes for rd.AwaitQty, Rd.FulfilledQty
                                            UpdateAwait(r.ReqId, rd.ItemCode, rd.AwaitQty);
                                            UpdateFulfilled(r.ReqId, rd.ItemCode, rd.FulfilledQty);

                                            TransactionVM t = new TransactionVM();
                                            t.TranDateTime = DateTime.Now;
                                            t.ItemCode = i.ItemCode;
                                            t.QtyChange = rd.AwaitQty;
                                            t.UnitPrice = i.Price1 ?? default(double);
                                            t.Desc = "Disbursement";
                                            t.DeptCode = deptcode;

                                            TransactionBL.AddTran(t);
                                        }

                                        else if (rd.AwaitQty > 0 && rd.AwaitQty > count)
                                        {
                                            rd.FulfilledQty += count;
                                            rd.AwaitQty -= count;


                                            //Save Changes for rd.AwaitQty, Rd.FulfilledQty
                                            UpdateAwait(r.ReqId, rd.ItemCode, rd.AwaitQty);
                                            UpdateFulfilled(r.ReqId, rd.ItemCode, rd.FulfilledQty);

                                            TransactionVM t = new TransactionVM();
                                            t.TranDateTime = DateTime.Now;
                                            t.ItemCode = i.ItemCode;
                                            t.QtyChange = count;
                                            t.UnitPrice = i.Price1 ?? default(double);
                                            t.Desc = "Disbursement";
                                            t.DeptCode = deptcode;

                                            TransactionBL.AddTran(t);

                                            count = 0;
                                        }

                                    }
                                    cntFulfilled += (rd.ReqQty - rd.FulfilledQty);
                                }

                                //Check if Request Fulfilled
                                if (cntFulfilled == 0) r.Status = "Fulfilled";
                                //int openCount = 0;
                                //foreach (RequestDetail rd in rdList.Where(rd => rd.ReqId == r.ReqId))
                                //{
                                //    int shortQty = (rd.ReqQty - rd.FulfilledQty);
                                //    openCount += shortQty;
                                //}

                                //if (openCount == 0)
                                //{
                                //    r.Status = "Fulfilled";

                                //    RequestVM request = RequestBL.GetReq(r.ReqId);

                                //    request.Status = r.Status;
                                //    RequestBL.UpdateReq(request); //save changes for this request object
                                //}
                            }

                            //Check Low Stock item
                            bool status = CheckLowStk(i);

                            if (status)
                            {
                                Item item = Utility.ItemUtility.Convert_ItemVMObj_To_ItemObj(i);
                                NotificationBL.AddLowStkNotification(empId, item);
                            }

                            //send email acknowledgement to rep, specific head and all clerks
                            NotificationBL.AddAcptNotification(r.ReqId);
                        }
                    }


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
                                Price1 = i.Price1,
                                Price2 = i.Price2,
                                Price3 = i.Price3
                            }).ToList<ItemVM>();

                foreach (ItemVM item in itemlist)
                {
                    item.ReccReorderQty = Math.Max(item.ReorderQty, GetReccReorderQty(item.ItemCode));
                }
            }
            return itemlist;
        }

        //Get Recommand ReOrderQty
        public static int GetReccReorderQty(string iCode)
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

                List<RequestVM> rlist = BusinessLogic.RequestBL.GetReq("Approved");

                List<ItemVM> iList = GetAllItems();

                foreach (ItemVM item in iList)
                {
                    item.TempQtyReq = 0;
                }

                List<ItemVM> ritemlist = new List<ItemVM>();
                foreach (RequestVM r in rlist)
                {
                    List<RequestDetail> rdlist = entities.RequestDetails.Where(rd => rd.ReqId == r.ReqId).ToList();

                    foreach (RequestDetail rd in rdlist)
                    {
                        if (rd.ReqQty - rd.FulfilledQty > 0)
                        {
                            iList.ToList().Find(x => x.ItemCode.Equals(rd.ItemCode)).TempQtyReq += rd.ReqQty;

                            ritemlist = iList.Where(x => x.TempQtyReq > 0).ToList();

                        }
                    }
                }
                return ritemlist;
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
                               Price1 = i.Price1,
                               Price2 = i.Price2,
                               Price3 = i.Price3
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
        public static void FulfillRequest(List<ItemVM> items)
        {
            List<RequestDetailVM> fulfilledList = new List<RequestDetailVM>();

            foreach (ItemVM i in items)
            {
                int count = (i.TempQtyDisb > i.Balance) ? i.Balance : i.TempQtyDisb ?? default(int);

                foreach (RequestVM r in RequestBL.GetReq("Approved"))
                {
                    if (count > 0)
                    {
                        string deptCode = EmployeeBL.GetEmp(r.EmpId).DeptCode;

                        foreach (RequestDetailVM rd in RequestDetailBL.GetReqDetList(r.ReqId))
                        {
                            if (count > 0)
                            {
                                if (i.ItemCode.Equals(rd.ItemCode))
                                {
                                    int shortQty = (rd.ReqQty - rd.FulfilledQty);

                                    if (shortQty <= count)
                                    {
                                        count -= shortQty;
                                        i.Balance -= shortQty;
                                        rd.AwaitQty += shortQty;
                                        try
                                        {
                                            UpdateBal(rd.ItemCode, i.Balance);
                                            //update to Request Details table
                                            fulfilledList.Add(rd);
                                        }
                                        catch (Exception)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        i.Balance -= count;
                                        rd.AwaitQty += count;
                                        try
                                        {
                                            UpdateBal(rd.ItemCode, i.Balance);
                                            //update to Request Details table
                                            fulfilledList.Add(rd);
                                        }
                                        catch (Exception)
                                        {
                                            break;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }


            //Making PDF Reports
            //Group By Department then By Item
            List<RequestDetailVM> ListByDept = new List<RequestDetailVM>();

            foreach (string d in DepartmentBL.GetDeptCodes())
            {
                List<RequestDetailVM> rdList = new List<RequestDetailVM>();

                foreach (RequestDetailVM rd in fulfilledList)
                {
                    if (GetDeptCode(rd.ReqId).Equals(d))
                    {
                        if (rdList.Contains(rd))
                        {
                            rdList.Find(x => x.ItemCode.Equals(rd.ItemCode)).AwaitQty += rd.AwaitQty;
                        }
                        else
                        {
                            rdList.Add(rd);
                        }

                        ListByDept.AddRange(rdList);
                    }
                }
            }


        }


        //FulfillRequestUrgent
        public static void FulfillRequestUrgent(int empId, List<ItemVM> items, DateTime D1, int Collpt)
        {
            List<RequestDetailVM> fulfilledList = new List<RequestDetailVM>();

            foreach (ItemVM i in items)
            {
                int count = (i.TempQtyDisb > i.Balance) ? i.Balance : i.TempQtyDisb ?? default(int);

                foreach (RequestVM r in RequestBL.GetReq(empId, "Approved"))
                {
                    if (count > 0)
                    {
                        string deptCode = EmployeeBL.GetEmp(r.EmpId).DeptCode;

                        foreach (RequestDetailVM rd in RequestDetailBL.GetReqDetList(r.ReqId))
                        {
                            if (count > 0)
                            {
                                if (i.ItemCode.Equals(rd.ItemCode))
                                {
                                    int shortQty = (rd.ReqQty - rd.FulfilledQty);

                                    if (shortQty <= count)
                                    {
                                        shortQty = 0;
                                        count -= shortQty;
                                        i.Balance -= shortQty;
                                        rd.AwaitQty += shortQty;
                                        try
                                        {
                                            UpdateBal(rd.ItemCode, i.Balance);
                                            fulfilledList.Add(rd);
                                        }
                                        catch (Exception)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        shortQty -= count;
                                        count = 0;
                                        i.Balance -= count;
                                        rd.AwaitQty += count;
                                        try
                                        {
                                            UpdateBal(rd.ItemCode, i.Balance);
                                            fulfilledList.Add(rd);
                                        }
                                        catch (Exception)
                                        {
                                            break;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }


            //Making PDF Reports
            //Group By Department then By Item
            List<RequestDetailVM> ListByDept = new List<RequestDetailVM>();

            foreach (string d in DepartmentBL.GetDeptCodes())
            {
                List<RequestDetailVM> rdList = new List<RequestDetailVM>();

                foreach (RequestDetailVM rd in fulfilledList)
                {
                    if (GetDeptCode(rd.ReqId).Equals(d))
                    {
                        if (rdList.Contains(rd))
                        {
                            rdList.Find(x => x.ItemCode.Equals(rd.ItemCode)).AwaitQty += rd.AwaitQty;
                        }
                        else
                        {
                            rdList.Add(rd);
                        }

                        ListByDept.AddRange(rdList);
                    }
                }
            }


        }
        //Get DeptCode by request id
        public static string GetDeptCode(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                int EmpID = entities.Requests.Where(r => r.ReqId == reqId).Select(x => x.EmpId).First();

                string DeptCode = EmployeeBL.GetDeptCode(EmpID);

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
                Item item = entities.Items
                            .Where(x => x.ItemCode.Equals(i.ItemCode) && x.Balance < x.ReorderLevel)
                            .First();
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
                List<Request> rList = entities.Requests.Where(r => r.EmpId == empId && r.Status.Equals("Approved")).ToList();

                List<RequestDetail> rdList = new List<RequestDetail>();

                List<ItemVM> iList = new List<ItemVM>();

                foreach (Request r in rList)
                {
                    List<RequestDetail> reqdetailLists = entities.RequestDetails.Where(rd => rd.ReqId == r.ReqId).ToList();
                    rdList.AddRange(reqdetailLists);
                }

                foreach (RequestDetail rd in rdList)
                {
                    List<Item> itemlists = entities.Items.Where(i => i.ItemCode.Equals(rd.ItemCode)).ToList(); //need to refer to retrieve item method *************************************************************************

                    iList.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(itemlists));
                }

                return iList;

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
                                SuppCode1 = i.SuppCode1,
                                SuppCode2 = i.SuppCode2,
                                SuppCode3 = i.SuppCode3,
                                Price1 = i.Price1,
                                Price2 = i.Price2,
                                Price3 = i.Price3
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
                UpdateItem(i.ItemCode, i.ReccReorderLvl, i.ReccReorderQty, i.SuppCode1, i.Price1 ?? default(double), i.SuppCode2, i.Price2 ?? default(double), i.SuppCode3, i.Price3 ?? default(double));
            }
        }


        //GetItems by Cat, Desc and Threshold
        public static List<ItemVM> GetItems(string cat, string desc, double threshold)
        {
            List<ItemVM> itemlist = new List<ItemVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                if (cat != null && desc == null && threshold.Equals(null))
                {
                    List<Item> ilist1 = entities.Items.Where(i => i.Cat == cat).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist1));
                    return itemlist;
                }
                else if (desc != null && cat == null && threshold.Equals(null))
                {
                    List<Item> ilist2 = entities.Items.Where(i => i.Desc == desc).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist2));
                    return itemlist;
                }

                else if (cat != null && !threshold.Equals(null) && desc == null)
                {
                    List<Item> ilist1 = entities.Items.Where(i => i.Cat == cat).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist1));


                    foreach (ItemVM item in itemlist)
                    {
                        item.lvlDiff = (item.ReccReorderLvl - item.ReorderLevel) / item.ReorderLevel;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }

                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (desc != null && !threshold.Equals(null) && cat == null)
                {
                    List<Item> ilist2 = entities.Items.Where(i => i.Desc == desc).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist2));


                    foreach (ItemVM item in itemlist)
                    {
                        item.lvlDiff = (item.ReccReorderLvl - item.ReorderLevel) / item.ReorderLevel;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }

                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
                    return itemlist;
                }
                else if (cat != null && desc != null && threshold.Equals(null))
                {
                    List<Item> ilist = entities.Items.Where(i => i.Cat == cat && i.Desc.Contains(desc)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist));
                    return itemlist;
                }
                else if (cat != null && desc != null && !threshold.Equals(null))
                {
                    List<Item> ilist = entities.Items.Where(i => i.Cat == cat && i.Desc.Contains(desc)).ToList();
                    itemlist.AddRange(Utility.ItemUtility.Convert_Item_To_ItemVM(ilist));

                    foreach (ItemVM item in itemlist)
                    {
                        item.lvlDiff = (item.ReccReorderLvl - item.ReorderLevel) / item.ReorderLevel;
                        item.qtyDiff = (item.ReccReorderQty - item.ReorderQty) / item.ReorderQty;
                    }

                    itemlist = itemlist.Where(i => i.lvlDiff >= threshold || i.qtyDiff >= threshold).ToList();
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

