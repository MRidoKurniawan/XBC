using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XBC.ViewModel
{
    public class BootcampTestTypeViewModel
    {
        public long id { get; set; }

        [Required, StringLength(255)]
        public string name { get; set; }
        [Required, StringLength(255)]
        public string notes { get; set; }
        public bool isDelete { get; set; }
        public long created_by { get; set; }
        public string UserName { get; set; }
    }
}
