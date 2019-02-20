using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TechnologyTrainerViewModel
    {
        public long id { get; set; }
        public long technology_id { get; set; }
        public string technology_name { get; set; }
        public string technology_notes { get; set; }
        public long technology_created_by { get; set; }
        public bool technology_is_delete { get; set; }
        public long trainer_id { get; set; }
        public string trainer_name { get; set; }
        public long created_by { get; set; }
        public DateTime created_on { get; set; }
    }
}
