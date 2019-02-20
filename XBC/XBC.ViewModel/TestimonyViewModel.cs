using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TestimonyViewModel
    {
        public long id { get; set; }

        [Display(Name="TITLE")]
        [Required, StringLength(255)]
        public string title { get; set; }

        [StringLength(5000)]
        public string content { get; set; }
        public bool is_delete { get; set; }
    }
}
