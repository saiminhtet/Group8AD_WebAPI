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
        public static List<AdjustmentVM> GetAdj(string voucherNo)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Adjustment> adjList = entities.Adjustments.Where(a => a.VoucherNo == voucherNo).ToList();
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
                    if (adjList[i].ApproverId != null)
                        adj.ApproverId = (int)adjList[i].ApproverId;
                    else
                        adjList[i].ApproverId = 0;
                    adj.ApproverComment = adjList[i].ApproverComment;
                    avmList.Add(adj);
                }
                return avmList;
            }
        }

        // get a list of adjustment by status
        // done
        public static List<AdjustmentVM> GetAdjList(string status)
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
                        if (adjlist[i].ApproverId != null)
                            adj.ApproverId = (int)adjlist[i].ApproverId;
                        else
                            adjlist[i].ApproverId = 0;
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
                        if (adjlist[i].ApproverId != null)
                            adj.ApproverId = (int)adjlist[i].ApproverId;
                        else
                            adjlist[i].ApproverId = 0;
                        adj.ApproverComment = adjlist[i].ApproverComment;
                        list.Add(adj);
                    }
                }
            }
            return list;
        }

        // raise adjustment
        // done, except email
        public static bool RaiseAdjustments(int empId, List<ItemVM> iList)

        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                bool isRaised = false;
                string vNum = GenerateVoucherNo();
                double chgBck = 0;
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
                        a.ApproverComment = "";
                        entities.Adjustments.Add(a);
                        entities.SaveChanges();

                        if ((iList[i].TempQtyCheck - iList[i].Balance) < 0)
                        {
                            chgBck = chgBck + a.QtyChange * -1 * iList[i].Price1;
                        }
                    }
                }

                Employee emp = new Employee();
                if (chgBck >= 250)
                {
                    emp = entities.Employees.Where(x => x.Role.Equals("Store Manager")).FirstOrDefault();
                }
                else
                {
                    emp = entities.Employees.Where(x => x.Role.Equals("Store Supervisor")).FirstOrDefault();
                }
                if (chgBck > 0) isRaised = true;
                int fromEmpIdA = empId;
                int toEmpIdA = emp.EmpId;
                string typeA = "Adjustment Request";
                string contentA = vNum + " has been raised";
                NotificationBL.AddNewNotification(fromEmpIdA, toEmpIdA, typeA, contentA);

                List<Employee> clerk = entities.Employees.Where(x => x.Role.Equals("Store Clerk")).ToList();
                int fromEmpIdL = empId;
                string typeL = "Low Stock";
                string contentL = "In a recent stationery stock check, there are some items with balance below reorder level.";
                for (int i = 0; i < clerk.Count; i++)
                {
                    int toEmpIdL = clerk[i].EmpId;
                    NotificationBL.AddNewNotification(fromEmpIdL, toEmpIdL, typeL, contentL);
                }

                // will implement when Email service method is done
                // send email to clerk
                // EmailBL.SendAdjReqEmail(empId, adjlist);
                //// send email to manager
                // EmailBL.SendAdjReqEmail(104, adjlist);
                //// send email to supervisor
                // EmailBL.SendAdjReqEmail(105, adjlist);
                return isRaised;
            }

            // dummy codes
            //List<Adjustment> adjList = new List<Adjustment>();
            //foreach (Item i in iList)
            //  if (TempQtyChk - i.Balance > 0) {
            //      Adjustment a = new Adjustment();
            //      a.EmpId = empId;
            //      a.DateTimeIssued = DateTime.Now();
            //      a.ItemCode = i.ItemCode;
            //      int index = iList.FindIndex(x => x.ItemCode.Equals(i.ItemCode));
            //      a.Reason = reasonList[index]; // Assumes reasonList is populated from page and is as long as iList 
            //      a.QtyChange = TempQtyChk - i.Balance;
            //      a.Status = “Submitted”
            //      adjList.Add(a)
            //      ctx.Adjustment.Add(a);
            //      ctx.SaveChanges();
            //      double chgVal = a.QtyChange * i.Price1
            //      if (chgVal >= 250)
            //          Notify Manager
            //      else
            //          Notify Supervisor
            //  }
            //  Call AddLowStockNotification(empId, i);
            //loop through each i in iList
            //Send email notification to all clerks, supervisor and manager with SendAdjReqEmail(empId, adjList)
            //Item controller displays toast message on StockTake page

        }

        // reject adjustment request
        // done, except email
        public static void RejectRequest(string voucherNo, int empId, string cmt)
        {
            // Call GetAdj(voucherNo)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Update Status as “Rejected”
            // SaveChanges()
            // Send Notification to Clerk
            // Send Email to Clerk

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Adjustment adjustment = entities.Adjustments.Where(a => a.VoucherNo == voucherNo).FirstOrDefault();
                adjustment.ApproverId = empId;
                adjustment.ApproverComment = cmt;
                adjustment.Status = "Rejected";
                entities.SaveChanges();

                int fromEmpId = empId;
                int toEmpId = adjustment.EmpId;
                Notification notif = new Notification();
                notif.NotificationDateTime = DateTime.Now;
                notif.FromEmp = fromEmpId;
                notif.ToEmp = toEmpId;
                notif.RouteUri = "";
                notif.Type = "Adjustment Request";
                notif.Content = "Rejected: " + cmt;
                notif.IsRead = false;
                entities.Notifications.Add(notif);

                List<Notification> lst = entities.Notifications.ToList();
                Notification n = lst[lst.Count - 1];

                NotificationVM notification = new NotificationVM();
                notification.NotificationId = n.NotificationId;
                notification.NotificationDateTime = n.NotificationDateTime;
                notification.FromEmp = n.FromEmp;
                notification.ToEmp = n.ToEmp;
                notification.RouteUri = n.RouteUri;
                notification.Type = n.Type;
                notification.Content = n.Content;
                notification.IsRead = n.IsRead;

                NotificationBL.AdjApprNotification(fromEmpId, toEmpId, notification);
                //// will uncomment when email service method is done
                // EmailBL.SendAdjApprEmail(empId, adjustment);
            }
            return;
        }

        // accept adjustment request
        // done, except email
        public static void AcceptRequest(string voucherNo, int empId, string cmt)
        {
            // Call GetAdj(empId)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Update Status as “Approved”
            // SaveChanges()
            // Add Transaction for each Adjustment
            // Send Notification to Clerk
            // Send Email to Clerk

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Adjustment adjustment = entities.Adjustments.Where(a => a.VoucherNo == voucherNo).FirstOrDefault();
                adjustment.ApproverId = empId;
                adjustment.ApproverComment = cmt;
                adjustment.Status = "Approved";
                entities.SaveChanges();

                int fromEmpId = empId;
                int toEmpId = adjustment.EmpId;
                Notification notif = new Notification();
                notif.NotificationDateTime = DateTime.Now;
                notif.FromEmp = fromEmpId;
                notif.ToEmp = toEmpId;
                notif.RouteUri = "";
                notif.Type = "Adjustment Request";
                notif.Content = "Approved: " + cmt;
                notif.IsRead = false;
                entities.Notifications.Add(notif);

                List<Notification> lst = entities.Notifications.ToList();
                Notification n = lst[lst.Count - 1];

                NotificationVM notification = new NotificationVM();
                notification.NotificationId = n.NotificationId;
                notification.NotificationDateTime = n.NotificationDateTime;
                notification.FromEmp = n.FromEmp;
                notification.ToEmp = n.ToEmp;
                notification.RouteUri = n.RouteUri;
                notification.Type = n.Type;
                notification.Content = n.Content;
                notification.IsRead = n.IsRead;

                NotificationBL.AdjApprNotification(fromEmpId, toEmpId, notification);
                //// will uncomment when email and notification service method is done
                // EmailBL.SendAdjApprEmail(empId, adjustment);
            }
            return;
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