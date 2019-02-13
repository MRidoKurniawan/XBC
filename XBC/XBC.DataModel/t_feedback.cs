namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_feedback
    {
        public long id { get; set; }

        public long test_id { get; set; }

        public long document_test_id { get; set; }

        [StringLength(5000)]
        public string json_feedback { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        public virtual t_document_test t_document_test { get; set; }

        public virtual t_test t_test { get; set; }
    }
}
