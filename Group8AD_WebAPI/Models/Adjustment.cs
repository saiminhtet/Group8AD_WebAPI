namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adjustment")]
    public partial class Adjustment
    {
        [Key]
        [StringLength(20)]
        public string VoucherNo { get; set; }

        public int EmpId { get; set; }

        public DateTime DateTimeIssued { get; set; }

        [Required]
        [StringLength(4)]
        public string ItemCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Reason { get; set; }

        public int QtyChange { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public int? ApproverId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Item Item { get; set; }

        public static List<Adjustment> GetAll()
        {
            return Query("SELECT * FROM Adjustment");
        }

        public static List<Adjustment> GetAdjByEmp(int empId)
        {
            string sqlquery = String.Format("SELECT * FROM Adjustment " +
                "WHERE EmpId = '{0}'", empId);
            return Query(sqlquery);
        }

        public static List<string> GetStatusList()
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Database.SqlQuery<string>("SELECT DISTINCT [Status] FROM Adjustment").ToListAsync();
                return query.Result;
            }
        }


        public static List<Adjustment> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Adjustment.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        //Dummy methods

        public static Boolean ReceiveItem(ItemQty itemQty, string suppCode)
        {
            return true;
        }

        public static Boolean ApproveAdj(string voucherNo, Boolean isApprove)
        {
            return true;
        }
    }
}
