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
    public class RequestDetailController : ApiController
    {
        //AddReqDet(int empId, RequestDetail reqDet , string status)     
        //ok
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/addReqDet")]
        public HttpResponseMessage AddReqDet(int empId, RequestDetailVM reqDetVM, string status)
        {
            RequestDetailVM req = BusinessLogic.RequestDetailBL.AddReqDet(empId, reqDetVM, status);
            if (req == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, req);

        }

        //AddReqDet(int reqId, RequestDetail reqDet)
        //ok
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/addReqDet/{reqId}")]
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
        [Route("api/RequestDetail/update")]
        public HttpResponseMessage UpdateReqDet(RequestDetailVM reqDetVM)
        {
            try
            {
                RequestDetailVM req = BusinessLogic.RequestDetailBL.AddReqDet(reqDetVM.ReqId, reqDetVM);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //removeReqDet(int empId, string itemCode, string status)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/removeReqDet_empId/{empId}")]
        public HttpResponseMessage removeReqDet(RequestDetailVM reqDetvm)
        {
            try
            {
                BusinessLogic.RequestDetailBL.removeReqDet(reqDetvm.EmpId, reqDetvm.ItemCode, reqDetvm.Status);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }

        //removeReqDet(int reqId, string itemCode)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/removeReqDet/{reqId}")]
        public HttpResponseMessage removeReqDet_reqId(RequestDetailVM reqDetvm)
        {
            try
            {
                BusinessLogic.RequestDetailBL.removeReqDet(reqDetvm.ReqId, reqDetvm.ItemCode);
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
        [Route("api/RequestDetail/removeAll/{reqId}")]
        public HttpResponseMessage removeAllReqDet(int reqId)
        {
            try
            {
                BusinessLogic.RequestDetailBL.removeAllReqDet(reqId);
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
        public HttpResponseMessage GetReqDetList(int reqId)
        {
            List<RequestDetailVM> reqDetail = BusinessLogic.RequestDetailBL.GetReqDetList(reqId);

            if (reqDetail == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, reqDetail);
        }

        //UpdateAwait(int reqId, int awaitQty)
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/RequestDetail/updateAwait/{reqId}/{awaitQty}")]
        public HttpResponseMessage UpdateAwait(int reqId, int awaitQty)
        {
            try
            {
                BusinessLogic.RequestDetailBL.UpdateAwait(reqId,awaitQty);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }

        //string GetDeptCode(RequestDetail reqDet)
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
        [Route("api/RequestDetail/updateFulfilled/{reqId}/{fulfilledQty}")]
        public HttpResponseMessage UpdateFulfilled(int reqId, int fulfilledQty)
        {
            try
            {
                BusinessLogic.RequestDetailBL.UpdateAwait(reqId, fulfilledQty);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "error");
            }
        }
    }
}
