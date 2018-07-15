namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            Employee3 = new HashSet<Employee>();
        }

        [Key]
        [StringLength(4)]
        public string DeptCode { get; set; }

        [Required]
        [StringLength(100)]
        public string DeptName { get; set; }

        [Required]
        [StringLength(20)]
        public string DeptCtcNo { get; set; }

        [Required]
        [StringLength(20)]
        public string DeptFaxNo { get; set; }

        public int? ColPtId { get; set; }

        public int? DeptHeadId { get; set; }

        public int? DeptRepId { get; set; }

        public int? DelegateApproverId { get; set; }

        public DateTime? DelegateFromDate { get; set; }

        public DateTime? DelegateToDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee3 { get; set; }

        public static List<Department> GetAll()
        {
            return Query("SELECT * FROM Department");
        }
        public static List<Department> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Department.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        //--Dummy methods
        public static Boolean UpdateDel(int empId, DateTime fromDate, DateTime toDate)
        {
            return true;
        }

        public static Boolean UpdateRep(int empId)
        {
            return true;
        }

        public static Boolean RemoveDel(int empId)
        {
            return true;
        }
    }
}
