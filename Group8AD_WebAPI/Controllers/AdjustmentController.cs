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
        [Route("api/Adjustment/add/{adj}")]
        public HttpResponseMessage AddAdjustment(Adjustment adj)
        {
            AdjustmentVM adjustment = AdjustmentBL.AddAdj(adj);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        // cannot test as vohcherNo contain "/" character
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Adjustment/voucher/{voucherNo}")]
        public HttpResponseMessage GetAdjustment(string voucherNo)
        {
            AdjustmentVM adjustment = AdjustmentBL.GetAdj(voucherNo);
            if (adjustment == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjustment);
        }

        // test done
        [AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Adjustment/status/{status}")]
        public HttpResponseMessage GetAdjustmentList(string status)
        {
            List<AdjustmentVM> adjlist = AdjustmentBL.GetAdjList(status);
            if (adjlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjlist);
        }

        [Route("api/Adjustment/raise/{empId}/{iList}")]
        public HttpResponseMessage RaiseAdjustment(int empId, List<AdjustmentVM> iList)
        {
            List<AdjustmentVM> adjlist = AdjustmentBL.RaiseAdjustments(empId, iList);
            if (adjlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, adjlist);
        }

        // test done
        [Route("api/Adjustment/accept/{voucherNo}/{empId}/{cmt}")]
        public HttpResponseMessage AcceptRequest(string voucherNo, int empId, string cmt)
        {
            AdjustmentBL.AcceptRequest(voucherNo, empId, cmt);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // test done
        [Route("api/Adjustment/reject/{voucherNo}/{empId}/{cmt}")]
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