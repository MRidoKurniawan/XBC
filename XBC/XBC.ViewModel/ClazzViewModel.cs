using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class ClazzViewModel
    {
        public long id { get; set; }
        public long batchId { get; set; }
        public string BatchName { get; set; }
        [Display(Name = "Biodata Name")]
        public long biodataId { get; set; }
        public string BiodataName { get; set; }
        public long UserId { get; set; }
    }
}
