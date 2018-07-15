namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Adjustment = new HashSet<Adjustment>();
            Adjustment1 = new HashSet<Adjustment>();
            Department = new HashSet<Department>();
            Department1 = new HashSet<Department>();
            Department2 = new HashSet<Department>();
            Notification = new HashSet<Notification>();
            Notification1 = new HashSet<Notification>();
            Request = new HashSet<Request>();
            Request1 = new HashSet<Request>();
        }

        [Key]
        public int EmpId { get; set; }

        [Required]
        [StringLength(4)]
        public string DeptCode { get; set; }

        [Required]
        [StringLength(80)]
        public string EmpName { get; set; }

        [Required]
        [StringLength(200)]
        public string EmpAddr { get; set; }

        [Required]
        [StringLength(50)]
        public string EmpEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string EmpCtcNo { get; set; }

        [Required]
        [StringLength(80)]
        public string Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjustment> Adjustment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjustment> Adjustment1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department2 { get; set; }

        public virtual Department Department3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notification1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Request { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Request1 { get; set; }

        public static List<Employee> GetDeptEmps(int empId)
        {
            string sqlquery = String.Format("SELECT * FROM Employee WHERE DeptCode IN (" +
                "SELECT DeptCode FROM Department WHERE DeptHeadId = '{0}')", empId);
            return Query(sqlquery);
        }

        public static Employee GetDel(int empHeadId)
        {
            string sqlquery = String.Format("SELECT * FROM Employee WHERE EmpId IN (SELECT DelegateApproverId FROM Department " +
                "WHERE DeptCode IN (SELECT DeptCode FROM Employee WHERE EmpId = '{0}'))", empHeadId);
            return QueryFirst(sqlquery);
        }

        public static Employee GetRep(int empHeadId)
        {
            string sqlquery = String.Format("SELECT * FROM Employee WHERE EmpId IN (SELECT DeptRepId FROM Department " +
                "WHERE DeptCode IN (SELECT DeptCode FROM Employee WHERE EmpId = '{0}'))", empHeadId);
            return QueryFirst(sqlquery);
        }
        public static List<Employee> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Employee.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }

        public static Employee QueryFirst(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Employee.SqlQuery(sqlquery).FirstAsync();
                return query.Result;
            }
        }
    }
}
