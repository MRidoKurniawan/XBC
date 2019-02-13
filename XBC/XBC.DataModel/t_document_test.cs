namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_document_test
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_document_test()
        {
            t_document_test_detail = new HashSet<t_document_test_detail>();
            t_feedback = new HashSet<t_feedback>();
        }

        public long id { get; set; }

        public long test_id { get; set; }

        public long test_type_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal version { get; set; }

        [Required]
        [StringLength(10)]
        public string token { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_document_test_detail> t_document_test_detail { get; set; }

        public virtual t_test t_test { get; set; }

        public virtual t_test_type t_test_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_feedback> t_feedback { get; set; }
    }
}
