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
        // get a list of request by empId and status
        // done
        public static List<RequestVM> GetReq(int empId, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<RequestVM> reqlist = new List<RequestVM>();
                List<Request> lst = new List<Request>();
                Employee employee = new Employee();
                employee = entities.Employees.Where(e => e.EmpId == empId).FirstOrDefault();
                string role = employee.Role;
                string deptCode = employee.DeptCode;
                Department dept = entities.Departments.Where(x => x.DeptCode == deptCode).FirstOrDefault();
                if (empId == dept.DeptHeadId || empId == dept.DelegateApproverId)
                {
                    reqlist = GetReq(deptCode, status);
                }
                else
                {
                    if (status == "All")
                    {
                        lst = entities.Requests.Where(r => r.EmpId == empId && (r.Status == "Submitted" ||
                        r.Status == "Approved" || r.Status == "Rejected" || r.Status == "Cancelled" || r.Status == "Fulfilled")).ToList();
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
                        lst = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).ToList();
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
        // done
        public static List<RequestVM> GetReq(string status)
        {
            List<RequestVM> reqlist = new List<RequestVM>();
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
        public static bool RemoveReq(int empId, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Request> reqlist = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).ToList();
                if (reqlist.Count > 0)
                {
                    for (int i = 0; i < reqlist.Count; i++)
                    {
                        if (reqlist[i].Status.Equals("Unsubmitted") || reqlist[i].Status.Equals("BookMarked"))
                        {
                            RequestDetailBL.removeAllReqDet(reqlist[i].ReqId);
                        }
                        else
                        {
                            reqlist[i].Status = "Cancelled";
                            reqlist[i].CancelledDateTime = DateTime.Now;
                            entities.SaveChanges();
                        }
                    }
                }
                return true;
            }
        }

        // remove request by reqId
        // done
        public static bool RemoveReq(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Request request = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                if (request != null)
                {
                    if (request.Status.Equals("Unsubmitted") || request.Status.Equals("Unsubmitted"))
                    {
                        RequestDetailBL.removeAllReqDet(reqId);
                    }
                    else
                    {
                        request.Status = "Cancelled";
                        request.CancelledDateTime = DateTime.Now;
                        entities.SaveChanges();
                    }
                }
                return true;
            }
        }

        //// submit request
        //// done
        //public static RequestVM SubmitReq(int empId, List<RequestDetailVM> reqDetList)
        //{
        //    // This is only to explain code steps at Web Api service
        //    // Call GetReq(empId, “Unsubmitted”)
        //    // Call UpdateReqDet(reqId, reqDet) for each reqDet in reqDetLst
        //    // Set reqDateTime for currReq object to DateTime.Now()
        //    // Set status for currReq to “Submitted”
        //    // Call UpdateReq(currReq) to persist at db
        //    // Return currReq object
        //    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
        //    {
        //        RequestVM request = new RequestVM();
        //        List<RequestVM> reqlist = GetReq(empId, "Unsubmitted");
        //        for (int i = 0; i < reqlist.Count; i++)
        //        {
        //            for (int j = 0; j < reqDetList.Count; j++)
        //            {
        //                if (reqlist[i].ReqId == reqDetList[j].ReqId)
        //                {
        //                    //RequestDetailVM rvm = RequestDetailBL.UpdateReqDet(reqDetList[j].ReqId, reqDetList[j]);
        //                    //RequestDetail rd = entities.RequestDetails.Where(r => r.ReqId == reqDetList[j].ReqId && r.ReqLineNo == reqDetList[j].ReqLineNo).FirstOrDefault();
        //                    List<RequestDetail> rdlist = entities.RequestDetails.ToList();
        //                    for (int k = 0; k < rdlist.Count; k++)
        //                    {
        //                        if (reqDetList[j].ReqId == rdlist[k].ReqId && reqDetList[j].ReqLineNo == rdlist[k].ReqLineNo)
        //                        {
        //                            rdlist[k].ItemCode = reqDetList[j].ItemCode;
        //                            rdlist[k].ReqQty = reqDetList[j].ReqQty;
        //                            rdlist[k].AwaitQty = reqDetList[j].AwaitQty;
        //                            rdlist[k].FulfilledQty = reqDetList[j].FulfilledQty;
        //                            entities.SaveChanges();
        //                        }
        //                    }
        //                }
        //            }
        //            reqlist[i].ReqDateTime = DateTime.Now;
        //            reqlist[i].Status = "Submitted";
        //            request = UpdateReq(reqlist[i]);
        //        }
        //        return request;
        //        //RequestVM r = new RequestVM();
        //        //List<RequestVM> reqlist = GetReq(empId, status);
        //        //for (int i = 0; i < reqlist.Count; i++)
        //        //{
        //        //    List<RequestDetail> rdlist = entities.RequestDetails.Where(rd => rd.ReqId == reqlist[i].ReqId).ToList();
        //        //    for (int j = 0; j < rdlist.Count; j++)
        //        //    {
        //        //        for (int k = 0; k < reqDetList.Count; k++)
        //        //        {
        //        //            if (rdlist[j].ReqId == reqDetList[k].ReqId && rdlist[j].ReqLineNo == reqDetList[k].ReqLineNo)
        //        //            {
        //        //                RequestDetailVM rvm = RequestDetailBL.UpdateReqDet(rdlist[j].ReqId, reqDetList[k]);
        //        //            }
        //        //        }
        //        //    }
        //        //    reqlist[i].ReqDateTime = DateTime.Now;
        //        //    reqlist[i].Status = "Submitted";
        //        //    r = UpdateReq(reqlist[i]);
        //        //}
        //        //return r;
        //    }
        //}

        // submit request
        // done, except email
        public static RequestVM SubmitReq(int reqId, List<RequestDetailVM> reqDetList)
        {
            // make requestId in reqDetList is the same as reqId
            RequestVM req = GetReq(reqId);
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                for (int i = 0; i < reqDetList.Count; i++)
                {
                    if (reqDetList[i].ReqId == reqId)
                    {
                        //// will call this method when UpdateReqDet is finished
                        //RequestDetailVM rd = RequestDetailBL.UpdateReqDet(reqId, reqDetList[i]);
                        //RequestDetail requestDetail = entities.RequestDetails.Where(rd => rd.ReqId == reqDetList[i].ReqId &&
                        //rd.ReqLineNo == reqDetList[i].ReqLineNo).FirstOrDefault();
                        //requestDetail.ReqQty = reqDetList[i].ReqQty;
                        //requestDetail.AwaitQty = reqDetList[i].AwaitQty;
                        //requestDetail.FulfilledQty = reqDetList[i].FulfilledQty;
                        //entities.SaveChanges();
                        List<RequestDetail> rdlist = entities.RequestDetails.ToList();
                        for (int j = 0; j < rdlist.Count; j++)
                        {
                            if (reqDetList[i].ReqId == rdlist[j].ReqId && reqDetList[i].ReqLineNo == rdlist[j].ReqLineNo)
                            {
                                rdlist[j].ReqQty = reqDetList[i].ReqQty;
                                rdlist[j].AwaitQty = reqDetList[i].AwaitQty;
                                rdlist[j].FulfilledQty = reqDetList[i].FulfilledQty;
                                entities.SaveChanges();
                            }
                        }
                    }
                }
                req.ReqDateTime = DateTime.Now;
                req.Status = "Submitted";
                req = UpdateReq(req);
            }

            int empId = req.EmpId;
            //// will call when method is completed
            // EmailBL.SendNewReqEmail(empId, req);
            NotificationBL.AddNewReqNotification(empId, req);
            // redirect to SubmittedRequestDetails page
            return req;
        }

        // update request
        // done
        public static RequestVM UpdateReq(RequestVM req)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                int reqId = req.ReqId;
                Request request = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                request.EmpId = req.EmpId;
                request.ApproverId = req.ApproverId;
                request.ApproverComment = req.ApproverComment;
                if (req.ReqDateTime != null && DateTime.Compare(req.ReqDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.ReqDateTime = req.ReqDateTime;
                if (req.ApprovedDateTime != null && DateTime.Compare(req.ApprovedDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.ApprovedDateTime = req.ApprovedDateTime;
                if (req.CancelledDateTime != null && DateTime.Compare(req.CancelledDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.CancelledDateTime = req.CancelledDateTime;
                if (req.FulfilledDateTime != null && DateTime.Compare(req.FulfilledDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.FulfilledDateTime = req.FulfilledDateTime;
                request.Status = req.Status;
                entities.SaveChanges();
            }
            return req;
        }

        // accept request
        // done
        public static void AcceptRequest(int reqId, int empId, string cmt)
        {
            // This is only to explain code steps at Web Api service
            // Call GetReq(empId, “Submitted”)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Add ApprovalDateTime as DateTime.Now()
            // Update Status as “Approved”

            List<RequestVM> reqlist = GetReq(empId, "Submitted");
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
            // send accept notification
            NotificationBL.AddAcptNotification(reqId);
            return;
        }

        // reject request
        // done
        public static void RejectRequest(int reqId, int empId, string cmt)
        {
            // This is only to explain code steps at Web Api service
            // Call GetReq(empId, “Submitted”)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Add ApprovalDateTime as DateTime.Now()
            // Update Status as “Rejected”

            List<RequestVM> reqlist = GetReq(empId, "Submitted");
            int toId;
            for (int i = 0; i < reqlist.Count; i++)
            {
                if (reqlist[i].ReqId == reqId)
                {
                    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                    {
                        Request req = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                        toId = req.EmpId;
                        req.ApproverId = empId;
                        req.ApproverComment = cmt;
                        req.ApprovedDateTime = DateTime.Now;
                        req.Status = "Rejected";
                        entities.SaveChanges();
                    }
                }
            }
            // send reject notification
            //NotificationBL.AddAcptNotification(reqId);

            // pending notification

            int fromId = empId;
            return;
        }

        // update fulfilled request status
        // included in "Accept Disbursed Items" use case, no need to implement
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

            //int openCount = 0;

            return;
        }
    }
}