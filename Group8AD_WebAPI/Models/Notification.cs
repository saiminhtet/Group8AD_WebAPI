namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        public int NotificationId { get; set; }

        public DateTime NotificationDateTime { get; set; }

        public int FromEmp { get; set; }

        public int ToEmp { get; set; }

        [StringLength(100)]
        public string RouteUri { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Content { get; set; }

        public bool IsRead { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public static List<Notification> GetEmpUnreadNoti(int empId)
        {
            string sqlquery = String.Format("SELECT * FROM [Notification] " +
                "WHERE ToEmp = '{0}'", empId);
            return Query(sqlquery);
        }
        public static List<Notification> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.Notification.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }
    }
}
