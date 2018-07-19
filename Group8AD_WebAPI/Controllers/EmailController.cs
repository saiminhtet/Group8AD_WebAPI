using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Group8AD_WebAPI.Controllers
{
    public class EmailController : ApiController
    {
        //SendNewReqEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendNewReqEmail")]
        public HttpResponseMessage SendNewReqEmail(int empId, RequestVM currReq)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendReqApprEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendReqApprEmail")]
        public HttpResponseMessage SendReqApprEmail(int empId, RequestVM currReq)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendDisbEmailForClerk
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendDisbEmailForClerk")]
        public HttpResponseMessage SendDisbEmailForClerk(int empId, List<RequestDetailVM> ListByDept, List<RequestDetailVM> ListByReq)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendDisbEmailForReq
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendDisbEmailForReq")]
        public HttpResponseMessage SendDisbEmailForReq(int empId, string deptCode, List<RequestDetailVM> ListByDept, List<RequestDetailVM> ListByReq)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendRcvEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendRcvEmail")]
        public HttpResponseMessage SendRcvEmail(int empId)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendLowStockEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendLowStockEmail")]
        public HttpResponseMessage SendLowStockEmail(int empId, List<ItemVM> items)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendPOEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendPOEmail")]
        public HttpResponseMessage SendPOEmail(int empId, DateTime targetDate, List<ItemVM> item)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //SendAdjReqEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendPOEmail")]
        public HttpResponseMessage SendAdjReqEmail(int empId, List<AdjustmentVM> adjList)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendAdjApprEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendAdjApprEmail")]
        public HttpResponseMessage SendAdjApprEmail(int empId, AdjustmentVM adj)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendReportEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendReportEmail")]
        public HttpResponseMessage SendReportEmail(int empId, List<ItemVM> reportItemList)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //SendInvListEmail
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        [Route("api/Email/SendInvListEmail")]
        public HttpResponseMessage SendInvListEmail(int empId, List<ItemVM> items)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
