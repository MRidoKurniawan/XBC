namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_test_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_test_type()
        {
            t_document_test = new HashSet<t_document_test>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(255)]
        public string notes { get; set; }

        public long type_of_answer { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_document_test> t_document_test { get; set; }
    }
}
