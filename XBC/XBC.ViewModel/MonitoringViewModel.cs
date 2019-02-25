using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class MonitoringViewModel
    {
        public long id { get; set; }

        public long biodata_id { get; set; }
        public string name { get; set; }

        public DateTime idle_date { get; set; }

        [StringLength(50)]
        public string last_project { get; set; }

        [StringLength(255)]
        public string idle_reason { get; set; }

        public DateTime? placement_date { get; set; }

        [StringLength(50)]
        public string placement_at { get; set; }

        [StringLength(255)]
        public string notes { get; set; }
        public bool is_delete { get; set; }

    }
}
