using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Group8AD_WebAPI.Models;

namespace Group8AD_WebAPI.Controllers
{
    public class RequestController : ApiController
    {
        //Returns request object belonging to the given request id
        //[HttpPost]    
        //public Request GetReqById(int reqId)
        //{
        //    return Request.GetReqById(reqId);
        //}

        ////Returns a list of request belonging to the given employee id for the given status
        ////Pass empty string parameter to status to return all requests belonging to the given employee id
        //[HttpPost]
        //public List<Request> GetReqs(int empId, string status)
        //{
        //    return Request.GetReqs(empId, status);
        //}

        ////Returns a list of request in the same department as the given employee id
        //[HttpPost]
        //public List<Request> GetDeptReqs(int empId)
        //{
        //    return Request.GetDeptReqs(empId);
        //}

        ////Returns the request object for the given ReqId
        //[HttpPost]
        //public Request GetReq(int reqId)
        //{
            
        //    return Request.GetReq(reqId);
        //}

        ////Returns a list of request base on the search criteria: department code, employee name
        ////Pass empty string parameter if that criteria is not required (e.g. empName = "")
        //[HttpPost]
        //public List<Request> GetReqsByDeptEmp(string deptCode, string empName)
        //{
        //    return Request.GetReqsByDeptEmp(deptCode, empName);
        //}

        ////Returns a list of unfulfilled request
        //[HttpPost]
        //public List<Request> GetUnfulfilledReqs()
        //{
        //    return Request.GetUnfulfilledReqs();
        //}

        //public Boolean RemoveCurrReq(int empId)
        //{
        //    return Request.RemoveCurrReq(empId);
        //}

        //public Boolean SubmitCurrReq(int empId)
        //{
        //    return Request.SubmitCurrReq(empId);
        //}

        //public Boolean ApproveReq(int reqId, Boolean isApprove, string comment)
        //{
        //    return Request.ApproveReq(reqId, isApprove, comment);
        //}

        //public Boolean AcceptDisb(int empId, List<ItemQty> fulfilItems)
        //{
        //    return Request.AcceptDisb(empId, fulfilItems);
        //}

        //public static Boolean UpdateColPt(int empId, int colPtId)
        //{
        //    return true;
        //}

        //public static Boolean FulfilReqs(List<ItemQty> itemQtys)
        //{
        //    return true;
        //}

        //public Boolean FulfilEmpReq(int reqId, List<ItemQty> itemQtys)
        //{
        //    return true;
        //}

        //public Boolean SubmitStockTake(List<ItemQty> itemQtys)
        //{
        //    return true;
        //}
    }
}