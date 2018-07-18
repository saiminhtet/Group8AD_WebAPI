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
        // hope can set all fileld not null in Request table
        // done
        public static List<RequestVM> GetReq(int empId, string status)
        {
            List<RequestVM> reqlist = new List<RequestVM>();
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    reqlist = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).Select(r => new RequestVM()
            //    {
            //        ReqId = r.ReqId,
            //        EmpId = r.EmpId,
            //        ApproverId = (int) r.ApproverId,
            //        ApproverComment = r.ApproverComment,
            //        ReqDateTime = (DateTime) r.ReqDateTime,
            //        ApprovedDateTime = (DateTime) r.ApprovedDateTime,
            //        CancelledDateTime = (DateTime) r.CancelledDateTime,
            //        FulfilledDateTime = (DateTime) r.FulfilledDateTime,
            //        Status = r.Status
            //    }).ToList<RequestVM>();
            //}
            //return reqlist;
            List<Request> lst = new List<Request>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                lst = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).ToList();
            }
            for (int i = 0; i < lst.Count; i++)
            {
                RequestVM req = new RequestVM();
                req.ReqId = lst[i].ReqId;
                req.EmpId = lst[i].EmpId;
                if (lst[i].ApproverId != null)
                    req.ApproverId = (int) lst[i].ApproverId;
                else
                    req.ApproverId = 0;
                req.ApproverComment = lst[i].ApproverComment;
                if (lst[i].ReqDateTime != null)
                    req.ReqDateTime = (DateTime) lst[i].ReqDateTime;
                if (lst[i].ApprovedDateTime != null)
                    req.ApprovedDateTime = (DateTime) lst[i].ApprovedDateTime;
                if (lst[i].CancelledDateTime != null)
                    req.CancelledDateTime = (DateTime) lst[i].CancelledDateTime;
                if (lst[i].FulfilledDateTime != null)
                    req.FulfilledDateTime = (DateTime) lst[i].FulfilledDateTime;
                req.Status = lst[i].Status;
                reqlist.Add(req);
            }
            return reqlist;
        }

        // get a list of request by status
        // hope can set all fileld not null in Request table
        // done
        public static List<RequestVM> GetReq(string status)
        {
            List<RequestVM> reqlist = new List<RequestVM>();
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    reqlist = entities.Requests.Where(r => r.Status == status).Select(r => new RequestVM()
            //    {
            //        ReqId = r.ReqId,
            //        EmpId = r.EmpId,
            //        //ApproverId = r.ApproverId,
            //        ApproverComment = r.ApproverComment,
            //        //ReqDateTime = r.ReqDateTime,
            //        //ApprovedDateTime = r.ApprovedDateTime,
            //        //CancelledDateTime = r.CancelledDateTime,
            //        //FulfilledDateTime = r.FulfilledDateTime,
            //        Status = r.Status
            //    }).ToList<RequestVM>();
            //}
            List<Request> lst = new List<Request>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                lst = entities.Requests.Where(r => r.Status == status).ToList();
            }
            for (int i = 0; i < lst.Count; i++)
            {
                RequestVM req = new RequestVM();
                req.ReqId = lst[i].ReqId;
                req.EmpId = lst[i].EmpId;
                if (lst[i].ApproverId != null)
                    req.ApproverId = (int)lst[i].ApproverId;
                else
                    req.ApproverId = 0;
                req.ApproverComment = lst[i].ApproverComment;
                if (lst[i].ReqDateTime != null)
                    req.ReqDateTime = (DateTime) lst[i].ReqDateTime;
                if (lst[i].ApprovedDateTime != null)
                    req.ApprovedDateTime = (DateTime) lst[i].ApprovedDateTime;
                if (lst[i].CancelledDateTime != null)
                    req.CancelledDateTime = (DateTime) lst[i].CancelledDateTime;
                if (lst[i].FulfilledDateTime != null)
                    req.FulfilledDateTime = (DateTime) lst[i].FulfilledDateTime;
                req.Status = lst[i].Status;
                reqlist.Add(req);
            }
            return reqlist;
        }

        // get a list of request by deptCode and status
        // hope can have a "deptCode" field in Request table
        // done
        public static List<RequestVM> GetReq(string deptCode, string status)
        {
            List<RequestVM> reqlist = new List<RequestVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Employee> emplist = entities.Employees.Where(e => e.DeptCode == deptCode).ToList();
                List<Request> templist = entities.Requests.Where(r => r.Status == status).ToList();
                for (int i = 0; i < templist.Count; i++)
                {
                    for (int j = 0; j < emplist.Count; j++)
                    {
                        if (templist[i].EmpId == emplist[j].EmpId)
                        {
                            RequestVM req = new RequestVM();
                            req.ReqId = templist[i].ReqId;
                            req.EmpId = templist[i].EmpId;
                            if (templist[i].ApproverId != null)
                                req.ApproverId = (int) templist[i].ApproverId;
                            else
                                req.ApproverId = 0;
                            req.ApproverComment = templist[i].ApproverComment;
                            if (templist[i].ReqDateTime != null)
                                req.ReqDateTime = (DateTime) templist[i].ReqDateTime;
                            if (templist[i].ApprovedDateTime != null)
                                req.ApprovedDateTime = (DateTime) templist[i].ApprovedDateTime;
                            if (templist[i].CancelledDateTime != null)
                                req.CancelledDateTime = (DateTime) templist[i].CancelledDateTime;
                            if (templist[i].FulfilledDateTime != null)
                                req.FulfilledDateTime = (DateTime) templist[i].FulfilledDateTime;
                            req.Status = templist[i].Status;
                            reqlist.Add(req);
                        }
                    }
                }
            }
            return reqlist;
        }

        // get request by reqId
        // done
        public static RequestVM GetReq(int reqId)
        {
            RequestVM request = new RequestVM();
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    request = entities.Requests.Where(r => r.ReqId == reqId).Select(r => new RequestVM()
            //    {
            //        ReqId = r.ReqId,
            //        EmpId = r.EmpId,
            //        //ApproverId = r.ApproverId,
            //        ApproverComment = r.ApproverComment,
            //        //ReqDateTime = r.ReqDateTime,
            //        //ApprovedDateTime = r.ApprovedDateTime,
            //        //CancelledDateTime = r.CancelledDateTime,
            //        //FulfilledDateTime = r.FulfilledDateTime,
            //        Status = r.Status
            //    }).First<RequestVM>();
            //}
            Request req = new Request();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                req = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
            }
            request.ReqId = req.ReqId;
            request.EmpId = req.EmpId;
            if (req.ApproverId != null)
                request.ApproverId = (int)req.ApproverId;
            else
                request.ApproverId = 0;
            request.ApproverComment = req.ApproverComment;
            if (req.ReqDateTime != null)
                request.ReqDateTime = (DateTime)req.ReqDateTime;
            if (req.ApprovedDateTime != null)
                request.ApprovedDateTime = (DateTime)req.ApprovedDateTime;
            if (req.CancelledDateTime != null)
                request.CancelledDateTime = (DateTime)req.CancelledDateTime;
            if (req.FulfilledDateTime != null)
                request.FulfilledDateTime = (DateTime)req.FulfilledDateTime;
            request.Status = req.Status;

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

        // dummy
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
            return;
        }

        // not dummy, referential integrity issue
        // remove request by reqId
        public static void RemoveReq(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Request request = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                if (request != null)
                {
                    entities.Requests.Remove(request);
                    entities.SaveChanges();
                }
            }
            return;
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

        // dummy
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

        // dummy
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

        // dummy
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