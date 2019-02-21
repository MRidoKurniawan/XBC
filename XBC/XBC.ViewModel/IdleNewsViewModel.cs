using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class IdleNewsViewModel
    {
        public long id { get; set; }

        [Display(Name = "CATEGORY")]
        public long categoryId { get; set; }
        [Display(Name = "CATEGORY")]
        public string categoryName { get; set; }

        [Required, StringLength(255)]

        [Display(Name = "TITLE")]
        public string title { get; set; }

        [StringLength(5000)]
        public string content { get; set; }

        public bool isPublish { get; set; }
        public bool isDelete { get; set; }
    }
}
