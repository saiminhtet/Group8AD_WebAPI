using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class RequestDetailBL
    {
        //add RequestDetail with empId , reqDet and status
        public static RequestDetailVM AddReqDet(int empId, RequestDetail reqDet , string status)
        {
            RequestDetailVM reqDetail = new RequestDetailVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                reqDetail = entities.RequestDetails.Where(r => r.Request.EmpId == empId && r.Request.Status == status).Select(r => new RequestDetailVM()
                {
                    ReqId = r.ReqId,
                    ReqLineNo = r.ReqLineNo,
                    ItemCode = r.ItemCode,
                    ReqQty = r.ReqQty,
                    AwaitQty = r.AwaitQty,
                    FulfilledQty = r.FulfilledQty,
                    Request = r.Request
                }).First<RequestDetailVM>();
            }
            return reqDetail;
        }

        //add RequestDetail with reqId and reqDet
        public static RequestDetailVM AddReqDet(int reqId, RequestDetail reqDet)
        {
            RequestDetailVM addReqDetail = new RequestDetailVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                addReqDetail = entities.RequestDetails.Where(r => r.ReqId == reqId).Select(r => new RequestDetailVM()
                {
                    ReqId = r.ReqId,
                    ReqLineNo = r.ReqLineNo,
                    ItemCode = r.ItemCode,
                    ReqQty = r.ReqQty,
                    AwaitQty = r.AwaitQty,
                    FulfilledQty = r.FulfilledQty,
                    Request = r.Request
                }).First<RequestDetailVM>();
            }
            return addReqDetail;
        }

        //update RequestDetail with reqId and reqDet
        public static RequestDetailVM UpdateReqDet(int reqId, RequestDetail reqDet)
        {
            RequestDetailVM updateReqDetail = new RequestDetailVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                updateReqDetail = entities.RequestDetails.Where(r => r.ReqId == reqId).Select(r => new RequestDetailVM()
                {
                    ReqId = r.ReqId,
                    ReqLineNo = r.ReqLineNo,
                    ItemCode = r.ItemCode,
                    ReqQty = r.ReqQty,
                    AwaitQty = r.AwaitQty,
                    FulfilledQty = r.FulfilledQty,
                    Request = r.Request
                }).First<RequestDetailVM>();
                entities.SaveChanges();
            }
            return updateReqDetail;
        }
        //remove removeReqDet by empId and reqId 
        public static void removeReqDet(int empId, string itemCode, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var reqDetail = entities.RequestDetails.Where(r => r.Request.EmpId == empId && r.Item.ItemCode == itemCode && r.Request.Status == status).FirstOrDefault();
                if (reqDetail != null)
                {
                    entities.RequestDetails.Remove(reqDetail);
                    entities.SaveChanges();
                }
            }            
        }

        //remove ReqDet
        public static void removeReqDet(int reqId, string itemCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var reqDetail = entities.RequestDetails.Where(r => r.ReqId == reqId && r.Item.ItemCode == itemCode).FirstOrDefault();
                if (reqDetail != null)
                {
                    entities.RequestDetails.Remove(reqDetail);
                    entities.SaveChanges();
                }
            }
        }

        //remove All
        public static void removeAllReqDet(int reqId, string itemCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var reqDetail = entities.RequestDetails.Where(r => r.ReqId == reqId && r.Item.ItemCode == itemCode).FirstOrDefault();
                if (reqDetail != null)
                {
                    entities.RequestDetails.Remove(reqDetail);
                    entities.SaveChanges();
                }
            }
        }

        //get ReqDetList
        public static List<RequestDetailVM> GetReqDetList(int reqId)
        {
            List<RequestDetailVM> reqDetlists = new List<RequestDetailVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                reqDetlists = entities.RequestDetails.Where(r => r.ReqId == reqId).Select(r => new RequestDetailVM()
                {
                    ReqId = r.ReqId,
                    ReqLineNo = r.ReqLineNo,
                    ItemCode = r.ItemCode,
                    ReqQty = r.ReqQty,
                    AwaitQty = r.AwaitQty,
                    FulfilledQty = r.FulfilledQty,
                    Request = r.Request
                }).ToList<RequestDetailVM>();
            }
            return reqDetlists;
        }

        //update Await
        public static void UpdateAwait(int reqId, int awaitQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var await = entities.RequestDetails.Where(r => r.ReqId == reqId && r.AwaitQty == awaitQty).FirstOrDefault();
                if (await != null)
                {
                    await.AwaitQty = awaitQty;
                    entities.SaveChanges();
                }
            }             
        }

        //GetDeptCode
        public static string GetDeptCode(RequestDetail reqDet)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptcode = entities.RequestDetails.Select(r => r.Request.Employee.DeptCode).FirstOrDefault();
                return deptcode;
            }
        }

        //Update Fulfilled
        public static void UpdateFulfilled(int reqId, int fulfilledQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var await = entities.RequestDetails.Where(r => r.ReqId == reqId && r.FulfilledQty == fulfilledQty).FirstOrDefault();
                if (await != null)
                {
                    await.FulfilledQty = fulfilledQty;
                    entities.SaveChanges();
                }
            }
        }
        
    }
}