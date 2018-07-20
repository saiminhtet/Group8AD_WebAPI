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
    public class TransactionController : ApiController
    {
        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Transaction/add")]
        public HttpResponseMessage AddTransaction(TransactionVM t)
        {
            TransactionVM transaction = TransactionBL.AddTran(t);
            if (transaction == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, transaction);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Transaction/CBMonth")]
        public HttpResponseMessage CBByMth(string deptCode, DateTime fromDate, DateTime toDate)
        {
            List<ReportItemVM> translist = ReportItemBL.GetCBByMth(deptCode, fromDate, toDate);
            if (translist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, translist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Transaction/CBRange")]
        public HttpResponseMessage CBByRng(string deptCode, DateTime fromDate, DateTime toDate)
        {
            List<ReportItemVM> translist = ReportItemBL.GetCBByRng(deptCode, fromDate, toDate);
            if (translist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, translist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Transaction/getLastTen")]
        public HttpResponseMessage GetLastTenTrans(string itemCode)
        {
            List<TransactionVM> translist = ReportItemBL.GetLastTenTrans(itemCode);
            if (translist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, translist);
        }

        // tested
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Transaction/CBMonthly")]
        public HttpResponseMessage CBMonthly(DateTime toDate)
        {
            List<ReportItemVM> translist = ReportItemBL.GetCBMonthly(toDate);
            if (translist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, translist);
        }

        // tested, dummy
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Transaction/VolAnnual")]
        public HttpResponseMessage VolAnnual(DateTime toDate)
        {
            List<TransactionVM> translist = ReportItemBL.GetVolAnnual(toDate);
            if (translist == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, translist);
        }

        // tested, dummy
        [AcceptVerbs("POST")]
        [HttpPost]
        [Route("api/Transaction/costReport")]
        public HttpResponseMessage ShowCostReport(string dept1, string dept2, string supp1, string supp2,
            string cat, string type, List<DateTime> dates, bool byMonth)
        {
            try
            {
                ReportItemBL.ShowCostReport(dept1, dept2, supp1, supp2, cat, type, dates, byMonth);
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
        [Route("api/Transaction/volumeReport")]
        public HttpResponseMessage ShowVolumeReport(string dept1, string dept2, string supp1, string supp2,
            string cat, string type, List<DateTime> dates, bool byMonth)
        {
            try
            {
                ReportItemBL.ShowVolumeReport(dept1, dept2, supp1, supp2, cat, type, dates, byMonth);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
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