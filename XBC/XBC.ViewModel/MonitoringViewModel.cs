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

        [Display(Name = "Name")]
        public long biodata_id { get; set; }

        public string name { get; set; } //t_biodata

        public DateTime idle_date { get; set; }

        [StringLength(50)]
        [Display(Name = "Last_Project")]
        public string last_project { get; set; }

        [StringLength(255)]
        [Display(Name = "Idle_Reason")]
        public string idle_reason { get; set; }

        public DateTime? placement_date { get; set; }

        [StringLength(50)]
        [Display(Name = "Placement_At")]
        public string placement_at { get; set; }
        [Display(Name = "Placement_Date")]
        public string date_placement { get; set; }

        [Display(Name = "Idle_Date")]
        public string date_idle { get; set; }

        [StringLength(255)]
        [Display(Name = "Notes")]
        public string notes { get; set; }
        public long UserId { get; set; }
        public bool is_delete { get; set; }

    }
}
