namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_assignment
    {
        public long id { get; set; }

        public long biodata_id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime? realization_date { get; set; }

        [StringLength(255)]
        public string notes { get; set; }

        public bool? is_hold { get; set; }

        public bool? is_done { get; set; }

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
