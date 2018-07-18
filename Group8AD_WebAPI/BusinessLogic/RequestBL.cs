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
            // need to check employee role first
            // all: except bookmarked, unsubmitted

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
            List<RequestVM> reqlist = new List<RequestVM>();
            List<Request> lst = new List<Request>();
            Employee employee = new Employee();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                employee = entities.Employees.Where(e => e.EmpId == empId).FirstOrDefault();
            }
            string role = employee.Role;
            string deptCode = employee.DeptCode;
            if (role == "Department Head" || role == "Delegate")
            {
                reqlist = GetReq(deptCode, status);
            }
            else
            {
                if (status == "All")
                {
                    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                    {
                        lst = entities.Requests.Where(r => r.EmpId == empId && (r.Status == "Submitted" ||
                        r.Status == "Approved" || r.Status == "Rejected" || r.Status == "Cancelled" || r.Status == "Fulfilled")).ToList();
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
                            req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                        if (lst[i].ApprovedDateTime != null)
                            req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                        if (lst[i].CancelledDateTime != null)
                            req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                        if (lst[i].FulfilledDateTime != null)
                            req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                        req.Status = lst[i].Status;
                        reqlist.Add(req);
                    }
                }
                else
                {
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
                            req.ApproverId = (int)lst[i].ApproverId;
                        else
                            req.ApproverId = 0;
                        req.ApproverComment = lst[i].ApproverComment;
                        if (lst[i].ReqDateTime != null)
                            req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                        if (lst[i].ApprovedDateTime != null)
                            req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                        if (lst[i].CancelledDateTime != null)
                            req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                        if (lst[i].FulfilledDateTime != null)
                            req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                        req.Status = lst[i].Status;
                        reqlist.Add(req);
                    }
                }
            }
            return reqlist;
        }

        // get request by empId, status, fromDate and toDate
        // done
        public static List<RequestVM> GetReq(int empId, string status, DateTime fromDate, DateTime toDate)
        {
            List<RequestVM> reqlist = GetReq(empId, status);
            List<RequestVM> list = new List<RequestVM>();
            for (int i = 0; i < reqlist.Count; i++)
            {
                DateTime reqDate = reqlist[i].ReqDateTime;
                int result1 = DateTime.Compare(reqDate, fromDate);
                int result2 = DateTime.Compare(reqDate, toDate);
                if (result1 >= 0 && result2 <= 0)
                    list.Add(reqlist[i]);
            }
            return list;
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
            if (status == "All")
            {
                using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                {
                    lst = entities.Requests.Where(r => r.Status == "Submitted" || r.Status == "Approved" ||
                    r.Status == "Rejected" || r.Status == "Cancelled" || r.Status == "Fulfilled").ToList();
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
                        req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                    if (lst[i].ApprovedDateTime != null)
                        req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                    if (lst[i].CancelledDateTime != null)
                        req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                    if (lst[i].FulfilledDateTime != null)
                        req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                    req.Status = lst[i].Status;
                    reqlist.Add(req);
                }
            }
            else
            {
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
                        req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                    if (lst[i].ApprovedDateTime != null)
                        req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                    if (lst[i].CancelledDateTime != null)
                        req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                    if (lst[i].FulfilledDateTime != null)
                        req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                    req.Status = lst[i].Status;
                    reqlist.Add(req);
                }
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
                List<Request> templist = new List<Request>();
                if (status == "All")
                {
                    templist = entities.Requests.Where(r => r.Status == "Submitted" || r.Status == "Approved" ||
                    r.Status == "Rejected" || r.Status == "Cancelled" || r.Status == "Fulfilled").ToList();
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
                                    req.ApproverId = (int)templist[i].ApproverId;
                                else
                                    req.ApproverId = 0;
                                req.ApproverComment = templist[i].ApproverComment;
                                if (templist[i].ReqDateTime != null)
                                    req.ReqDateTime = (DateTime)templist[i].ReqDateTime;
                                if (templist[i].ApprovedDateTime != null)
                                    req.ApprovedDateTime = (DateTime)templist[i].ApprovedDateTime;
                                if (templist[i].CancelledDateTime != null)
                                    req.CancelledDateTime = (DateTime)templist[i].CancelledDateTime;
                                if (templist[i].FulfilledDateTime != null)
                                    req.FulfilledDateTime = (DateTime)templist[i].FulfilledDateTime;
                                req.Status = templist[i].Status;
                                reqlist.Add(req);
                            }
                        }
                    }
                }
                else
                {
                    templist = entities.Requests.Where(r => r.Status == status).ToList();
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
                                    req.ApproverId = (int)templist[i].ApproverId;
                                else
                                    req.ApproverId = 0;
                                req.ApproverComment = templist[i].ApproverComment;
                                if (templist[i].ReqDateTime != null)
                                    req.ReqDateTime = (DateTime)templist[i].ReqDateTime;
                                if (templist[i].ApprovedDateTime != null)
                                    req.ApprovedDateTime = (DateTime)templist[i].ApprovedDateTime;
                                if (templist[i].CancelledDateTime != null)
                                    req.CancelledDateTime = (DateTime)templist[i].CancelledDateTime;
                                if (templist[i].FulfilledDateTime != null)
                                    req.FulfilledDateTime = (DateTime)templist[i].FulfilledDateTime;
                                req.Status = templist[i].Status;
                                reqlist.Add(req);
                            }
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

        // add request
        // done
        public static RequestVM AddReq(int empId, string status)
        {
            RequestVM request = new RequestVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Request req = new Request();
                req.EmpId = empId;
                req.Status = status;
                req.ApproverComment = "";
                entities.Requests.Add(req);
                entities.SaveChanges();

                List<Request> lst = entities.Requests.ToList();
                Request r = lst[lst.Count - 1];
                request.ReqId = r.ReqId;
                request.EmpId = r.EmpId;
                if (r.ApproverId != null)
                    request.ApproverId = (int)r.ApproverId;
                else
                    request.ApproverId = 0;
                request.ApproverComment = r.ApproverComment;
                if (r.ReqDateTime != null)
                    request.ReqDateTime = (DateTime)r.ReqDateTime;
                if (r.ApprovedDateTime != null)
                    request.ApprovedDateTime = (DateTime)r.ApprovedDateTime;
                if (r.CancelledDateTime != null)
                    request.CancelledDateTime = (DateTime)r.CancelledDateTime;
                if (r.FulfilledDateTime != null)
                    request.FulfilledDateTime = (DateTime)r.FulfilledDateTime;
                request.Status = r.Status;
            }
            return request;
        }

        // remove request by empId and status
        // done
        public static void RemoveReq(int empId, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Request> reqlist = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).ToList();
                if (reqlist.Count > 0)
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

        // remove request by reqId
        // done
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

            //List<RequestVM> reqlist = GetReq(empId, "Unsubmitted");
            //for (int i = 0; i < reqlist.Count; i++)
            //{

            //}

            // dummy
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

        // update request
        // not dummy, but unable to pass in Request object to test
        public static Request UpdateReq(Request req)
        {
            RequestVM rvm = new RequestVM();
            Request request = new Request();
            if (req != null)
            {
                int reqId = req.ReqId;
                using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                {
                    request = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                    request.EmpId = req.EmpId;
                    if (req.ApproverId != null)
                        request.ApproverId = (int)req.ApproverId;
                    else
                        request.ApproverId = 0;
                    request.ApproverComment = req.ApproverComment;
                    if (req.ReqDateTime != null)
                        request.ReqDateTime = req.ReqDateTime;
                    if (req.ApprovedDateTime != null)
                        request.ApprovedDateTime = req.ApprovedDateTime;
                    if (req.CancelledDateTime != null)
                        request.CancelledDateTime = req.CancelledDateTime;
                    if (req.FulfilledDateTime != null)
                        request.FulfilledDateTime = req.FulfilledDateTime;
                    request.Status = req.Status;
                    entities.SaveChanges();

                    rvm.ReqId = request.ReqId;
                    rvm.EmpId = request.EmpId;
                    if (request.ApproverId != null)
                        rvm.ApproverId = (int)request.ApproverId;
                    else
                        rvm.ApproverId = 0;
                    rvm.ApproverComment = request.ApproverComment;
                    if (request.ReqDateTime != null)
                        rvm.ReqDateTime = (DateTime)request.ReqDateTime;
                    if (request.ApprovedDateTime != null)
                        rvm.ApprovedDateTime = (DateTime)request.ApprovedDateTime;
                    if (request.CancelledDateTime != null)
                        rvm.CancelledDateTime = (DateTime)request.CancelledDateTime;
                    if (request.FulfilledDateTime != null)
                        rvm.FulfilledDateTime = (DateTime)request.FulfilledDateTime;
                    rvm.Status = request.Status;
                }
            }
            else
            {
                rvm.ReqId = 1;
                request.ReqId = 1;
            }
            return request;
        }

        // accept request
        // not dummy, not tested, will test after GetReq()
        public static void AcceptRequest(int reqId, int empId, string cmt)
        {
            // This is only to explain code steps at Web Api service
            // Call GetReq(empId, “Unsubmitted”)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Add ApprovalDateTime as DateTime.Now()
            // Update Status as “Approved”

            List<RequestVM> reqlist = GetReq(empId, "Unsubmitted");
            for (int i = 0; i < reqlist.Count; i++)
            {
                if (reqlist[i].ReqId == reqId)
                {
                    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                    {
                        Request req = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                        req.ApproverId = empId;
                        req.ApproverComment = cmt;
                        req.ApprovedDateTime = DateTime.Now;
                        req.Status = "Approved";
                        entities.SaveChanges();
                    }
                }
            }
            return;
        }

        // reject request
        // not dummy, not tested, will test after GetReq()
        public static void RejectRequest(int reqId, int empId,string cmt)
        {
            // This is only to explain code steps at Web Api service
            // Call GetReq(empId, “Unsubmitted”)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Add ApprovalDateTime as DateTime.Now()
            // Update Status as “Rejected”

            List<RequestVM> reqlist = GetReq(empId, "Unsubmitted");
            for (int i = 0; i < reqlist.Count; i++)
            {
                if (reqlist[i].ReqId == reqId)
                {
                    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                    {
                        Request req = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                        req.ApproverId = empId;
                        req.ApproverComment = cmt;
                        req.ApprovedDateTime = DateTime.Now;
                        req.Status = "Rejected";
                        entities.SaveChanges();
                    }
                }
            }
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