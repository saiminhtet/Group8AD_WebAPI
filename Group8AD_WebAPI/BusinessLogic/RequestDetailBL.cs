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
        
        public static RequestDetailVM AddReqDet(int empId, string itemCode,int reqQty, string status)
        {
            RequestDetailVM reqDetail = new RequestDetailVM();
            RequestVM req = new RequestVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<RequestVM> requestlists = RequestBL.GetReq(empId, status);
                foreach (RequestVM r in requestlists)
                {
                    req.ReqId = r.ReqId;
                }

                if (requestlists.Count == 0)
                {
                    RequestBL.AddReq(empId, status);
                }

                List<RequestDetail> requestDetailList = entities.RequestDetails.Where(x => x.ReqId == req.ReqId).ToList<RequestDetail>();

                reqDetail = entities.RequestDetails.Where(r => r.ReqId == req.ReqId).Select(r => new RequestDetailVM()
                {
                    ReqId = req.ReqId,
                    ReqLineNo = r.ReqLineNo,
                    ItemCode = itemCode,
                    ReqQty = reqQty,
                    AwaitQty = r.AwaitQty,
                    FulfilledQty = r.FulfilledQty
                }).First<RequestDetailVM>();

                foreach (RequestDetail rd in requestDetailList)
                {
                    if (status == "Unsubmitted")
                    {
                        if (reqDetail.ItemCode == rd.ItemCode)//if exist reqDet with same itemCode
                        {
                            reqDetail.ReqQty++;//increase reqQty

                            UpdateReqDet(reqDetail.ReqId, reqDetail);
                        }
                        else
                        {
                            AddReqDet(req.ReqId, reqDetail);//create reqDet
                        }
                    }

                    if (status == "Bookmarked")
                    {
                        if (reqDetail.ItemCode != rd.ItemCode)//if reqDet does not exist  with itemCode
                        {
                            AddReqDet(reqDetail.ReqId, reqDetail);//create reqDet
                        }
                    }
                }

            }
            return reqDetail;
        }

        //add RequestDetail with reqId and reqDet
        public static RequestDetailVM AddReqDet(int reqId, RequestDetailVM reqDet)
        {
            RequestDetailVM request = new RequestDetailVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                RequestDetail req = new RequestDetail();
                req.ReqId = reqId;
                req.ReqLineNo = reqDet.ReqLineNo;
                req.ItemCode = reqDet.ItemCode;
                req.ReqQty = reqDet.ReqQty;
                req.AwaitQty = reqDet.AwaitQty;
                req.FulfilledQty = reqDet.FulfilledQty;
                entities.RequestDetails.Add(req);
                entities.SaveChanges();

                List<RequestDetail> lst = entities.RequestDetails.ToList();
                RequestDetail r = lst[lst.Count - 1];
                request.ReqId = r.ReqId;
                request.ReqLineNo = r.ReqLineNo;
                request.ItemCode = r.ItemCode;
                request.ReqQty = r.ReqQty;
                request.AwaitQty = r.AwaitQty;
                request.FulfilledQty = r.FulfilledQty;
            }

            return request;

        }

        //update RequestDetail with reqId and reqDet
        public static RequestDetailVM UpdateReqDet(int reqId, RequestDetailVM reqDet)
        {
            RequestDetailVM updateReqDetail = new RequestDetailVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                updateReqDetail = entities.RequestDetails.Where(r => r.ReqId == reqId).Select(r => new RequestDetailVM()
                {
                    ReqId = reqId,
                    ReqLineNo = r.ReqLineNo,
                    ItemCode = r.ItemCode,
                    ReqQty = r.ReqQty,
                    AwaitQty = r.AwaitQty,
                    FulfilledQty = r.FulfilledQty,
                    //Request = r.Request
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
                List<RequestDetail> reqDetlist = entities.RequestDetails.Where(r => r.ItemCode == itemCode).ToList();
                if (reqDetlist.Count > 0)
                {
                    for (int i = 0; i < reqDetlist.Count; i++)
                    {
                        entities.RequestDetails.Remove(reqDetlist[i]);
                        entities.SaveChanges();
                    }
                }
            }
        }

        //remove ReqDet
        public static void removeReqDet(int reqId, string itemCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                RequestDetail request = entities.RequestDetails.Where(r => r.ReqId == reqId && r.ItemCode == itemCode).FirstOrDefault();
                if (request != null)
                {
                    entities.RequestDetails.Remove(request);
                    entities.SaveChanges();
                }
            }
        }

        //remove All
        public static void removeAllReqDet(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                RequestDetail request = entities.RequestDetails.Where(r => r.ReqId == reqId).FirstOrDefault();
                if (request != null)
                {
                    entities.RequestDetails.Remove(request);
                    entities.SaveChanges();
                }
            }
        }

        //get ReqDetList
        public static List<RequestDetailVM> GetReqDetList(int reqId)
        {
            List<RequestDetailVM> reqDetlists = new List<RequestDetailVM>();
            List<RequestDetail> lst = new List<RequestDetail>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                lst = entities.RequestDetails.Where(r => r.ReqId == reqId).ToList();
            }
            for (int i = 0; i < lst.Count; i++)
            {
                RequestDetailVM req = new RequestDetailVM();
                req.ReqId = lst[i].ReqId;
                req.ItemCode = lst[i].ItemCode;
                req.ReqLineNo = lst[i].ReqLineNo;
                req.ReqQty = lst[i].ReqQty;
                req.AwaitQty = lst[i].AwaitQty;
                req.FulfilledQty = lst[i].FulfilledQty;
                reqDetlists.Add(req);
            }
            return reqDetlists;

        }

        //update Await
        public static void UpdateAwait(int reqId, int awaitQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                RequestDetail await = entities.RequestDetails.Where(r => r.ReqId == reqId).First<RequestDetail>();
                if (await != null)
                {
                    await.AwaitQty = awaitQty;                    
                }
                entities.SaveChanges();
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
                RequestDetail fulfilled = entities.RequestDetails.Where(r => r.ReqId == reqId).First<RequestDetail>();
                //RequestDetail fulfilled = entities.RequestDetails.Where(r => r.ReqId == reqId).First<RequestDetail>();
                if (fulfilled != null)
                {
                    fulfilled.FulfilledQty = fulfilledQty;
                    entities.SaveChanges();
                }
            }
        }

    }
}