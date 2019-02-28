using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class RoomViewModel
    {
        public long id { get; set; }
        
        [Required, StringLength (50)]
        public string code { get; set; }

        [Required, StringLength(50)]
        public string name { get; set; }

        public long capacity { get; set; }

        public bool any_projector { get; set; }

        [Required, StringLength(500)]
        public string notes { get; set; }

        public long officeId { get; set; }

        public bool isDelete { get; set; }
        public long UserId { get; set; }
    }
}
