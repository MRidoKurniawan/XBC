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

        [Display(Name = "Name")]
        [Required, StringLength(255)]
        public string name { get; set; }

        [Display(Name = "Technology")]
        public long technologyId { get; set; }

        [Display(Name = "Technology")]
        public string technologyName { get; set; }

        [Display(Name = "Period To")]
        public DateTime? periodTo { get; set; }

        [Display(Name = "Bootcamp Type")]
        public long? bootcampTypeId { get; set; }

        [Display(Name = "Room")]
        public long? roomId { get; set; }

        [Display(Name = "Trainer")]
        public long trainerId { get; set; }

        [Display(Name = "Trainer")]
        public string trainerName { get; set; }

        [Display(Name = "Period From")]
        public DateTime? periodFrom { get; set; }

        [Display(Name = "Notes")]
        [StringLength(255)]
        public string notes { get; set; }

        public long UserId { get; set; }
    }
}
