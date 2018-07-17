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
            ItemVM item= BusinessLogic.ItemBL.GetItem(itemcode);

            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }


        //get low stock Item list
        [System.Web.Http.AcceptVerbs("Post")]
        [System.Web.Http.HttpPost]
        [Route("api/Item/GetItems/")]
        public HttpResponseMessage GetItems(string cat, string desc)
        {
            List<ItemVM> itemlists = BusinessLogic.ItemBL.GetItems(cat,desc);

            if (itemlists == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, itemlists);
        }





    }

    
}