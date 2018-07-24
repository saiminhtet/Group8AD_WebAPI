using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public class TransactionBL
    {
        // add a transaction
        // done
        public static TransactionVM AddTran(TransactionVM t)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                Transaction tran = new Transaction();
                tran.TranDateTime = t.TranDateTime;
                tran.ItemCode = t.ItemCode;
                tran.QtyChange = t.QtyChange;
                tran.UnitPrice = t.UnitPrice;
                tran.Desc = t.Desc;
                tran.DeptCode = t.DeptCode;
                tran.SuppCode = t.SuppCode;
                tran.VoucherNo = t.VoucherNo;

                entities.Transactions.Add(tran);
                entities.SaveChanges();

                List<Transaction> translist = entities.Transactions.ToList();
                int transId = translist[translist.Count - 1].TranId;
                t.TranId = transId;
                return t;
            }
        }
    }
}