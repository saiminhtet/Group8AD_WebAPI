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
        [Route("api/Report/GenerateInventoryItemList")]
        public HttpResponseMessage GenerateInventoryItemList(int empId)
        {
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
    }
}
