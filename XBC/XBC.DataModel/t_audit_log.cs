namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_audit_log
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Required]
        [StringLength(10)]
        public string type { get; set; }

        [StringLength(5000)]
        public string json_insert { get; set; }

        [StringLength(5000)]
        public string json_before { get; set; }

        [StringLength(5000)]
        public string json_after { get; set; }

        [StringLength(5000)]
        public string json_delete { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }
    }
}
