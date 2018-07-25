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
        public static AdjustmentVM AddAdj(AdjustmentVM adj)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Adjustment a = new Adjustment();
                a.VoucherNo = GenerateVoucherNo();
                a.EmpId = adj.EmpId;
                a.DateTimeIssued = adj.DateTimeIssued;
                a.ItemCode = adj.ItemCode;
                a.Reason = adj.Reason;
                a.QtyChange = adj.QtyChange;
                a.Status = adj.Status;
                a.ApproverId = adj.ApproverId;
                a.ApproverComment = adj.ApproverComment;

                entities.Adjustments.Add(a);
                entities.SaveChanges();

                List<Adjustment> adjList = entities.Adjustments.ToList();

                AdjustmentVM avm = new AdjustmentVM();
                avm.VoucherNo = adjList[adjList.Count - 1].VoucherNo;
                avm.EmpId = adjList[adjList.Count - 1].EmpId;
                avm.DateTimeIssued = adjList[adjList.Count - 1].DateTimeIssued;
                avm.ItemCode = adjList[adjList.Count - 1].ItemCode;
                avm.Reason = adjList[adjList.Count - 1].Reason;
                avm.QtyChange = adjList[adjList.Count - 1].QtyChange;
                avm.Status = adjList[adjList.Count - 1].Status;
                if (adjList[adjList.Count - 1].ApproverId != null)
                    avm.ApproverId = (int)adjList[adjList.Count - 1].ApproverId;
                avm.ApproverComment = adjList[adjList.Count - 1].ApproverComment;
                return avm;
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
                string vNum = GenerateVoucherNo();
                List<AdjustmentVM> adjlist1 = new List<AdjustmentVM>();
                List<AdjustmentVM> adjlist2 = new List<AdjustmentVM>();
                for (int i = 0; i < iList.Count; i++)
                {
                    if ((iList[i].TempQtyCheck - iList[i].Balance) != 0)
                    {
                        AdjustmentVM avm = new AdjustmentVM();
                        Adjustment a = new Adjustment();

                        a.VoucherNo = vNum;
                        a.EmpId = empId;
                        a.DateTimeIssued = DateTime.Now;
                        a.ItemCode = iList[i].ItemCode;
                        a.Reason = iList[i].TempReason;

                        // a.Reason = "";

                        a.QtyChange = (int)iList[i].TempQtyCheck - iList[i].Balance;
                        a.Status = "Submitted";
                        a.ApproverComment = "";
                        entities.Adjustments.Add(a);
                        entities.SaveChanges();
                        List<Adjustment> alist = entities.Adjustments.ToList();
                        Adjustment temp = alist[alist.Count - 1];
                        avm.VoucherNo = temp.VoucherNo;
                        avm.EmpId = temp.EmpId;
                        avm.DateTimeIssued = temp.DateTimeIssued;
                        avm.ItemCode = temp.ItemCode;
                        avm.Reason = temp.Reason;
                        avm.QtyChange = temp.QtyChange;
                        avm.Status = temp.Status;
                        avm.ApproverComment = temp.ApproverComment;


                        double chgVal = a.QtyChange * (double)iList[i].Price1;
                        if (chgVal >= 250)
                        {
                            adjlist1.Add(avm);
                        }
                        else
                        {
                            adjlist2.Add(avm);
                        }

                        //Item item = new Item();
                        //item.ItemCode = iList[i].ItemCode;
                        //item.Cat = iList[i].Cat;
                        //item.Desc = iList[i].Desc;
                        //item.Location = iList[i].Location;
                        //item.UOM = iList[i].UOM;
                        //item.IsActive = iList[i].IsActive;
                        //item.Balance = iList[i].Balance;
                        //item.ReorderLevel = iList[i].ReorderLevel;
                        //item.ReorderQty = iList[i].ReorderQty;
                        //item.TempQtyDisb = iList[i].TempQtyDisb;
                        //item.TempQtyCheck = iList[i].TempQtyCheck;
                        //item.SuppCode1 = iList[i].SuppCode1;
                        //item.Price1 = iList[i].Price1;
                        //item.SuppCode2 = iList[i].SuppCode2;
                        //item.Price2 = iList[i].Price2;
                        //item.SuppCode3 = iList[i].SuppCode3;
                        //item.Price3 = iList[i].Price3;
                        //NotificationBL.AddLowStkNotification(empId, item);
                    }
                }

                // SEND NOTIFICATION FOR SUPERVISOR
                Notification notif = new Notification();
                notif.NotificationDateTime = DateTime.Now;
                notif.FromEmp = empId;
                notif.ToEmp = 105; // *********************HARD CODED********************************
                notif.RouteUri = "";
                notif.Type = "Adjustment Request";
                notif.Content = "Submitted";
                notif.IsRead = false;
                entities.Notifications.Add(notif);
                entities.SaveChanges();

                List<Notification> nlist = entities.Notifications.ToList();
                Notification tempNotif = nlist[nlist.Count - 1];
                NotificationVM nvm = new NotificationVM();
                nvm.NotificationId = tempNotif.NotificationId;
                nvm.NotificationDateTime = tempNotif.NotificationDateTime;
                nvm.FromEmp = tempNotif.FromEmp;
                nvm.ToEmp = tempNotif.ToEmp;
                nvm.RouteUri = tempNotif.RouteUri;
                nvm.Type = tempNotif.Type;
                nvm.Content = tempNotif.Content;
                nvm.IsRead = tempNotif.IsRead;
                NotificationBL.NotifySupervisor(nvm);

                // SEND NOTIFICATION FOR MANAGER
                notif = new Notification();
                notif.NotificationDateTime = DateTime.Now;
                notif.FromEmp = empId;
                notif.ToEmp = 104; // *********************HARD CODED********************************
                notif.RouteUri = "";
                notif.Type = "Adjustment Request";
                notif.Content = "Submitted";
                notif.IsRead = false;
                entities.Notifications.Add(notif);
                entities.SaveChanges();

                nlist = entities.Notifications.ToList();
                tempNotif = nlist[nlist.Count - 1];
                nvm = new NotificationVM();
                nvm.NotificationId = tempNotif.NotificationId;
                nvm.NotificationDateTime = tempNotif.NotificationDateTime;
                nvm.FromEmp = tempNotif.FromEmp;
                nvm.ToEmp = tempNotif.ToEmp;
                nvm.RouteUri = tempNotif.RouteUri;
                nvm.Type = tempNotif.Type;
                nvm.Content = tempNotif.Content;
                nvm.IsRead = tempNotif.IsRead;
                NotificationBL.NotifyManager(nvm);

                // will implement when Email service method is done
                // send email to clerk
                // EmailBL.SendAdjReqEmail(empId, adjlist);
                //// send email to manager
                // EmailBL.SendAdjReqEmail(104, adjlist);
                //// send email to supervisor

                //SendAdjReqEmail(105, adjlist);
                return true;

                // EmailBL.SendAdjReqEmail(105, adjlist);

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