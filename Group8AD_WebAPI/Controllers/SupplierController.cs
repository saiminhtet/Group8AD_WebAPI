using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

/* 
* Class Name       :       SupplierController
* Created by       :       Noel Noel Han
* Created date     :       13/Jul/2018
* Student No.      :       A0180529B
*/

namespace Group8AD_WebAPI.Controllers
{
    [Authorize]
    public class SupplierController : ApiController
    {
        //get AllSupplier list by itemCode
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Supplier/{itemCode}")]
        public HttpResponseMessage GetAllSupp(string itemCode)
        {
            List<SupplierVM> supplier = BusinessLogic.SupplierBL.GetAllSupp(itemCode);

            if (supplier == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, supplier);
        }

        //get AllSupplier list 
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/Supplier")]
        public HttpResponseMessage GetAllSupp()
        {
            List<SupplierVM> supplier = BusinessLogic.SupplierBL.GetAllSupp();

            if (supplier == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, supplier);
        }
    }
}