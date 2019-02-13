namespace XBC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class t_menu_access
    {
        public long id { get; set; }

        public long menu_id { get; set; }

        public long role_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public virtual t_menu t_menu { get; set; }

        public virtual t_role t_role { get; set; }
    }
}
