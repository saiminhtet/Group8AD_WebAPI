using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Group8AD_WebAPI.Controllers
{
    public class ReportController : ApiController
    {
        //Inventory Status Report
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Report/InventoryList/{empId}")]
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


        //Inventory Status Report
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Report/LowStockItemList/{empId}")]
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
    }
}
