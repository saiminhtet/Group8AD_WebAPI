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
        [System.Web.Http.AcceptVerbs("Post")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/SearchItem/")]
        public HttpResponseMessage SearchItems(ItemSearchVM itemSearch)
        {

            List<ItemVM> itemlists = BusinessLogic.ItemBL.GetItems(itemSearch.Cat, itemSearch.Desc);

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



    }


}