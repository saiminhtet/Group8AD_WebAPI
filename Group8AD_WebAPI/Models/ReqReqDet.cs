using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Models
{
    public class ReqReqDet
    {
        public Request Request { get; set; }
        public List<RequestDetail> RequestDetail { get; set; }

        public static ReqReqDet GetReqReqDets(int reqId)
        {
            ReqReqDet reqJoinReqDet = new ReqReqDet();
            reqJoinReqDet.Request = Request.GetReqById(reqId);
            reqJoinReqDet.RequestDetail = Group8AD_WebAPI.Models.RequestDetail.GetReqDetByReqId(reqId);
            return reqJoinReqDet;
        }
    }
}