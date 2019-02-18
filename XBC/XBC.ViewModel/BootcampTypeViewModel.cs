using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BootcampTypeViewModel
    {
        public long id { get; set; }

        [Required, StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string notes { get; set; }
        public long createdBy { get; set; }
    }
}
