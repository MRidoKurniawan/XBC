using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TestViewModel
    {
        public long id { get; set; }

        [Display(Name = "Name")]
        [Required, StringLength(255)]
        public string name { get; set; }

        [Display(Name = "Is Bootcamp Test ?")]
        public bool isBootcampTest { get; set; }

        [Display(Name = "Notes")]
        [StringLength(255)]
        public string notes { get; set; }
       
        public long createdBy { get; set; }

        public long UserId { get; set; }

        [Display(Name = "Created By")]
        public string UserName { get; set; }

        public bool check { get; set; }
    }
}
