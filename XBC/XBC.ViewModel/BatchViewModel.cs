using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BatchViewModel
    {        
        public long id { get; set; }

        [Required, StringLength(255)]
        public string name { get; set; }

        public long technologyId { get; set; }

        public string technologyName { get; set; }

        public DateTime? periodTo { get; set; }

        public long? bootcampTypeId { get; set; }

        public long? roomId { get; set; }

        public long trainerId { get; set; }

        public string trainerName { get; set; }

        public DateTime? periodFrom { get; set; }

        [StringLength(255)]
        public string notes { get; set; }
    }
}
