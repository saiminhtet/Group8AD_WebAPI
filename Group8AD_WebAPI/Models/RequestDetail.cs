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

        public static List<RequestDetail> GetCurrReqDets(int empId)
        {
            string sqlquery = String.Format("SELECT * FROM RequestDetail WHERE ReqId IN (SELECT ReqId FROM Request WHERE " +
                "EmpId = '{0}' AND [Status] = 'Unsubmitted')", empId);
            return Query(sqlquery);
        }

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
        public static Boolean AddCurrReqDet(int empId, ItemQty itemQty)
        {
            return true;
        }
        public static Boolean RemoveCurrReqDet(int empId, int lineNo)
        {
            return true;
        }

        public static Boolean UpdateSubmittedReq(int reqId, List<ItemQty> reqDetQty)
        {
            return true;
        }
    }
}
