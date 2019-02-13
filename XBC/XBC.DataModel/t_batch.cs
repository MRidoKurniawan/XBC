namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_batch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_batch()
        {
            t_batch_test = new HashSet<t_batch_test>();
            t_clazz = new HashSet<t_clazz>();
        }

        public long id { get; set; }

        public long technology_id { get; set; }

        public long trainer_id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public DateTime? period_from { get; set; }

        public DateTime? period_to { get; set; }

        public long? room_id { get; set; }

        public long? bootcamp_type_id { get; set; }

        [StringLength(255)]
        public string notes { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        public virtual t_bootcamp_type t_bootcamp_type { get; set; }

        public virtual t_room t_room { get; set; }

        public virtual t_technology t_technology { get; set; }

        public virtual t_trainer t_trainer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_batch_test> t_batch_test { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_clazz> t_clazz { get; set; }
    }
}
