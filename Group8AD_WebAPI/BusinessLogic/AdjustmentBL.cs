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
        // not dummy
        public static AdjustmentVM GetAdj(string voucherNo)
        {
            AdjustmentVM adjustment = new AdjustmentVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                adjustment = entities.Adjustments.Where(a => a.VoucherNo == voucherNo).Select(a => new AdjustmentVM()
                {
                    VoucherNo = a.VoucherNo,
                    EmpId = a.EmpId,
                    DateTimeIssued = a.DateTimeIssued,
                    ItemCode = a.ItemCode,
                    Reason = a.Reason,
                    QtyChange = a.QtyChange,
                    Status = a.Status,
                    //ApproverId = a.ApproverId,
                    ApproverComment = a.ApproverComment
                }).First<AdjustmentVM>();
            }
            return adjustment;
        }

        // get a list of adjustment by status
        // not dummy
        public static List<AdjustmentVM> GetAdjList(string status)
        {
            List<AdjustmentVM> adjlist = new List<AdjustmentVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                adjlist = entities.Adjustments.Where(a => a.Status == status).Select(a => new AdjustmentVM()
                {
                    VoucherNo = a.VoucherNo,
                    EmpId = a.EmpId,
                    DateTimeIssued = a.DateTimeIssued,
                    ItemCode = a.ItemCode,
                    Reason = a.Reason,
                    QtyChange = a.QtyChange,
                    Status = a.Status,
                    //ApproverId = a.ApproverId,
                    ApproverComment = a.ApproverComment
                }).ToList<AdjustmentVM>();
            }
            return adjlist;
        }

        // raise adjustment
        // dummy
        public static List<AdjustmentVM> RaiseAdjustments(int empId, List<AdjustmentVM> iList)
        {
            List<AdjustmentVM> adjlist = new List<AdjustmentVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                adjlist = entities.Adjustments.Select(a => new AdjustmentVM()
                {
                    VoucherNo = a.VoucherNo,
                    EmpId = a.EmpId,
                    DateTimeIssued = a.DateTimeIssued,
                    ItemCode = a.ItemCode,
                    Reason = a.Reason,
                    QtyChange = a.QtyChange,
                    Status = a.Status,
                    //ApproverId = a.ApproverId,
                    ApproverComment = a.ApproverComment
                }).ToList<AdjustmentVM>();
            }
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
        // dummy
        public static void RejectRequest(string voucherNo, int empId, string cmt)
        {
            // Call GetAdj(voucherNo)
            // Update ApproverId as empId
            // Add ApproverComment as cmt
            // Update Status as “Rejected”
            // SaveChanges()
            // Send Notification to Clerk
            // Send Email to Clerk
            return;
        }

        // accept adjustment request
        // dummy
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
            return;
        }
    }
}