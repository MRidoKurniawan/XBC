using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class AssignmentViewModel
    {
        public long id { get; set; }

        public long biodata_id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime? realization_date { get; set; }

        [StringLength(255)]
        public string notes { get; set; }

        public bool? is_hold { get; set; }

        public bool? is_done { get; set; }

        public long UserId { get; set; }

        public string TanggalMulai { get; set; }
        public string TanggalSelesai { get; set; }
    }
}
