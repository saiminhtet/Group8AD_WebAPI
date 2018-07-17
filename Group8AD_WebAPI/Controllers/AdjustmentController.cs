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
    //Controllers
    public class AdjustmentController : ApiController
    {
        [Route("api/adjustment/add")]
        public HttpResponseMessage AddAdjustment(Adjustment adj)
        {
            AdjustmentVM adjustment = AdjustmentBL.AddAdj(adj);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/adjustment/{voucherNo}")]
        public HttpResponseMessage GetAdjustment(string voucherNo)
        {
            AdjustmentVM adjustment = AdjustmentBL.GetAdj(voucherNo);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/adjustment/{status}")]
        public HttpResponseMessage GetAdjustmentList(string status)
        {
            List<AdjustmentVM> adjlist = AdjustmentBL.GetAdjList(status);
            if (adjlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjlist);
        }

        [Route("api/adjustment/raise")]
        public HttpResponseMessage RaiseAdjustment(int empId, List<AdjustmentVM> iList)
        {
            List<AdjustmentVM> adjlist = AdjustmentBL.RaiseAdjustments(empId, iList);
            if (adjlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjlist);
        }

        [Route("api/adjustment/accept")]
        public HttpResponseMessage AcceptRequest(string voucherNo, int empId, string cmt)
        {
            AdjustmentBL.AcceptRequest(voucherNo, empId, cmt);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/adjustment/rejct")]
        public HttpResponseMessage RejectRequest(string voucherNo, int empId, string cmt)
        {
            AdjustmentBL.RejectRequest(voucherNo, empId, cmt);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}