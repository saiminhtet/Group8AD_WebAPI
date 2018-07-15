namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SA46Team08ADProjectContext : DbContext
    {
        public SA46Team08ADProjectContext()
            : base("name=SA46Team08ADProjectContext")
        {
        }

        public virtual DbSet<Adjustment> Adjustment { get; set; }
        public virtual DbSet<CollectionPoint> CollectionPoint { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestDetail> RequestDetail { get; set; }
        public virtual DbSet<SupplierList> SupplierList { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employee3)
                .WithRequired(e => e.Department3)
                .HasForeignKey(e => e.DeptCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Adjustment)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ApproverId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Adjustment1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.EmpId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Department)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.DelegateApproverId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Department1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.DeptHeadId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Department2)
                .WithOptional(e => e.Employee2)
                .HasForeignKey(e => e.DeptRepId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Notification)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.FromEmp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Notification1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.ToEmp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Request)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ApproverId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Request1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.EmpId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Adjustment)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.RequestDetail)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.RequestDetail)
                .WithRequired(e => e.Request)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierList>()
                .HasMany(e => e.Item)
                .WithOptional(e => e.SupplierList)
                .HasForeignKey(e => e.SuppCode1);

            modelBuilder.Entity<SupplierList>()
                .HasMany(e => e.Item1)
                .WithOptional(e => e.SupplierList1)
                .HasForeignKey(e => e.SuppCode2);

            modelBuilder.Entity<SupplierList>()
                .HasMany(e => e.Item2)
                .WithOptional(e => e.SupplierList2)
                .HasForeignKey(e => e.SuppCode3);
        }
    }
}
