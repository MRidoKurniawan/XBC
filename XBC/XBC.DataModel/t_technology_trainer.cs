namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_technology_trainer
    {
        public long id { get; set; }

        public long technology_id { get; set; }

        public long trainer_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public virtual t_technology t_technology { get; set; }

        public virtual t_trainer t_trainer { get; set; }
    }
}
