namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_biodata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_biodata()
        {
            t_assignment = new HashSet<t_assignment>();
            t_clazz = new HashSet<t_clazz>();
            t_monitoring = new HashSet<t_monitoring>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [StringLength(5)]
        public string gender { get; set; }

        [Required]
        [StringLength(100)]
        public string last_education { get; set; }

        [Required]
        [StringLength(5)]
        public string graduation_year { get; set; }

        [Required]
        [StringLength(5)]
        public string educational_level { get; set; }

        [Required]
        [StringLength(100)]
        public string majors { get; set; }

        [Required]
        [StringLength(5)]
        public string gpa { get; set; }

        public long? bootcamp_test_type { get; set; }

        public long? iq { get; set; }

        [StringLength(10)]
        public string du { get; set; }

        public long? arithmetic { get; set; }

        public long? nested_logic { get; set; }

        public long? join_table { get; set; }

        [StringLength(50)]
        public string tro { get; set; }

        [StringLength(100)]
        public string notes { get; set; }

        [StringLength(100)]
        public string interviewer { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_assignment> t_assignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_clazz> t_clazz { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_monitoring> t_monitoring { get; set; }
    }
}
