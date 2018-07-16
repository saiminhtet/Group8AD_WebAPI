namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        [Key]
        public int TranId { get; set; }

        public DateTime TranDateTime { get; set; }

        [Required]
        [StringLength(4)]
        public string ItemCode { get; set; }

        public int QtyChange { get; set; }

        public double? UnitPrice { get; set; }

        [StringLength(100)]
        public string Desc { get; set; }

        [StringLength(4)]
        public string DeptCode { get; set; }

        [StringLength(4)]
        public string SuppCode { get; set; }

        //Returns list of DeptCBVol{DeptCode, ChargeBack, Volume} for the given month and year grouped by department
        public static List<DeptCBVol> GetMthYrDeptCBVol(int mth, int year)
        {
            string sqlquery = String.Format("SELECT DeptCode, SUM(UnitPrice * QtyChange) AS ChargeBack, SUM(QtyChange) AS Volume FROM [Transaction] " +
                "WHERE MONTH(TranDateTime) = '{0}' AND YEAR(TranDateTime) = '{1}' AND [Desc] = 'Disbursement' Group By DeptCode", mth, year);
            return QueryDeptCBVolList(sqlquery);
        }

        //Returns list of DeptCBVol{DeptCode, ChargeBack, Volume} between the given dates grouped by department
        public static List<DeptCBVol> GetDateRangeDeptCBVol(DateTime fromDate, DateTime toDate)
        {
            string fromDateFormat = fromDate.ToString("yyyy-MM-dd");
            string toDateFormat = toDate.ToString("yyyy-MM-dd");
            string sqlquery = String.Format("SELECT DeptCode, SUM(UnitPrice * QtyChange) AS ChargeBack, SUM(QtyChange) AS Volume FROM [Transaction] " +
                "WHERE TranDateTime BETWEEN '{0}' AND '{1}' AND [Desc] = 'Disbursement' Group By DeptCode", fromDateFormat, toDateFormat);
            return QueryDeptCBVolList(sqlquery);
        }

        public static List<DeptCBVol> QueryDeptCBVolList(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Database.SqlQuery<DeptCBVol>(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        public static DeptCBVol QueryDeptCBVol(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Database.SqlQuery<DeptCBVol>(sqlquery).FirstAsync();
                return query.Result;
            }
        }

        //Dummy methods
        //Inserts a transaction object into the transaction table
        public static Boolean AddTransaction(Transaction transaction)
        {
            return true;
        }
    }
}
