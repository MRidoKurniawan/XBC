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

        [Display(Name = "NAME")]
        [Required, StringLength(255)]
        public string name { get; set; }

        [Display(Name = "NOTES")]
        [StringLength(255)]
        public string notes { get; set; }

        [Display(Name = "CREATED BY")]
        public long createdBy { get; set; }
        public long UserId { get; set; }
        public string CreatedByName { get; set; }
    }
}
