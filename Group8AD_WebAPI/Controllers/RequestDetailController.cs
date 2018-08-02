using Group8AD_WebAPI.BusinessLogic;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/* 
* Class Name       :       RequestDetailController
* Created by       :       Noel Noel Han
* Created date     :       13/Jul/2018
* Student No.      :       A0180529B
*/

namespace Group8AD_WebAPI.Controllers
{
    [Authorize]
    public class RequestDetailController : ApiController
    {
        //AddReqDet(int empId, RequestDetail reqDet , string status) 
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/addReqDet")]
        public HttpResponseMessage AddReqDet(int empId, string itemCode, int reqQty, string status)
        {
            RequestDetailVM req = BusinessLogic.RequestDetailBL.AddReqDet(empId, itemCode, reqQty, status);
            if (req == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, req);

        }
        //AddReqDet(int empId, RequestDetail reqDet , string status)
        //dummy
        //[AcceptVerbs("POST")]
        //[HttpPost]
        //[Route("api/RequestDetail/addReqDet")]
        //public HttpResponseMessage AddReqDet(int empId, string itemCode, int reqQty, string status)
        //{
        //    RequestDetailVM req = BusinessLogic.RequestDetailBL.AddReqDet(empId, itemCode,reqQty,status);
        //    if (req == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, req);
        //}

        //AddReqDet(int reqId, RequestDetail reqDet)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/addReqDet")]
        public HttpResponseMessage AddReqDet_reqId(int reqId, RequestDetailVM reqDetVM)
        {
            RequestDetailVM req = BusinessLogic.RequestDetailBL.AddReqDet(reqId, reqDetVM);
            if (req == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, req);

        }

        //UpdateReqDet(int reqId, RequestDetailVM reqDet)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/update/{reqId}")]
        public HttpResponseMessage UpdateReqDet(string reqId, RequestDetailVM reqDetVM)
        {
            int RepId = Convert.ToInt16(reqId);
            RequestDetailVM req = BusinessLogic.RequestDetailBL.UpdateReqDet(RepId, reqDetVM);
            if (req == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, req);

        }

        //removeReqDet(int empId, string itemCode, string status)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/removeReqDet")]
        public HttpResponseMessage removeReqDet(int empId, string itemCode, string status)
        {
            try
            {
                RequestDetailBL.removeReqDet(empId,itemCode,status);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        //removeReqDet(int reqId, string itemCode)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/removeReqDet")]
        public HttpResponseMessage removeReqDet(string reqId, string itemCode)
        {
            int ReqId = Convert.ToInt16(reqId);
            try
            {
                BusinessLogic.RequestDetailBL.removeReqDet(ReqId, itemCode);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }

        //removeAllReqDet(int reqId)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/removeAll")]
        public HttpResponseMessage removeAllReqDet(string reqId)
        {
            int ReqId = Convert.ToInt16(reqId);
            try
            {
                BusinessLogic.RequestDetailBL.removeAllReqDet(ReqId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }

        //List<RequestDetailVM> GetReqDetList(int reqId)
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/RequestDetail/GetReqDetList/{reqId}")]
        public HttpResponseMessage GetReqDetList(string reqId)
        {
            int ReqId = Convert.ToInt16(reqId);
            List<RequestDetailVM> reqDetail = BusinessLogic.RequestDetailBL.GetReqDetList(ReqId);

            if (reqDetail == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, reqDetail);
        }

        //UpdateAwait(int reqId, int awaitQty)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/updateAwait")]
        public HttpResponseMessage UpdateAwait(string reqId, string awaitQty)
        {
            int ReqId = Convert.ToInt16(reqId);
            int AwaitQty = Convert.ToInt16(awaitQty);
            try
            {
                BusinessLogic.RequestDetailBL.UpdateAwait(ReqId, AwaitQty);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }

        //string GetDeptCode(RequestDetail reqDet)
        //
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/RequestDetail/GetDeptCode")]
        public HttpResponseMessage GetDeptCode(RequestDetail reqDet)
        {
          
            string DeptCode = BusinessLogic.RequestDetailBL.GetDeptCode(reqDet);

            if (DeptCode == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, DeptCode);
        }

        //UpdateFulfilled(int reqId, int fulfilledQty)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/updateFulfilled/{reqId}")]
        public HttpResponseMessage UpdateFulfilled(string reqId, string fulfilledQty)
        {
            int ReqId = Convert.ToInt16(reqId);
            int FulfilledQty = Convert.ToInt16(fulfilledQty);
            try
            {
                BusinessLogic.RequestDetailBL.UpdateFulfilled(ReqId, FulfilledQty);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }
    }
}
