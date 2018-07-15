namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Adjustment = new HashSet<Adjustment>();
            RequestDetail = new HashSet<RequestDetail>();
        }

        [Key]
        [StringLength(4)]
        public string ItemCode { get; set; }

        [Required]
        [StringLength(20)]
        public string Cat { get; set; }

        [Required]
        [StringLength(100)]
        public string Desc { get; set; }

        [Required]
        [StringLength(3)]
        public string Location { get; set; }

        [Required]
        [StringLength(20)]
        public string UOM { get; set; }

        public bool IsActive { get; set; }

        public int Balance { get; set; }

        public int ReorderLevel { get; set; }

        public int ReorderQty { get; set; }

        public int? TempQtyDisb { get; set; }

        public int? TempQtyCheck { get; set; }

        [StringLength(4)]
        public string SuppCode1 { get; set; }

        public double? Price1 { get; set; }

        [StringLength(4)]
        public string SuppCode2 { get; set; }

        public double? Price2 { get; set; }

        [StringLength(4)]
        public string SuppCode3 { get; set; }

        public double? Price3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjustment> Adjustment { get; set; }

        public virtual SupplierList SupplierList { get; set; }

        public virtual SupplierList SupplierList1 { get; set; }

        public virtual SupplierList SupplierList2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }

        public static List<Item> GetFreqItems(int empId)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                string sqlquery = String.Format("SELECT * From Item WHERE ItemCode IN(SELECT TOP 6 ItemCode From RequestDetail " +
                    "WHERE ReqId IN (Select ReqId From Request Where EmpId = '{0}') " +
                    "GROUP BY ItemCode Order By SUM(ReqQty) Desc)", empId);

                var query = context.Item.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        public static List<Item> GetItemsByCatDesc(string cat, string desc)
        {
            string sqlquery = String.Format("SELECT * FROM Item WHERE " +
                "Cat = '{0}' AND [Desc] = '{1}'", cat, desc);
            return Query(sqlquery);
        }

        public static List<Item> GetLowStocks()
        {
            string sqlquery = String.Format("SELECT *FROM Item " +
                "WHERE ReorderLevel > Balance");
            return Query(sqlquery);
        }

        public static List<Item> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Item.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        //--Dummy Methods

        public static Boolean UpdateTempQtyDisb(ItemQty itemQty)
        {
            return true;
        }

        public static Boolean ResetTempQtyDisb()
        {
            return true;
        }

        public static Boolean UpdateTempQtyCheck(ItemQty itemQty)
        {
            return true;
        }

        public static Boolean ResetTempQtyCheck()
        {
            return true;
        }

        public static Boolean UpdateRestockLevel(List<ItemQty> itemQtys)
        {
            return true;
        }

        public static Boolean UpdatePrefSupp(List<PrefSupp> prefSupp)
        {
            return true;
        }
    }
}
