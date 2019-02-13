namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_idle_news
    {
        public long id { get; set; }

        public long category_id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [StringLength(5000)]
        public string content { get; set; }

        public bool is_publish { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        public virtual t_category t_category { get; set; }
    }
}
