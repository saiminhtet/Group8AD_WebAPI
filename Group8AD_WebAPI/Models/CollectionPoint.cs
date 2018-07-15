namespace Group8AD_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CollectionPoint")]
    public partial class CollectionPoint
    {
        [Key]
        public int ColPtId { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        public static List<CollectionPoint> GetAll()
        {
            return Query("SELECT * FROM CollectionPoint");
        }
        public static List<CollectionPoint> Query(string sqlquery)
        {
            using (var context = new SA46Team08ADProjectContext())
            {
                var query = context.CollectionPoint.SqlQuery(sqlquery).ToListAsync();
                return query.Result;
            }
        }
    }
}
