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

        //Returns a list of item which is the top 6 items requested for the given employee id
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

        //Returns a list of items with the search parameter, category and description
        //If search parameter is not required, pass empty string parameter
        public static List<Item> GetItems(string cat, string desc)
        {
            string sqlquery = String.Format("SELECT * FROM Item WHERE " +
                "Cat = '{0}' AND [Desc] = '{1}'", cat, desc);
            return Query(sqlquery);
        }

        //Returns a list of item where the balance is less than the re-order level
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
        //Updates the temporarly disbursement quantity for the given item code found in ItemQty{ItemCode, Qty} 
        public static Boolean UpdateTempQtyDisb(ItemQty itemQty)
        {
            return true;
        }

        //Resets the temporarly disbursement quantity to 0 for all the items in the item table
        public static Boolean ResetTempQtyDisb()
        {
            return true;
        }

        //Updates the temporarly stock check quantity for the given item code found in ItemQty{ItemCode, Qty
        public static Boolean UpdateTempQtyCheck(ItemQty itemQty)
        {
            return true;
        }

        //Resets the temporarly stock check quantity to 0 for all the items in the item table
        public static Boolean ResetTempQtyCheck()
        {
            return true;
        }

        //Updates the restock level in the Item table for the given item code in ItemCode{ItemCode, Qty}
        public static Boolean UpdateRestockLevel(List<ItemQty> itemQtys)
        {
            return true;
        }

        //Updates the preferred supplier for the item codes 
        //in the list of PrefSupp{ItemCode, SuppCode1, Price1, SuppCode2, Price2, SuppCode3, Price3}
        public static Boolean UpdatePrefSupp(List<PrefSupp> prefSupp)
        {
            return true;
        }
    }
}
