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
        // dummy code

        // not dummy, difficult to pass in Transaction object to test
        // add a transaction
        public static TransactionVM AddTran(Transaction t)
        {
            TransactionVM transaction = new TransactionVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                entities.Transactions.Add(t);
                entities.SaveChanges();

                transaction.TranId = t.TranId;
                transaction.TranDateTime = t.TranDateTime;
                transaction.ItemCode = t.ItemCode;
                transaction.QtyChange = t.QtyChange;
                //transaction.UnitPrice = t.UnitPrice;
                transaction.Desc = t.Desc;
                transaction.DeptCode = t.DeptCode;
                transaction.SuppCode = t.SuppCode;
                transaction.VoucherNo = t.VoucherNo;
            }
            return transaction;
        }
    }
}