using Group8AD_WebAPI.BusinessLogic;
using Group8AD_WebAPI.Models;
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
    //Controllers
    public class AdjustmentController : ApiController
    {

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/add")]
        public HttpResponseMessage AddAdjustment(List<AdjustmentVM> adj)
        {
            List<AdjustmentVM> adjustment = AdjustmentBL.AddAdj(adj);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/get")]
        public HttpResponseMessage GetAdjustment(string voucherNo)
        {
            List<AdjustmentVM> adjustment = AdjustmentBL.GetAdjListByVoucherNo(voucherNo);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/get")]
        public HttpResponseMessage GetAdjustment(string voucherNo, int approverId)
        {
            List<AdjustmentVM> adjustment = AdjustmentBL.GetAdjList(voucherNo, approverId);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/get")]
        public HttpResponseMessage GetAdjustment(string voucherNo, string status)
        {
            List<AdjustmentVM> adjustment = AdjustmentBL.GetAdjList(voucherNo, status);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/get")]
        public HttpResponseMessage GetAdjustmentList(string status)
        {
            List<AdjustmentVM> adjlist = AdjustmentBL.GetAdjListByStatus(status);
            if (adjlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjlist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/get")]
        public HttpResponseMessage GetAdjustmentList(int approverId)
        {
            List<AdjustmentVM> adjlist = AdjustmentBL.GetAdjListByApproverId(approverId);
            if (adjlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjlist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/raise")]
        public HttpResponseMessage RaiseAdjustment(int empId, List<ItemVM> iList)
        {
            if (!AdjustmentBL.RaiseAdjustments(empId, iList))
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/accept")]
        public HttpResponseMessage AcceptRequest(string voucherNo, int empId, string cmt)
        {
            bool isAccepted = AdjustmentBL.AcceptRequest(voucherNo, empId, cmt);
            if (isAccepted == false)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Adjustment/reject")]
        public HttpResponseMessage RejectRequest(string voucherNo, int empId, string cmt)
        {
            bool isRejected = AdjustmentBL.RejectRequest(voucherNo, empId, cmt);
            if (isRejected == false)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}