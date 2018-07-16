using Group8AD_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class RequestDetailBL
    {
        //add RequestDetail with empId , reqDet and status
        public static RequestDetail AddReqDet(int empId, RequestDetail reqDet , string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var req_det = from req_detail in entities.RequestDetails.Where(d => d.ReqId == reqDet.ReqId)
                              from req in entities.Requests
                              where req_detail.ReqId == req.ReqId && req.EmpId == empId && req.Status ==status
                              select new { RequestDetail = req_detail, Request = req };

                if (req_det != null)
                {
                    //entities.RequestDetail.Add(req_det);
                    entities.SaveChanges();
                }
            }
            return reqDet;
        }

        //add RequestDetail with reqId and reqDet
        public static RequestDetail AddReqDet(int reqId, RequestDetail reqDet)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var req_det = entities.RequestDetails.Where(d => d.ReqId == reqId).First<RequestDetail>();
                if (req_det != null)
                {
                    entities.RequestDetails.Add(req_det);
                    entities.SaveChanges();
                }
            }
            return reqDet;
        }

        //update RequestDetail with reqId and reqDet
        public static RequestDetail UpdateReqDet(int reqId, RequestDetail reqDet)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var req_det = entities.RequestDetails.Where(d => d.ReqId == reqId).First<RequestDetail>();
                //RequestDetail requestDetail = reqDet;
                entities.SaveChanges();

            }
            return reqDet;
        }
        //remove removeReqDet by empId and reqId 
        public static void removeReqDet(int empId, string itemCode, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                //var req_det = from req_detail in entities.RequestDetail.Where(d => d.ReqId == empId)
                //                from req in entities.Request
                //                where req_detail.ReqId == req.ReqId && req.EmpId == empId //&& req.Status == status
                //                select new { RequestDetail = req_detail, Request = req.Status };
                var req_det = entities.RequestDetails.Where(d => d.ReqId == empId).First<RequestDetail>();
                if (req_det != null)
                {
                    req_det.ItemCode = itemCode;
                    entities.RequestDetails.Remove(req_det);
                    entities.SaveChanges();
                }

            }
        }

        //remove ReqDet
        public static void removeReqDet(int reqId, string itemCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var req_det = entities.RequestDetails.Where(d => d.ReqId == reqId).First<RequestDetail>();
                if (req_det != null)
                {
                    req_det.ItemCode = itemCode;
                    entities.RequestDetails.Remove(req_det);
                    entities.SaveChanges();
                }

            }
        }

        //remove All
        public static void removeAllReqDet(int reqId, string itemCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                
            }
        }

        //get ReqDetList
        public static List<RequestDetail> GetReqDetList(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
               List<RequestDetail> requestDetailslist = entities.RequestDetails.ToList<RequestDetail>();
                
                return requestDetailslist;
            }
        }

        //update Await
        public static void UpdateAwait(int reqId, int awaitQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var req_det = entities.RequestDetails.Where(d => d.ReqId == reqId).First<RequestDetail>();
                if (req_det != null)
                {
                    req_det.AwaitQty = awaitQty;
                    entities.SaveChanges();
                }

            }
        }

        //GetDeptCode
        public static string GetDeptCode(RequestDetail reqDet)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

            }
            return "";
        }

        //Update Fulfilled
        public static void UpdateFulfilled(int reqId, int fulfilledQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var req_det = entities.RequestDetails.Where(d => d.ReqId == reqId).First<RequestDetail>();
                if (req_det != null)
                {
                    req_det.FulfilledQty = fulfilledQty;
                    entities.SaveChanges();
                }

            }
        }
        
    }
}