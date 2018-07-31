using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.BusinessLogic;
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
        [Route("api/Item/GetFrequentList/{empId}")]
        public HttpResponseMessage GetFrequentList(int empId)
        {
            List<ItemVM> freq_itemlist = BusinessLogic.ItemBL.GetFrequentList(empId);


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

            List<ItemVM> itemlists = BusinessLogic.ItemBL.GetItems(cat, desc, threshold);

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
            try
            {
                BusinessLogic.ItemBL.AcceptDisbursement(empId, iList);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        //void AcceptDisbursement
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/AcceptDisbursement/")]
        public HttpResponseMessage AcceptDisbursement(int empId, int rcvEmpId, List<ItemVM> iList)
        {
            try
            {
                BusinessLogic.ItemBL.AcceptDisbursement(empId, rcvEmpId, iList);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
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

        //SaveQtyDisb
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/SaveQtyDisb")]
        public HttpResponseMessage SaveQtyDisb(string itemCode, int qtyDisb)
        {
            try
            {
                BusinessLogic.ItemBL.SaveQtyDisb(itemCode, qtyDisb);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //FulfillRequest
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/FulfillRequest")]
        public HttpResponseMessage FulfillRequest(List<ItemVM> item)
        {
            List<ItemVM> list = ItemBL.FulfillRequest(item);
            if (list == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        //UpdateBal
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/UpdateBal")]
        public HttpResponseMessage UpdateBal(string iCode, int bal)
        {

            try
            {
                BusinessLogic.ItemBL.UpdateBal(iCode, bal);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        //CheckLowStock
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/CheckLowStock")]
        public HttpResponseMessage CheckLowStk(ItemVM i)
        {
            try
            {
                BusinessLogic.ItemBL.CheckLowStk(i);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //ReceiveItem
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/ReceiveItem")]
        public HttpResponseMessage ReceiveItem(string suppCode, int qty, string ItemCode)
        {

            try
            {
                BusinessLogic.ItemBL.ReceiveItem(suppCode, qty, ItemCode);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //GetEmpItems
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetEmpItems/{empid}")]
        public HttpResponseMessage GetEmpItems(int empid)
        {

            List<ItemVM> empitemlists = BusinessLogic.ItemBL.GetEmpItems(empid);

            if (empitemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            return Request.CreateResponse(HttpStatusCode.OK, empitemlists);

        }


        // ResetQtyChk
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/ResetQtyChk")]
        public HttpResponseMessage ResetQtyChk()
        {
            List<ItemVM> itemlists = BusinessLogic.ItemBL.ResetQtyChk();

            if (itemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
        }



        // GetQtyChk
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetQtyChk")]
        public HttpResponseMessage GetQtyChk()
        {
            List<ItemVM> itemlists = BusinessLogic.ItemBL.GetQtyChk();

            if (itemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
        }

        //SaveQtyChk
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/SaveQtyChk")]
        public HttpResponseMessage SaveQtyChk(string ItemCode, int qtyChk)
        {

            try
            {
                BusinessLogic.ItemBL.SaveQtyChk(ItemCode, qtyChk);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        //UpdateItem
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/UpdateItem")]
        public HttpResponseMessage UpdateItem(string itemCode, int reorderLvl, int reorderQty, string s1, double p1, string s2, double p2, string s3, double p3)
        {
            try
            {
                BusinessLogic.ItemBL.UpdateItem(itemCode, reorderLvl, reorderQty, s1, p1, s2, p2, s3, p3);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        //UpdateItems
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/UpdateItems")]
        public HttpResponseMessage UpdateItemList(List<ItemVM> iList)
        {
            try
            {
                BusinessLogic.ItemBL.UpdateItemLists(iList);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //FulfillRequestUrgent
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/FulfillRequestUrgent")]
        public HttpResponseMessage FulfillRequestUrgent(int empId, List<ItemVM> items)
        {
            try
            {
                BusinessLogic.ItemBL.FulfillRequestUrgent(empId, items);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
        


        //For Testing 
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/Test")]
        public HttpResponseMessage Test()
        {
            List<DisbursementDetailVM> disbursementListDept = new List<DisbursementDetailVM>();
            List<ItemVM> itemlist = BusinessLogic.ItemBL.GetAllItems();
            string filename = "disbursementListDept" + DateTime.Now.ToString("yyyMMddHHmmss") + ".pdf";
            string filename2 = "disbursementListDeptByOrder" + DateTime.Now.ToString("yyyMMddHHmmss") + ".pdf";
            PdfBL.GenerateDisbursementListbyDept(disbursementListDept,filename);
            PdfBL.GenerateDisbursementListby_Dept_Employee_OrderNo(disbursementListDept, filename2);
            int empId = 101;
            DateTime expt_date = System.DateTime.Now;
            List<ItemVM> iList = new List<ItemVM>();

            PdfBL.GenerateLowStockItemList(empId);
            PdfBL.GenerateInventoryItemList(empId);
            PdfBL.GeneratePurchaseOrderList(empId, expt_date, iList);
            return Request.CreateResponse(HttpStatusCode.OK);
            //try
            //{
            //BusinessLogic.ItemBL.GetDeptDisbList(empId);
            //}
            //catch (Exception)
            //{

            //    return Request.CreateResponse(HttpStatusCode.BadGateway);
            //}
            //return Request.CreateResponse(HttpStatusCode.OK);
        }
    }


}