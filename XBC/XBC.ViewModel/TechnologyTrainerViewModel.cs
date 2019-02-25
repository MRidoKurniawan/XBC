using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TechnologyTrainerViewModel
    {
        public long id { get; set; }
        public long technology_id { get; set; }
        public long trainer_id { get; set; }
        [Display(Name ="NAME")]
        public string trainer_name { get; set; }
        public long created_by { get; set; }
        public DateTime created_on { get; set; }

        
    }
}
