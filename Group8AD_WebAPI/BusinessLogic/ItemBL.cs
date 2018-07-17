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
                itemlist = entities.Items.Where(i => i.Cat == cat && i.Desc == desc).Select(i => new ItemVM()
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
    }


   
}

