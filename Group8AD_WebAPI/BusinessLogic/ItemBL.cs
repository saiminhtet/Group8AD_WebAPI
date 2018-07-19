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
                if (cat == null || desc == null)
                {
                    if (cat == null)
                    {
                        itemlist = entities.Items.Where(i => i.Desc == desc).Select(i => new ItemVM()
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
                        return itemlist;
                    }
                    if (desc == null)
                    {
                        itemlist = entities.Items.Where(i => i.Cat == cat).Select(i => new ItemVM()
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
                        return itemlist;
                    }
                }
                itemlist = entities.Items.Where(i => i.Cat == cat && i.Desc.Contains(desc)).Select(i => new ItemVM()
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
                return itemlist;
            }
        }

        //get Frequent Item list by Employee ID
        public static List<ItemVM> GetFrequentList(int empId)
        {
            //List<ItemVM> itemlist = new List<ItemVM>();
            //ArrayList itemcodes = new ArrayList();
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    // var requestIds = entities.Requests.Where(r => r.EmpId == empId).Select(r => r.ReqId).ToList();
            //    var requestIds = (from a in entities.Requests.Where(a => a.EmpId == empId)
            //                      select new
            //                      {
            //                          RequestId = a.ReqId
            //                      }).ToList();

            //var requestItems = (from b in entities.RequestDetails
            //                   join c in requestIds on b.ReqId equals c.RequestId
            //                   select new ItemVM
            //                   {
            //                     ItemCode = b.ItemCode
            //                   }).ToList();


            //foreach (var id in requestIds)
            //{
            //    itemlist = entities.RequestDetails.Where(a => a.ReqId == id.RequestId)
            //   .Select(i => new ItemVM()
            //   {
            //       ItemCode = i.ItemCode
            //   }).ToList<ItemVM>();

            //    foreach (var item in itemlist)
            //    {
            //        itemcodes.Add(item.ItemCode);
            //    }
            //}



            //itemlist = (from i in entities.Items
            //            join r in itemlist on i.ItemCode equals r.ItemCode
            //            select new ItemVM
            //            {
            //                ItemCode = i.ItemCode,
            //                Cat = i.Cat,
            //                Desc = i.Desc,
            //                Location = i.Location,
            //                UOM = i.UOM,
            //                IsActive = i.IsActive,
            //                Balance = i.Balance,
            //                ReorderLevel = i.ReorderLevel,
            //                ReorderQty = i.ReorderQty,
            //                TempQtyDisb = i.TempQtyDisb,
            //                TempQtyCheck = i.TempQtyCheck,
            //                SuppCode1 = i.SuppCode1,
            //                SuppCode2 = i.SuppCode2,
            //                SuppCode3 = i.SuppCode3,
            //                Price1 = i.Price1,
            //                Price2 = i.Price2,
            //                Price3 = i.Price3
            //            }).ToList<ItemVM>();

            //for (int i = 0; i < itemcodes.Count; i++)
            //{
            //    itemlist = en
            //}

            List<ItemVM> itemlist = new List<ItemVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                itemlist = entities.Items.Take(5)
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
            }
            return itemlist;
        }

        //get low stock Items
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



        //Get Dept DisbList by Employee ID
        public static List<ItemVM> GetDeptDisbList(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptcode = EmployeeBL.GetDeptCode(empId);
                int request_empid = Convert.ToInt16(entities.Requests.Where(r => r.EmpId == empId).Select(r => r.EmpId).First().ToString());
                string request_deptcode = EmployeeBL.GetDeptCode(request_empid);


                //get the request list by emloyee id
                List<Request> rlist = new List<Request>();

                rlist = entities.Requests.Where(r => r.EmpId == empId && request_deptcode.Equals(deptcode) && r.Status.Equals("Approved")).ToList();


                //get request details by request id
                List<RequestDetailVM> rdvlist = new List<RequestDetailVM>();
                foreach (Request r in rlist)
                {
                    List<RequestDetail> rdlist = entities.RequestDetails.Where(rd => rd.ReqId == r.ReqId).ToList();

                    foreach (RequestDetail rd in rdlist)
                    {
                        RequestDetailVM rdVm = new RequestDetailVM();
                        rdVm.ReqId = rd.ReqId;
                        rdVm.ReqLineNo = rd.ReqLineNo;
                        rdVm.ItemCode = rd.ItemCode;
                        rdVm.ReqQty = rd.ReqQty;
                        rdVm.AwaitQty = rd.AwaitQty;
                        rdVm.FulfilledQty = rd.FulfilledQty;

                        rdvlist.Add(rdVm);
                    }
                }


                //populating iList for display
                List<ItemVM> iList = new List<ItemVM>();

                foreach (RequestDetailVM rdv in rdvlist)
                {
                    if (rdv.AwaitQty > 0 && iList.Where(x => x.ItemCode == rdv.ItemCode).ToList().Count > 0)
                    {

                        ItemVM item = iList.Where(x => x.ItemCode == rdv.ItemCode).Select(x => new ItemVM()
                        {
                            ItemCode = x.ItemCode,
                            Cat = x.Cat,
                            Desc = x.Desc,
                            UOM = x.UOM,
                            Price1 = x.Price1,
                            TempQtyAcpt = x.TempQtyAcpt,
                            TempQtyReq = x.TempQtyReq
                        }).First<ItemVM>();
                        item.TempQtyReq += rdv.AwaitQty;

                        iList.Add(item);
                    }
                    else if (rdv.AwaitQty > 0 && iList.Where(x => x.ItemCode == rdv.ItemCode).ToList().Count == 0)
                    {
                        ItemVM i = new ItemVM();
                        i.ItemCode = rdv.ItemCode;

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

                return iList;
            }
        }


        //Get Retrieve Items
        public static List<ItemVM> GetRetrieveItems()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string status = "Approved";
                List<RequestVM> rlist = BusinessLogic.RequestBL.GetReq(status);

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

                foreach (Item item in updateditemlist)
                {
                    ItemVM itemVM = new ItemVM();
                    itemVM.ItemCode = item.ItemCode;
                    itemVM.Cat = item.Cat;

                    itemVM.Desc = item.Desc;
                    itemVM.Location = item.Location;
                    itemVM.UOM = item.UOM;
                    itemVM.IsActive = item.IsActive;
                    itemVM.Balance = item.Balance;
                    itemVM.ReorderLevel = item.ReorderLevel;
                    itemVM.ReorderQty = item.ReorderQty;
                    itemVM.TempQtyDisb = item.TempQtyDisb;
                    itemVM.TempQtyCheck = item.TempQtyCheck;
                    itemVM.SuppCode1 = item.SuppCode1;
                    itemVM.SuppCode2 = item.SuppCode2;
                    itemVM.SuppCode3 = item.SuppCode3;
                    itemVM.Price1 = item.Price1;
                    itemVM.Price2 = item.Price2;
                    itemVM.Price3 = item.Price3;

                    itemlistvm.Add(itemVM);
                }


                return itemlistvm;
            }
        }

        //GetQtyDisb
        public static List<ItemVM> GetQtyDisb()
        {
            List<ItemVM> itemlist = new List<ItemVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Item> items = entities.Items.ToList<Item>();

                foreach (Item item in items)
                {
                    ItemVM itemVM = new ItemVM();
                    itemVM.ItemCode = item.ItemCode;
                    itemVM.Cat = item.Cat;
                    itemVM.Desc = item.Desc;
                    itemVM.Location = item.Location;
                    itemVM.UOM = item.UOM;
                    itemVM.IsActive = item.IsActive;
                    itemVM.Balance = item.Balance;
                    itemVM.ReorderLevel = item.ReorderLevel;
                    itemVM.ReorderQty = item.ReorderQty;
                    itemVM.TempQtyDisb = item.TempQtyDisb;
                    itemVM.TempQtyCheck = item.TempQtyCheck;
                    itemVM.SuppCode1 = item.SuppCode1;
                    itemVM.SuppCode2 = item.SuppCode2;
                    itemVM.SuppCode3 = item.SuppCode3;
                    itemVM.Price1 = item.Price1;
                    itemVM.Price2 = item.Price2;
                    itemVM.Price3 = item.Price3;

                    itemlist.Add(itemVM);
                }

                return itemlist;
            }
        }


        //FulfillRequest
        public static void FulfillRequest(List<ItemVM> items)
        {
            List<RequestDetailVM> fulfilledList = new List<RequestDetailVM>();

            List<RequestVM> requestlist = RequestBL.GetReq("Approved");

            List<RequestDetailVM> requestdetails_list = new List<RequestDetailVM>();

            foreach (RequestVM r in requestlist)
            {
                requestdetails_list.AddRange(RequestDetailBL.GetReqDetList(r.ReqId));
            }

            
            foreach (ItemVM i in items)
            {
                int count = (i.TempQtyDisb > i.Balance) ? i.Balance : i.TempQtyDisb ?? default(int);

                foreach (RequestVM r in requestlist)
                {
                    if (count > 0)
                    {
                        string deptCode = EmployeeBL.GetEmp(r.EmpId).DeptCode;

                        foreach (RequestDetailVM rd in requestdetails_list)
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

                                        //need to call UpdateBal Method

                                        RequestDetailBL.UpdateAwait(r.ReqId, rd.AwaitQty);

                                        fulfilledList.Add(rd);
                                        break;
                                    }
                                    else
                                    {
                                        shortQty -= count;
                                        count = 0;
                                        i.Balance -= count;
                                        rd.AwaitQty += count;

                                        //need to call UpdateBal Method

                                        RequestDetailBL.UpdateAwait(r.ReqId, rd.AwaitQty);

                                        fulfilledList.Add(rd);
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
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
                    if (EmployeeBL.GetDeptCode(rd.EmpId).Equals(d))
                    {
                        if (rdList.Contains(rd))
                        {
                            rdList.Find(x => x.ItemCode.Equals(rd.ItemCode)).AwaitQty += rd.AwaitQty;
                        }
                        else
                        {
                            rdList.Add(rd);
                        }
                    }
                    ListByDept.AddRange(rdList);
                }

            }

           
        }
    }
}

