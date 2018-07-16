namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplierList")]
    public partial class SupplierList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierList()
        {
            Item = new HashSet<Item>();
            Item1 = new HashSet<Item>();
            Item2 = new HashSet<Item>();
        }

        [Key]
        [StringLength(4)]
        public string SuppCode { get; set; }

        [Required]
        [StringLength(80)]
        public string SuppName { get; set; }

        [Required]
        [StringLength(80)]
        public string SuppCtcName { get; set; }

        [Required]
        [StringLength(20)]
        public string SuppCtcNo { get; set; }

        [Required]
        [StringLength(20)]
        public string SuppFaxNo { get; set; }

        [Required]
        [StringLength(200)]
        public string SuppAddr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Item { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Item1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Item2 { get; set; }

        //Returns a list of Supplier found in the Supplier table
        public static List<SupplierList> GetAll()
        {
            return Query("SELECT * FROM SupplierList");
        }


        public static List<SupplierList> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.SupplierList.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }
    }
}
