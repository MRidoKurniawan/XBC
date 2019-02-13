namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_question()
        {
            t_document_test_detail = new HashSet<t_document_test_detail>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string question { get; set; }

        [Required]
        [StringLength(5)]
        public string question_type { get; set; }

        [StringLength(255)]
        public string option_a { get; set; }

        [StringLength(255)]
        public string option_b { get; set; }

        [StringLength(255)]
        public string option_c { get; set; }

        [StringLength(255)]
        public string option_d { get; set; }

        [StringLength(255)]
        public string option_e { get; set; }

        [StringLength(255)]
        public string image_url { get; set; }

        [StringLength(255)]
        public string image_a { get; set; }

        [StringLength(255)]
        public string image_b { get; set; }

        [StringLength(255)]
        public string image_c { get; set; }

        [StringLength(255)]
        public string image_d { get; set; }

        [StringLength(255)]
        public string image_e { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_document_test_detail> t_document_test_detail { get; set; }
    }
}
