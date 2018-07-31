using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Group8AD_WebAPI.Controllers
{
    [Authorize]
    public class ReportController : ApiController
    {
        //Inventory Status Report
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Report/InventoryList/")]
        public HttpResponseMessage GenerateInventoryItemList(int empId)
        {
            //int emp_id = Convert.ToInt16(empId);
            try
            {
                BusinessLogic.PdfBL.GenerateInventoryItemList(empId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        //Low Stock Report
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Report/LowStockItemList/")]
        public HttpResponseMessage GenerateLowStockItemList(int empId)
        {
            //int emp_id = Convert.ToInt16(empId);
            try
            {
                BusinessLogic.PdfBL.GenerateLowStockItemList(empId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        //Purchase Order Report
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Report/PurchaseOrder/")]
        public HttpResponseMessage GeneratePurchaseOrderItemList(int empId, DateTime expected_date, List<ItemVM> PoiList)
        {
            //int emp_id = Convert.ToInt16(empId);
            try
            {
                BusinessLogic.PdfBL.GeneratePurchaseOrderList(empId,expected_date, PoiList);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
