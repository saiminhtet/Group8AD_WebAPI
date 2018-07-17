using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public class RequestBL
    {
        // dummy code

        // get a list of request by empId and status
        public static List<RequestVM> GetReq(int empId, string status)
        {
            List<RequestVM> reqlist = new List<RequestVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                reqlist = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).Select(r => new RequestVM()
                {
                    ReqId = r.ReqId,
                    EmpId = r.EmpId,
                    //ApproverId = r.ApproverId,
                    ApproverComment = r.ApproverComment,
                    //ReqDateTime = r.ReqDateTime,
                    //ApprovedDateTime = r.ApprovedDateTime,
                    //CancelledDateTime = r.CancelledDateTime,
                    //FulfilledDateTime = r.FulfilledDateTime,
                    Status = r.Status
                }).ToList<RequestVM>();
            }
            return reqlist;
        }

        // get a list of request by status
        public static List<RequestVM> GetReq(string status)
        {
            List<RequestVM> reqlist = new List<RequestVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                reqlist = entities.Requests.Where(r => r.Status == status).Select(r => new RequestVM()
                {
                    ReqId = r.ReqId,
                    EmpId = r.EmpId,
                    //ApproverId = r.ApproverId,
                    ApproverComment = r.ApproverComment,
                    //ReqDateTime = r.ReqDateTime,
                    //ApprovedDateTime = r.ApprovedDateTime,
                    //CancelledDateTime = r.CancelledDateTime,
                    //FulfilledDateTime = r.FulfilledDateTime,
                    Status = r.Status
                }).ToList<RequestVM>();
            }
            return reqlist;
        }

        // dummy, hope can add "deptCode" into "Request" table
        // get a list of request by deptCode and status
        public static List<RequestVM> GetReq(string deptCode, string status)
        {
            List<RequestVM> reqlist = new List<RequestVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                reqlist = entities.Requests.Where(r => r.Status == status).Select(r => new RequestVM()
                {
                    ReqId = r.ReqId,
                    EmpId = r.EmpId,
                    //ApproverId = r.ApproverId,
                    ApproverComment = r.ApproverComment,
                    //ReqDateTime = r.ReqDateTime,
                    //ApprovedDateTime = r.ApprovedDateTime,
                    //CancelledDateTime = r.CancelledDateTime,
                    //FulfilledDateTime = r.FulfilledDateTime,
                    Status = r.Status
                }).ToList<RequestVM>();
            }
            return reqlist;
        }

        // get request by reqId
        public static RequestVM GetReq(int reqId)
        {
            RequestVM request = new RequestVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                request = entities.Requests.Where(r => r.ReqId == reqId).Select(r => new RequestVM()
                {
                    ReqId = r.ReqId,
                    EmpId = r.EmpId,
                    //ApproverId = r.ApproverId,
                    ApproverComment = r.ApproverComment,
                    //ReqDateTime = r.ReqDateTime,
                    //ApprovedDateTime = r.ApprovedDateTime,
                    //CancelledDateTime = r.CancelledDateTime,
                    //FulfilledDateTime = r.FulfilledDateTime,
                    Status = r.Status
                }).First<RequestVM>();
            }
            return request;
        }

        // dummy
        // add request
        public static RequestVM AddReq(int empId, string status)
        {
            RequestVM request = new RequestVM();
            //Request req = new Request();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                //req.EmpId = empId;
                //req.Status = status;
                //entities.Requests.Add(req);
                //entities.SaveChanges();

                request = entities.Requests.Select(r => new RequestVM()
                {
                    ReqId = r.ReqId,
                    EmpId = r.EmpId,
                    //ApproverId = r.ApproverId,
                    ApproverComment = r.ApproverComment,
                    //ReqDateTime = r.ReqDateTime,
                    //ApprovedDateTime = r.ApprovedDateTime,
                    //CancelledDateTime = r.CancelledDateTime,
                    //FulfilledDateTime = r.FulfilledDateTime,
                    Status = r.Status
                }).First<RequestVM>();
            }
            return request;
        }

        // remove request by empId and status
        public static void RemoveReq(int empId, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Request> reqlist = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).ToList();
                if (!reqlist.Any())
                {
                    for (int i = 0; i < reqlist.Count; i++)
                    {
                        entities.Requests.Remove(reqlist[i]);
                        entities.SaveChanges();
                    }
                }

            }
        }

        // remove request by reqId
        public static void RemoveReq(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var request = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                if (request != null)
                {
                    entities.Requests.Remove(request);
                    entities.SaveChanges();
                }

            }
        }

        // dummy
        // submit request
        public static RequestVM SubmitReq(int empId, List<RequestDetailVM> reqDetList, string status)
        {
            // This is only to explain code steps at Web Api service
            // Call GetReq(empId, “Unsubmitted”)
            // Call UpdateReqDet(reqId, reqDet) for each reqDet in reqDetLst
            // Set reqDateTime for currReq object to DateTime.Now()
            // Set status for currReq to “Submitted”
            // Call UpdateReq(currReq) to persist at db
            // Return currReq object

            RequestVM request = new RequestVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                request = entities.Requests.Select(r => new RequestVM()
                {
                    ReqId = r.ReqId,
                    EmpId = r.EmpId,
                    //ApproverId = r.ApproverId,
                    ApproverComment = r.ApproverComment,
                    //ReqDateTime = r.ReqDateTime,
                    //ApprovedDateTime = r.ApprovedDateTime,
                    //CancelledDateTime = r.CancelledDateTime,
                    //FulfilledDateTime = r.FulfilledDateTime,
                    Status = r.Status
                }).First<RequestVM>();
            }
            return request;
        }

        // dummy
        // update request
        public static RequestVM UpdateReq(Request req)
        {
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    var request = entities.Requests.Where(r => r.ReqId == req.ReqId).FirstOrDefault();
            //    request.ReqId = req.ReqId;
            //    request.EmpId = req.EmpId;
            //    request.ApproverId = req.ApproverId;
            //    request.ApproverComment = req.ApproverComment;
            //    request.ReqDateTime = req.ReqDateTime;
            //    request.ApprovedDateTime = req.ApprovedDateTime;
            //    request.CancelledDateTime = req.CancelledDateTime;
            //    request.FulfilledDateTime = req.FulfilledDateTime;
            //    request.Status = req.Status;
            //    entities.SaveChanges();
            //    return request;
            //}

            RequestVM request = new RequestVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                request = entities.Requests.Select(r => new RequestVM()
                {
                    ReqId = r.ReqId,
                    EmpId = r.EmpId,
                    //ApproverId = r.ApproverId,
                    ApproverComment = r.ApproverComment,
                    //ReqDateTime = r.ReqDateTime,
                    //ApprovedDateTime = r.ApprovedDateTime,
                    //CancelledDateTime = r.CancelledDateTime,
                    //FulfilledDateTime = r.FulfilledDateTime,
                    Status = r.Status
                }).First<RequestVM>();
            }
            return request;
        }

        // accept request
        public static void AcceptRequest(int reqId, int empId, string cmt)
        {
            // This is only to explain code steps at Web Api service
            // Call GetReq(empId, “Unsubmitted”)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Add ApprovalDateTime as DateTime.Now()
            // Update Status as “Approved”
            return;
        }

        // reject request
        public static void RejectRequest(int reqId, int empId,string cmt)
        {
            // This is only to explain code steps at Web Api service
            // Call GetReq(empId, “Unsubmitted”)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Add ApprovalDateTime as DateTime.Now()
            // Update Status as “Rejected”
            return;
        }

        // update fulfilled request status
        public static void UpdateFulfilledRequestStatus()
        {
            // int openCount = 0;
            // foreach(RequestDetail rd in r) {
            //  int shortQty = 
            //      (rd.ReqQty - rd.FulfilledQty);
            //  openCount += shortQty;}
            // if (openCount == 0)
            //  r.Status = “Fulfilled”;
            // Save Changes for this Request object 
            return;
        }
    }
}