using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.Controllers
{
    public class ItemController : ApiController
    {


        //get All Item list
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/")]
        public HttpResponseMessage GetAllitems()
        {
            List<ItemVM> itemlist = BusinessLogic.ItemBL.GetAllItems();


            if (itemlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlist);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetFrequentList/{id}")]
        public HttpResponseMessage GetEmployeebyID(int id)
        {
            List<ItemVM> freq_itemlist = BusinessLogic.ItemBL.GetFrequentList(id);


            if (freq_itemlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, freq_itemlist);
        }


        //get All Category list
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetCategory/")]
        public HttpResponseMessage GetCatList()
        {
            List<string> categorylist = BusinessLogic.ItemBL.GetCatList();


            if (categorylist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, categorylist);
        }

        //get low stock Item list
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetLowStock/")]
        public HttpResponseMessage GetLowStockitems()
        {
            List<ItemVM> lowstock_itemlist = BusinessLogic.ItemBL.GetLowStockItems();


            if (lowstock_itemlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, lowstock_itemlist);
        }



        //get Item by Item Code
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetItem/{itemcode}")]
        public HttpResponseMessage GetItem(string itemcode)
        {
            ItemVM item = BusinessLogic.ItemBL.GetItem(itemcode);

            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }


        //Search Item by Cat or Desc
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetItems/")]
        public HttpResponseMessage GetItems(string cat, string desc)
        {

            List<ItemVM> itemlists = BusinessLogic.ItemBL.GetItems(cat, desc);

            if (itemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
                //return BadRequest("Not a valid model");
            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
            //return Ok(itemlists);
        }

        //Search Item by Cat or Desc or threshold
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetItems/")]
        public HttpResponseMessage GetItems(string cat, string desc, double threshold)
        {

            List<ItemVM> itemlists = BusinessLogic.ItemBL.GetItems(cat, desc);

            if (itemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
                //return BadRequest("Not a valid model");
            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
            //return Ok(itemlists);
        }


        //get department Disb Item by EmpId
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetDeptDisbList/{empId}")]
        public HttpResponseMessage GetDeptDisbList(int empId)
        {
            List<ItemVM> deptdislist = BusinessLogic.ItemBL.GetDeptDisbList(empId);

            if (deptdislist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, deptdislist);
        }

        //void AcceptDisbursement
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/AcceptDisbursement/")]
        public HttpResponseMessage AcceptDisbursement(int empId, List<ItemVM> iList)
        {          
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //void AcceptDisbursement
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/AcceptDisbursement/")]
        public HttpResponseMessage AcceptDisbursement(int empId, int rcvEmpId, List<ItemVM> iList)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //get Retrieve Items
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetRetrieveItems")]
        public HttpResponseMessage GetRetrieveItems()
        {
            List<ItemVM> retrieveitemlists = BusinessLogic.ItemBL.GetRetrieveItems();

            if (retrieveitemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, retrieveitemlists);
        }


        // ResetQtyDisb
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/ResetQtyDisb")]
        public HttpResponseMessage ResetQtyDisb()
        {
            List<ItemVM> retrieveitemlists = BusinessLogic.ItemBL.ResetQtyDisb();

            if (retrieveitemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, retrieveitemlists);
        }


        //GetQtyDisb
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetQtyDisb")]
        public HttpResponseMessage GetQtyDisb()
        {
            List<ItemVM> getqtydisb_list = BusinessLogic.ItemBL.GetQtyDisb();

            if (getqtydisb_list == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, getqtydisb_list);
        }


        //FulfillRequest
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/FulfillRequest")]
        public HttpResponseMessage FulfillRequest(List<ItemVM> item)
        {
            //try
            //{
            //    BusinessLogic.ItemBL.FulfillRequest(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            //}
            //catch (Exception e)
            //{

            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            //}
        }

        //UpdateBal
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/UpdateBal")]
        public HttpResponseMessage UpdateBal(string iCode, int bal)
        {
          
            return Request.CreateResponse(HttpStatusCode.OK);            
        }


        //CheckLowStock
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/CheckLowStock")]
        public HttpResponseMessage CheckLowStk(Item i)
        {

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //GetEmpItems
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetEmpItems/{empid}")]
        public HttpResponseMessage GetEmpItems(int empid)
        {
            SA46Team08ADProjectContext ctx = new SA46Team08ADProjectContext();
            List<Item> ilist = ctx.Items.Take(10).ToList<Item>();

            List<ItemVM> itemlists = new List<ItemVM>();

            foreach (Item item in ilist)
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

                itemlists.Add(itemVM);
            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
        }


        // ResetQtyChk
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/ResetQtyChk")]
        public HttpResponseMessage ResetQtyChk()
        {
            SA46Team08ADProjectContext ctx = new SA46Team08ADProjectContext();
            List<Item> ilist = ctx.Items.Take(10).ToList<Item>();

            List<ItemVM> itemlists = new List<ItemVM>();

            foreach (Item item in ilist)
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

                itemlists.Add(itemVM);
            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
        }



        // GetQtyChk
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetQtyChk")]
        public HttpResponseMessage GetQtyChk()
        {
            SA46Team08ADProjectContext ctx = new SA46Team08ADProjectContext();
            List<Item> ilist = ctx.Items.Take(10).ToList<Item>();

            List<ItemVM> itemlists = new List<ItemVM>();

            foreach (Item item in ilist)
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

                itemlists.Add(itemVM);
            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
        }

        //SaveQtyChk
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/SaveQtyChk")]
        public HttpResponseMessage SaveQtyChk(int qtyChk)
        {

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //UpdateItem
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/UpdateItem")]
        public HttpResponseMessage UpdateItem(int reorderLvl, int reorderQty, string s1, string s2, double p2, string s3, double p3)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //FulfillRequestUrgent
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/UpdateItem")]
        public HttpResponseMessage FulfillRequestUrgent(int empId, List<ItemVM> items)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }


}