using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public class AdjustmentBL
    {
        // add an adjustment
        // done
        public static List<AdjustmentVM> AddAdj(List<AdjustmentVM> adjList)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string voucherNo = GenerateVoucherNo();
                for (int i = 0; i < adjList.Count; i++)
                {
                    Adjustment adj = new Adjustment();
                    adj.VoucherNo = voucherNo;
                    adjList[i].VoucherNo = voucherNo;
                    adj.EmpId = adjList[i].EmpId;
                    adj.DateTimeIssued = adjList[i].DateTimeIssued;
                    adj.ItemCode = adjList[i].ItemCode;
                    adj.Reason = adjList[i].Reason;
                    adj.QtyChange = adjList[i].QtyChange;
                    adj.Status = adjList[i].Status;
                    adj.ApproverId = adjList[i].ApproverId;
                    adj.ApproverComment = adjList[i].ApproverComment;
                    entities.Adjustments.Add(adj);
                    entities.SaveChanges();
                }
                return adjList;
            }
        }

        // get an adjustment by voucher number
        // done
        public static List<AdjustmentVM> GetAdjListByVoucherNo(string voucherNo)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Adjustment> adjList = entities.Adjustments.Where(a => a.VoucherNo.Equals(voucherNo)).ToList();
                List<AdjustmentVM> avmList = new List<AdjustmentVM>();
                for (int i = 0; i < adjList.Count; i++)
                {
                    AdjustmentVM adj = new AdjustmentVM();
                    adj.VoucherNo = adjList[i].VoucherNo;
                    adj.EmpId = adjList[i].EmpId;
                    adj.DateTimeIssued = adjList[i].DateTimeIssued;
                    adj.ItemCode = adjList[i].ItemCode;
                    adj.Reason = adjList[i].Reason;
                    adj.QtyChange = adjList[i].QtyChange;
                    adj.Status = adjList[i].Status;
                    Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                    if (adjList[i].ApproverId != null)
                        adj.ApproverId = (int)adjList[i].ApproverId;
                    else
                        adj.ApproverId = emp.EmpId;
                    adj.ApproverComment = adjList[i].ApproverComment;
                    avmList.Add(adj);
                }
                return avmList;
            }
        }

        // get an adjustment by voucher number and approverId
        // done
        public static List<AdjustmentVM> GetAdjList(string voucherNo, int approverId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<AdjustmentVM> avmList = new List<AdjustmentVM>();
                if (voucherNo == null || voucherNo.Equals("")) avmList = GetAdjListByApproverId(approverId);
                else if (approverId.Equals("") || approverId.Equals(null)) avmList = GetAdjListByVoucherNo(voucherNo);
                else
                {
                    List<Adjustment> adjList = entities.Adjustments.Where(a => a.VoucherNo.Equals(voucherNo) && a.ApproverId == approverId).ToList();
                    avmList = new List<AdjustmentVM>();
                    for (int i = 0; i < adjList.Count; i++)
                    {
                        AdjustmentVM adj = new AdjustmentVM();
                        adj.VoucherNo = adjList[i].VoucherNo;
                        adj.EmpId = adjList[i].EmpId;
                        adj.DateTimeIssued = adjList[i].DateTimeIssued;
                        adj.ItemCode = adjList[i].ItemCode;
                        adj.Reason = adjList[i].Reason;
                        adj.QtyChange = adjList[i].QtyChange;
                        adj.Status = adjList[i].Status;
                        Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                        if (adjList[i].ApproverId != null)
                            adj.ApproverId = (int)adjList[i].ApproverId;
                        else
                            adj.ApproverId = emp.EmpId;
                        adj.ApproverComment = adjList[i].ApproverComment;
                        avmList.Add(adj);
                    }
                }                
                return avmList;
            }
        }

        // get a list of adjustment by status
        // done
        public static List<AdjustmentVM> GetAdjListByStatus(string status)
        {
            List<AdjustmentVM> list = new List<AdjustmentVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Adjustment> adjlist = new List<Adjustment>();
                if (status.Equals("All"))
                {
                    adjlist = entities.Adjustments.Where(a => a.Status.Equals("Submitted") ||
                    a.Status.Equals("Approved") || a.Status.Equals("Rejected")).ToList();
                    for (int i = 0; i < adjlist.Count; i++)
                    {
                        AdjustmentVM adj = new AdjustmentVM();
                        adj.VoucherNo = adjlist[i].VoucherNo;
                        adj.EmpId = adjlist[i].EmpId;
                        adj.DateTimeIssued = adjlist[i].DateTimeIssued;
                        adj.ItemCode = adjlist[i].ItemCode;
                        adj.Reason = adjlist[i].Reason;
                        adj.QtyChange = adjlist[i].QtyChange;
                        adj.Status = adjlist[i].Status;
                        Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                        if (adjlist[i].ApproverId != null)
                            adj.ApproverId = (int)adjlist[i].ApproverId;
                        else
                            adj.ApproverId = emp.EmpId;
                        adj.ApproverComment = adjlist[i].ApproverComment;
                        list.Add(adj);
                    }
                }
                else
                {
                    adjlist = entities.Adjustments.Where(a => a.Status == status).ToList();
                    for (int i = 0; i < adjlist.Count; i++)
                    {
                        AdjustmentVM adj = new AdjustmentVM();
                        adj.VoucherNo = adjlist[i].VoucherNo;
                        adj.EmpId = adjlist[i].EmpId;
                        adj.DateTimeIssued = adjlist[i].DateTimeIssued;
                        adj.ItemCode = adjlist[i].ItemCode;
                        adj.Reason = adjlist[i].Reason;
                        adj.QtyChange = adjlist[i].QtyChange;
                        adj.Status = adjlist[i].Status;
                        Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                        if (adjlist[i].ApproverId != null)
                            adj.ApproverId = (int)adjlist[i].ApproverId;
                        else
                            adj.ApproverId = emp.EmpId;
                        adj.ApproverComment = adjlist[i].ApproverComment;
                        list.Add(adj);
                    }
                }
            }
            return list;
        }

        // get a list of adjustment by status
        // done
        public static List<AdjustmentVM> GetAdjList(string voucherNo, string status)
        {
            List<AdjustmentVM> list = new List<AdjustmentVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                if (voucherNo == null || voucherNo.Equals("")) list = GetAdjListByStatus(status);
                else if (status == null || status.Equals("")) list = GetAdjListByVoucherNo(voucherNo);
                else
                {

                    List<Adjustment> adjlist = new List<Adjustment>();
                    if (status.Equals("All"))
                    {
                        adjlist = entities.Adjustments.Where(a => a.VoucherNo.Equals(voucherNo) && (a.Status.Equals("Submitted") ||
                        a.Status.Equals("Approved") || a.Status.Equals("Rejected"))).ToList();
                        for (int i = 0; i < adjlist.Count; i++)
                        {
                            AdjustmentVM adj = new AdjustmentVM();
                            adj.VoucherNo = adjlist[i].VoucherNo;
                            adj.EmpId = adjlist[i].EmpId;
                            adj.DateTimeIssued = adjlist[i].DateTimeIssued;
                            adj.ItemCode = adjlist[i].ItemCode;
                            adj.Reason = adjlist[i].Reason;
                            adj.QtyChange = adjlist[i].QtyChange;
                            adj.Status = adjlist[i].Status;
                            Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                            if (adjlist[i].ApproverId != null)
                                adj.ApproverId = (int)adjlist[i].ApproverId;
                            else
                                adj.ApproverId = emp.EmpId;
                            adj.ApproverComment = adjlist[i].ApproverComment;
                            list.Add(adj);
                        }
                    }
                    else
                    {
                        adjlist = entities.Adjustments.Where(a => a.Status.Equals(status) && a.VoucherNo.Equals(voucherNo)).ToList();
                        for (int i = 0; i < adjlist.Count; i++)
                        {
                            AdjustmentVM adj = new AdjustmentVM();
                            adj.VoucherNo = adjlist[i].VoucherNo;
                            adj.EmpId = adjlist[i].EmpId;
                            adj.DateTimeIssued = adjlist[i].DateTimeIssued;
                            adj.ItemCode = adjlist[i].ItemCode;
                            adj.Reason = adjlist[i].Reason;
                            adj.QtyChange = adjlist[i].QtyChange;
                            adj.Status = adjlist[i].Status;
                            Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                            if (adjlist[i].ApproverId != null)
                                adj.ApproverId = (int)adjlist[i].ApproverId;
                            else
                                adj.ApproverId = emp.EmpId;
                            adj.ApproverComment = adjlist[i].ApproverComment;
                            list.Add(adj);
                        }
                    }
                }
                return list;
            }
        }

        // get adjustment by status and approverId
        // done
        public static List<AdjustmentVM> GetAdjListByStatusApproverId(string status, int approverId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<AdjustmentVM> avmList = new List<AdjustmentVM>();
                if (status == null || status.Equals("")) avmList = GetAdjListByApproverId(approverId);
                else if (approverId.Equals("") || approverId.Equals(null)) avmList = GetAdjListByStatus(status);
                else
                {
                    List<Adjustment> adjList = new List<Adjustment>();
                    if (status.Equals("All"))
                    {
                        adjList = entities.Adjustments.Where(a => (a.Status.Equals("Submitted") ||
                        a.Status.Equals("Approved") || a.Status.Equals("Rejected")) && a.ApproverId == approverId).ToList();
                        avmList = new List<AdjustmentVM>();
                        for (int i = 0; i < adjList.Count; i++)
                        {
                            AdjustmentVM adj = new AdjustmentVM();
                            adj.VoucherNo = adjList[i].VoucherNo;
                            adj.EmpId = adjList[i].EmpId;
                            adj.DateTimeIssued = adjList[i].DateTimeIssued;
                            adj.ItemCode = adjList[i].ItemCode;
                            adj.Reason = adjList[i].Reason;
                            adj.QtyChange = adjList[i].QtyChange;
                            adj.Status = adjList[i].Status;
                            Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                            if (adjList[i].ApproverId != null)
                                adj.ApproverId = (int)adjList[i].ApproverId;
                            else
                                adj.ApproverId = emp.EmpId;
                            adj.ApproverComment = adjList[i].ApproverComment;
                            avmList.Add(adj);
                        }
                    }
                    else
                    {
                        adjList = entities.Adjustments.Where(a => a.Status.Equals(status) && a.ApproverId == approverId).ToList();
                        avmList = new List<AdjustmentVM>();
                        for (int i = 0; i < adjList.Count; i++)
                        {
                            AdjustmentVM adj = new AdjustmentVM();
                            adj.VoucherNo = adjList[i].VoucherNo;
                            adj.EmpId = adjList[i].EmpId;
                            adj.DateTimeIssued = adjList[i].DateTimeIssued;
                            adj.ItemCode = adjList[i].ItemCode;
                            adj.Reason = adjList[i].Reason;
                            adj.QtyChange = adjList[i].QtyChange;
                            adj.Status = adjList[i].Status;
                            Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                            if (adjList[i].ApproverId != null)
                                adj.ApproverId = (int)adjList[i].ApproverId;
                            else
                                adj.ApproverId = emp.EmpId;
                            adj.ApproverComment = adjList[i].ApproverComment;
                            avmList.Add(adj);
                        }
                    }                
                }
                return avmList;
            }
        }

        // get adjustment by approverId
        // done
        public static List<AdjustmentVM> GetAdjListByApproverId(int approverId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Adjustment> adjList = entities.Adjustments.Where(a => a.ApproverId == approverId).ToList();
                List<AdjustmentVM> avmList = new List<AdjustmentVM>();
                for (int i = 0; i < adjList.Count; i++)
                {
                    AdjustmentVM adj = new AdjustmentVM();
                    adj.VoucherNo = adjList[i].VoucherNo;
                    adj.EmpId = adjList[i].EmpId;
                    adj.DateTimeIssued = adjList[i].DateTimeIssued;
                    adj.ItemCode = adjList[i].ItemCode;
                    adj.Reason = adjList[i].Reason;
                    adj.QtyChange = adjList[i].QtyChange;
                    adj.Status = adjList[i].Status;
                    Employee emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                    if (adjList[i].ApproverId != null)
                        adj.ApproverId = (int)adjList[i].ApproverId;
                    else
                        adj.ApproverId = emp.EmpId;
                    adj.ApproverComment = adjList[i].ApproverComment;
                    avmList.Add(adj);
                }
                return avmList;
            }
        }

        // raise adjustment
        // done
        public static bool RaiseAdjustments(int empId, List<ItemVM> iList)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                // for email
                List<AdjustmentVM> adjList = new List<AdjustmentVM>();

                string vNum = GenerateVoucherNo();
                for (int i = 0; i < iList.Count; i++)
                {
                    if ((iList[i].TempQtyCheck - iList[i].Balance) != 0)
                    {
                        Adjustment a = new Adjustment();
                        a.VoucherNo = vNum;
                        a.EmpId = empId;
                        a.DateTimeIssued = DateTime.Now;
                        a.ItemCode = iList[i].ItemCode;
                        a.Reason = iList[i].TempReason;
                        a.QtyChange = (int)iList[i].TempQtyCheck - iList[i].Balance;
                        a.Status = "Submitted";

                        Employee emp = new Employee();
                        emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                        double chgBck = a.QtyChange * iList[i].Price1;
                        if (a.QtyChange < 0)
                        {
                            chgBck = chgBck * -1;
                        }
                        if (chgBck >= 250)
                        {
                            emp = entities.Employees.Where(x => x.Role.Equals("Store Manager")).FirstOrDefault();
                        }
                        a.ApproverId = emp.EmpId;

                        a.ApproverComment = "";
                        entities.Adjustments.Add(a);
                        entities.SaveChanges();

                        // for email
                        AdjustmentVM adj = new AdjustmentVM();
                        adj.VoucherNo = adjList[i].VoucherNo;
                        adj.EmpId = adjList[i].EmpId;
                        adj.DateTimeIssued = adjList[i].DateTimeIssued;
                        adj.ItemCode = adjList[i].ItemCode;
                        adj.Reason = adjList[i].Reason;
                        adj.QtyChange = adjList[i].QtyChange;
                        adj.Status = adjList[i].Status;
                        adj.ApproverId = (int)adjList[i].ApproverId;
                        adj.ApproverComment = adjList[i].ApproverComment;
                        adjList.Add(adj);

                        int fromEmpIdA = empId;
                        int toEmpIdA = emp.EmpId;
                        string typeA = "Adjustment Request";
                        string contentA = vNum + " has been raised";
                        NotificationBL.AddNewNotification(fromEmpIdA, toEmpIdA, typeA, contentA);
                    }
                }

                // for email
                EmailBL.SendAdjReqEmail(empId, adjList);

                return true;
            }
        }

        // reject adjustment request
        // done, except email
        public static bool RejectRequest(string voucherNo, int empId, string cmt)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                // for email
                List<AdjustmentVM> adjListEmail = new List<AdjustmentVM>();

                int toId = 0;
                List<Adjustment> adjList = entities.Adjustments.Where(a => a.VoucherNo.Equals(voucherNo)).ToList();
                for (int i = 0; i < adjList.Count; i++)
                {
                    if (adjList[i].ApproverId == empId)
                    {
                        adjList[i].ApproverComment = cmt;
                        adjList[i].Status = "Rejected";

                        int adjRaiseEmpId = adjList[i].EmpId;
                        Employee adjRaiseEmp = entities.Employees.Where(x => x.EmpId == adjRaiseEmpId).FirstOrDefault();
                        if (adjRaiseEmp.Role.Equals("Store Clerk"))
                        {
                            toId = adjRaiseEmpId;
                        }
                        else
                        {
                            toId = entities.Employees.Where(x => x.Role.Equals("Store Clerk")).FirstOrDefault().EmpId;
                        }

                        // for email
                        AdjustmentVM adj = new AdjustmentVM();
                        adj.VoucherNo = adjList[i].VoucherNo;
                        adj.EmpId = adjList[i].EmpId;
                        adj.DateTimeIssued = adjList[i].DateTimeIssued;
                        adj.ItemCode = adjList[i].ItemCode;
                        adj.Reason = adjList[i].Reason;
                        adj.QtyChange = adjList[i].QtyChange;
                        adj.Status = adjList[i].Status;
                        adj.ApproverId = (int)adjList[i].ApproverId;
                        adj.ApproverComment = adjList[i].ApproverComment;
                        adjListEmail.Add(adj);
                    }
                }
                entities.SaveChanges();

                int fromEmpId = empId;
                int toEmpId = toId;
                string type = "Adjustment Request";
                string content = voucherNo + " has been rejected: Please review quantities";

                NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);

                // for email
                EmailBL.SendAdjApprEmail(toId, adjListEmail);

                return true;
            }
        }

        // accept adjustment request
        // done, except email
        public static bool AcceptRequest(string voucherNo, int empId, string cmt)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                // for email
                List<AdjustmentVM> adjListEmail = new List<AdjustmentVM>();

                int toId = 0;
                List<Adjustment> adjList = entities.Adjustments.Where(a => a.VoucherNo.Equals(voucherNo)).ToList();
                for (int i = 0; i < adjList.Count; i++)
                {
                    if (adjList[i].ApproverId == empId)
                    {
                        adjList[i].ApproverComment = cmt;
                        adjList[i].Status = "Approved";

                        int adjRaiseEmpId = adjList[i].EmpId;
                        Employee adjRaiseEmp = entities.Employees.Where(x => x.EmpId == adjRaiseEmpId).FirstOrDefault();
                        if (adjRaiseEmp.Role.Equals("Store Clerk"))
                        {
                            toId = adjRaiseEmpId;
                        }
                        else
                        {
                            toId = entities.Employees.Where(x => x.Role.Equals("Store Clerk")).FirstOrDefault().EmpId;
                        }

                        // for email
                        AdjustmentVM adj = new AdjustmentVM();
                        adj.VoucherNo = adjList[i].VoucherNo;
                        adj.EmpId = adjList[i].EmpId;
                        adj.DateTimeIssued = adjList[i].DateTimeIssued;
                        adj.ItemCode = adjList[i].ItemCode;
                        adj.Reason = adjList[i].Reason;
                        adj.QtyChange = adjList[i].QtyChange;
                        adj.Status = adjList[i].Status;
                        adj.ApproverId = (int)adjList[i].ApproverId;
                        adj.ApproverComment = adjList[i].ApproverComment;
                        adjListEmail.Add(adj);
                    }
                }
                entities.SaveChanges();

                int fromEmpId = empId;
                int toEmpId = toId;
                string type = "Adjustment Request";
                string content = voucherNo + " has been approved: No comment";

                NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);

                // for email
                EmailBL.SendAdjApprEmail(toId, adjListEmail);

                return true;
            }
        }

        public static string GenerateVoucherNo()
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string year = DateTime.Now.Year.ToString();
                List<Adjustment> adjlist = entities.Adjustments.Where(a => a.VoucherNo.Substring(0, 4) == year).ToList();
                int index = Int32.Parse(adjlist[adjlist.Count - 1].VoucherNo.Substring(5, 5));
                index = index + 1;
                string number = String.Format("{0:00000}", index);
                string voucherNo = year + "/" + number;
                return voucherNo;
            }
        }
    }
}