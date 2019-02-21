using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TechnologyViewModel
    {
        public long id { get; set; }
        [Required, StringLength(255)]
        [Display(Name="NAME")]
        public string name { get; set; }

        [StringLength(255)]
        public string notes { get; set; }
        [Display(Name="CREATED BY")]
        public long created_by { get; set; }
        public bool is_delete { get; set; }

        public List<TechnologyTrainerViewModel> Details { get; set; }

    }
}
