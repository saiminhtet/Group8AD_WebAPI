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

        // add a trnsaction
        public static Transaction AddTran(Transaction t)
        {
            Transaction transaction = new Transaction();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                transaction.TranId = t.TranId;
                transaction.TranDateTime = t.TranDateTime;
                transaction.ItemCode = t.ItemCode;
                transaction.QtyChange = t.QtyChange;
                transaction.UnitPrice = t.UnitPrice;
                transaction.Desc = t.Desc;
                transaction.DeptCode = t.DeptCode;
                transaction.SuppCode = t.SuppCode;
                entities.Transactions.Add(transaction);
                entities.SaveChanges();
            }
            return transaction;
        }
    }
}