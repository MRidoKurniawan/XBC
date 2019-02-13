namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_batch_test
    {
        public long id { get; set; }

        public long batch_id { get; set; }

        public long test_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public virtual t_batch t_batch { get; set; }

        public virtual t_test t_test { get; set; }
    }
}
