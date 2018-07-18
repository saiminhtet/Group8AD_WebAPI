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
        // dummy code

        // add an adjustment
        // not dummy, revise
        public static AdjustmentVM AddAdj(AdjustmentVM adj)
        {
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    entities.Adjustments.Add(adj);
            //    entities.SaveChanges();
            //    AdjustmentVM adjustment = new AdjustmentVM()
            //    {
            //        VoucherNo = adj.VoucherNo,
            //        EmpId = adj.EmpId,
            //        DateTimeIssued = adj.DateTimeIssued,
            //        ItemCode = adj.ItemCode,
            //        Reason = adj.Reason,
            //        QtyChange = adj.QtyChange,
            //        Status = adj.Status,
            //        //ApproverId = adj.ApproverId,
            //        ApproverComment = adj.ApproverComment
            //    };
            //    return adjustment;
            //}

            //AdjustmentVM adjustment = new AdjustmentVM()
            //{
            //    VoucherNo = adj.VoucherNo,
            //    EmpId = adj.EmpId,
            //    DateTimeIssued = adj.DateTimeIssued,
            //    ItemCode = adj.ItemCode,
            //    Reason = adj.Reason,
            //    QtyChange = adj.QtyChange,
            //    Status = adj.Status,
            //    //ApproverId = adj.ApproverId,
            //    ApproverComment = adj.ApproverComment
            //};
            //return adjustment;

            return adj;
        }

        // get an adjustment by voucher number
        // done
        public static AdjustmentVM GetAdj(string voucherNo)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                AdjustmentVM adj = new AdjustmentVM();
                Adjustment adjustment = new Adjustment();
                adjustment = entities.Adjustments.Where(a => a.VoucherNo == voucherNo).FirstOrDefault();
                adj.VoucherNo = adjustment.VoucherNo;
                adj.EmpId = adjustment.EmpId;
                adj.DateTimeIssued = adjustment.DateTimeIssued;
                adj.ItemCode = adjustment.ItemCode;
                adj.Reason = adjustment.Reason;
                adj.QtyChange = adjustment.QtyChange;
                adj.Status = adjustment.Status;
                if (adjustment.ApproverId != null)
                    adj.ApproverId = (int)adjustment.ApproverId;
                else
                    adjustment.ApproverId = 0;
                adj.ApproverComment = adjustment.ApproverComment;
                return adj;
            }
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    adjustment = entities.Adjustments.Where(a => a.VoucherNo == voucherNo).Select(a => new AdjustmentVM()
            //    {
            //        VoucherNo = a.VoucherNo,
            //        EmpId = a.EmpId,
            //        DateTimeIssued = a.DateTimeIssued,
            //        ItemCode = a.ItemCode,
            //        Reason = a.Reason,
            //        QtyChange = a.QtyChange,
            //        Status = a.Status,
            //        //ApproverId = a.ApproverId,
            //        ApproverComment = a.ApproverComment
            //    }).First<AdjustmentVM>();
            //}
        }

        // get a list of adjustment by status
        // done
        public static List<AdjustmentVM> GetAdjList(string status)
        {
            List<AdjustmentVM> list = new List<AdjustmentVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Adjustment> adjlist = entities.Adjustments.Where(a => a.Status == status).ToList();
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
            return list;
        }

        // raise adjustment
        // dummy
        public static List<AdjustmentVM> RaiseAdjustments(int empId, List<AdjustmentVM> iList)
        {
            List<AdjustmentVM> adjlist = new List<AdjustmentVM>();
            ////dummy
            //using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            //{
            //    adjlist = entities.Adjustments.Select(a => new AdjustmentVM()
            //    {
            //        VoucherNo = a.VoucherNo,
            //        EmpId = a.EmpId,
            //        DateTimeIssued = a.DateTimeIssued,
            //        ItemCode = a.ItemCode,
            //        Reason = a.Reason,
            //        QtyChange = a.QtyChange,
            //        Status = a.Status,
            //        //ApproverId = a.ApproverId,
            //        ApproverComment = a.ApproverComment
            //    }).ToList<AdjustmentVM>();
            //}
            return adjlist;

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
        // not dummy, not tested
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
                notif.Type = "Adjustment rejected";
                notif.Content = cmt;
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
                //// will uncomment when email and notification service method is done
                // AdjApprNotification(fromEmpId, toEmpId, notification);
                // SendAdjApprEmail(empId, adjustment);
            }
            return;
        }

        // accept adjustment request
        // not dummy, not tested
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
                notif.Type = "Adjustment approved";
                notif.Content = cmt;
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
                //// will uncomment when email and notification service method is done
                // AdjApprNotification(fromEmpId, toEmpId, notification);
                // SendAdjApprEmail(empId, adjustment);
            }
            return;
        }
    }
}