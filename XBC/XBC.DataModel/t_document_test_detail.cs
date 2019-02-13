namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_document_test_detail
    {
        public long id { get; set; }

        public long question_id { get; set; }

        public long document_test_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public virtual t_document_test t_document_test { get; set; }

        public virtual t_question t_question { get; set; }
    }
}
