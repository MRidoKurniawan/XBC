namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_monitoring
    {
        public long id { get; set; }

        public long biodata_id { get; set; }

        public DateTime idle_date { get; set; }

        [StringLength(50)]
        public string last_project { get; set; }

        [StringLength(255)]
        public string idle_reason { get; set; }

        public DateTime? placement_date { get; set; }

        [StringLength(50)]
        public string placement_at { get; set; }

        [StringLength(255)]
        public string notes { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        public virtual t_biodata t_biodata { get; set; }
    }
}
