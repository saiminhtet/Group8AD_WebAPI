namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Request")]
    public partial class Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request()
        {
            RequestDetail = new HashSet<RequestDetail>();
        }

        [Key]
        public int ReqId { get; set; }

        public int EmpId { get; set; }

        public int? ApproverId { get; set; }

        [StringLength(100)]
        public string ApproverComment { get; set; }

        public DateTime ReqDateTime { get; set; }

        public DateTime? ApprovedDateTime { get; set; }

        public DateTime? CancelledDateTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public DateTime? FulfilledDateTime { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }

        public static Request GetReqById(int reqId)
        {
            string sqlquery = String.Format("SELECT * FROM Request " +
           "WHERE ReqId = '{0}'", reqId);
            return QueryFirst(sqlquery);
        }
        public static List<Request> GetReqsByStatus(int empId, string status)
        {
            string sqlquery = String.Format("SELECT * FROM Request " +
            "WHERE [Status] = '{0}' AND EmpId = '{1}'", status, empId);

            return Query(sqlquery);
        }

        public static List<string> GetStatusList()
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Database.SqlQuery<string>("SELECT DISTINCT [Status] FROM Request").ToListAsync();
                return query.Result;
            }
        }

        public static List<Request> GetEmpReqs(int empId)
        {
            string sqlquery = String.Format("SELECT * FROM Request WHERE EmpId = '{0}'", empId);
            return Query(sqlquery);
        }

        public static Request GetReqDets(int reqId)
        {
            string sqlquery = String.Format("SELECT * FROM Request WHERE ReqId = '{0}'", reqId);
            Request req = QueryFirst(sqlquery);
            req.RequestDetail = Group8AD_WebAPI.Models.RequestDetail.GetReqDetByReqId(reqId);
            return req;
        }

        public static List<Request> GetReqsByDeptEmp(string deptCode, string empName)
        {
            string sqlquery = String.Format("SELECT * FROM Request WHERE EmpId IN (SELECT EmpId FROM Employee WHERE " +
                "DeptCode LIKE '{0}%' AND EmpName LIKE '{1}%')", deptCode, empName);
            return Query(sqlquery);
        }

        public static List<Request> GetUnfulfilledReqs()
        {
            return Query("SELECT * FROM Request WHERE Status = 'Unfulfilled'");
        }
        public static List<Request> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Request.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        public static Request QueryFirst(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Request.SqlQuery(sqlquery).FirstAsync();
                return query.Result;
            }
        }


        //--dummy methods
        public static Boolean RemoveCurrReq(int empId)
        {
            return true;
        }

        public static Boolean SubmitCurrReq(int empId)
        {
            return true;
        }

        public static Boolean ApproveReq(int reqId, Boolean isApprove, string comment)
        {
            return true;
        }

        public static Boolean AcceptDisb(int empId, List<ItemQty> fulfilItems)
        {
            return true;
        }

        public static Boolean UpdateColPt(int empId, int colPtId)
        {
            return true;
        }

        public static Boolean FulfilReqs(List<ItemQty> itemQtys)
        {
            return true;
        }

        public Boolean FulfilEmpReq(int reqId, List<ItemQty> itemQtys)
        {
            return true;
        }

        public Boolean SubmitStockTake(List<ItemQty> itemQtys)
        {
            return true;
        }
    }
}
