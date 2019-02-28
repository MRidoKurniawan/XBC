using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XBC.ViewModel
{
    public class OfficeViewModel
    {
        public long id { get; set; }

        [Required, StringLength(50)]
        public string name { get; set; }

        [Required, StringLength(50)]
        public string phone { get; set; }

        [Required, StringLength(255)]
        public string email { get; set; }

        [Required, StringLength(500)]
        public string address { get; set; }

        [Required, StringLength(500)]
        public string notes { get; set; }
        public bool isDelete { get; set; }
        public string contact { get; set; }
        public long UserId { get; set; }
        public List<RoomViewModel> details { get; set; }
    }
}
