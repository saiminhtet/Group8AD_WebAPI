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

        //Returns request object belonging to the given request id
        public static Request GetReqById(int reqId)
        {
            string sqlquery = String.Format("SELECT * FROM Request " +
           "WHERE ReqId = '{0}'", reqId);
            return QueryFirst(sqlquery);
        }

        //Returns a list of request belonging to the given employee id for the given status
        //Pass empty string parameter to status to return all requests belonging to the given employee id
        public static List<Request> GetReqs(int empId, string status)
        {
            string sqlquery = String.Format("SELECT * FROM Request " +
            "WHERE [Status] LIKE '{0}%' AND EmpId = '{1}'", status, empId);

            return Query(sqlquery);
        }

        //Returns a list of statuses found in the request table
        public static List<string> GetStatusList()
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Database.SqlQuery<string>("SELECT DISTINCT [Status] FROM Request").ToListAsync();
                return query.Result;
            }
        }

        //Returns a list of request in the same department as the given employee id
        public static List<Request> GetDeptReqs(int empId)
        {
            string sqlquery = String.Format("SELECT * FROM Request WHERE EmpId IN (SELECT EmpId FROM Employee WHERE DeptCode IN " +
                "(SELECT DeptCode FROM Department WHERE DeptHeadId = '{0}'))", empId);
            return Query(sqlquery);
        }

        //Returns the request object for the given ReqId
        public static Request GetReq(int reqId)
        {
            string sqlquery = String.Format("SELECT * FROM Request WHERE ReqId = '{0}'", reqId);
            Request req = QueryFirst(sqlquery);
            req.RequestDetail = Group8AD_WebAPI.Models.RequestDetail.GetReqDetByReqId(reqId);
            return req;
        }

        //Returns a list of request base on the search criteria: department code, employee name
        //Pass empty string parameter if that criteria is not required (e.g. empName = "")
        public static List<Request> GetReqsByDeptEmp(string deptCode, string empName)
        {
            string sqlquery = String.Format("SELECT * FROM Request WHERE EmpId IN (SELECT EmpId FROM Employee WHERE " +
                "DeptCode LIKE '{0}%' AND EmpName LIKE '{1}%')", deptCode, empName);
            return Query(sqlquery);
        }

        //Returns a list of unfulfilled request
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
        //Deletes the entire unsubmitted request belonging to the given employee id
        public static Boolean RemoveCurrReq(int empId)
        {
            return true;
        }

        //Submits the unsubmitted request belonging to the given employee id
        public static Boolean SubmitCurrReq(int empId)
        {
            return true;
        }

        //Approve/Reject a request belonging to the given request id
        //Updates request's status and inserts notification
        public static Boolean ApproveReq(int reqId, Boolean isApprove, string comment)
        {
            return true;
        }

        //Accept await request belonging to the department, for the given employee rep id
        //Takes in the model ItemQty{ItemCode, Qty} and updates the fulfill qty
        //Discrepancies found between await and fulfil qty will insert into the adjustment table
        public static Boolean AcceptDisb(int repId, List<ItemQty> fulfilItems)
        {
            return true;
        }

        //Updates the collection point of the department belonging to the given employee rep id
        public static Boolean UpdateColPt(int empId, int colPtId)
        {
            return true;
        }

        //BATCH update approved requests based on FIFO on request datetime
        //Updates await qty on impacted request details
        public static Boolean SubmitAwaitReqs(List<ItemQty> itemQtys)
        {
            return true;
        }

        //Single update on approved request for the given employee id
        //Updates await qty in request detail belonging to the request
        public Boolean FulfilEmpReq(int reqId, List<ItemQty> itemQtys)
        {
            return true;
        }

        //Inserts into adjustment if there is a discrepancy between itemQty and Item table's balance
        public Boolean SubmitStockTake(List<ItemQty> itemQtys)
        {
            return true;
        }
    }
}
