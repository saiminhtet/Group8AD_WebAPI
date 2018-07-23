﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.BusinessLogic;
using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;

namespace Group8AD_WebAPI.Controllers
{
    public class RequestController : ApiController
    {
        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/get")]
        public HttpResponseMessage GetRequestByIdStatus(int empId, string status)
        {
            List<RequestVM> reqlist = RequestBL.GetReq(empId, status);
            if (reqlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, reqlist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/get")]
        public HttpResponseMessage GetRequestByDateRange(int empId, string status, DateTime fromDate, DateTime toDate)
        {
            List<RequestVM> reqlist = RequestBL.GetReq(empId, status, fromDate, toDate);
            if (reqlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, reqlist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/get")]
        public HttpResponseMessage GetRequestByStatus(string status)
        {
            List<RequestVM> reqlist = RequestBL.GetReq(status);
            if (reqlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, reqlist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/get")]
        public HttpResponseMessage GetRequestByDeptCodeStatus(string deptCode, string status)
        {
            List<RequestVM> reqlist = RequestBL.GetReq(deptCode, status);
            if (reqlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, reqlist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/get")]
        public HttpResponseMessage GetRequestByReqId(int reqId)
        {
            RequestVM request = RequestBL.GetReq(reqId);
            if (request == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, request);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/add")]
        public HttpResponseMessage AddRequest(int empId, string status)
        {
            RequestVM request = RequestBL.AddReq(empId, status);
            if (request == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, request);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/remove")]
        public HttpResponseMessage DeleteRequestByEmpIdStatus(int empId, string status)
        {
            try
            {
                RequestBL.RemoveReq(empId, status);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/remove")]
        public HttpResponseMessage DeleteRequestByReqId(int reqId)
        {
            try
            {
                RequestBL.RemoveReq(reqId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/submit")]
        public HttpResponseMessage SubmitRequest(int reqId, List<RequestDetailVM> reqDetList)
        {
            RequestVM request = RequestBL.SubmitReq(reqId, reqDetList);
            if (request == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, request);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/update")]
        public HttpResponseMessage UpdateRequest(RequestVM req)
        {
            RequestVM request = RequestBL.UpdateReq(req);
            if (request == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, request);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/accept")]
        public HttpResponseMessage AcceptRequest(int reqId, int empId, string cmt)
        {
            try
            {
                RequestBL.AcceptRequest(reqId, empId, cmt);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/reject")]
        public HttpResponseMessage RejectRequest(int reqId, int empId, string cmt)
        {
            try
            {
                RequestBL.RejectRequest(reqId, empId, cmt);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // tested, dummy
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Request/updatefulfilled")]
        public HttpResponseMessage UpdateFulfilledStatus()
        {
            try
            {
                RequestBL.UpdateFulfilledRequestStatus();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}