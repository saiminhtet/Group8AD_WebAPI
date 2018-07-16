namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestDetail")]
    public partial class RequestDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReqId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReqLineNo { get; set; }

        [Required]
        [StringLength(4)]
        public string ItemCode { get; set; }

        public int ReqQty { get; set; }

        public int AwaitQty { get; set; }

        public int FulfilledQty { get; set; }

        public virtual Item Item { get; set; }

        public virtual Request Request { get; set; }

        //Returns a list of request detail belonging to the UNSUBMITTED request for the given employee id
        public static List<RequestDetail> GetCurrReqDets(int empId)
        {
            string sqlquery = String.Format("SELECT * FROM RequestDetail WHERE ReqId IN (SELECT ReqId FROM Request WHERE " +
                "EmpId = '{0}' AND [Status] = 'Unsubmitted')", empId);
            return Query(sqlquery);
        }

        //Returns a list of request details belong to the given request id
        public static List<RequestDetail> GetReqDetByReqId(int reqId)
        {
            string sqlquery = String.Format("SELECT * FROM RequestDetail WHERE ReqId = '{0}'", reqId);
            return Query(sqlquery);
        }


        public static List<RequestDetail> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.RequestDetail.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        //--dummy methods
        //inserts ItemQty{ItemCode, Qty} into request detail into the UNSUBMITTED request belonging to the given employee id
        //inserts a new Request with status unsubmitted if there is no unsubmitted request belonging to the given employee id
        public static Boolean AddCurrReqDet(int empId, ItemQty itemQty)
        {
            return true;
        }

        //removes the request detail for the given item code in the UNSUBMITTED request belonging to the given employee id
        public static Boolean RemoveCurrReqDet(int empId, string itemCode)
        {
            return true;
        }

        //updates a submitted request based on the list ItemQty{ItemCode, Qty} for the given request id
        public static Boolean UpdateSubmittedReq(int reqId, List<ItemQty> reqDetQty)
        {
            return true;
        }
    }
}
