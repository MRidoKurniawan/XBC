using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class QuestionViewModel
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string question { get; set; }

        [Required]
        [StringLength(5)]
        public string question_type { get; set; }

        [StringLength(255)]
        public string option_a { get; set; }

        [StringLength(255)]
        public string option_b { get; set; }

        [StringLength(255)]
        public string option_c { get; set; }

        [StringLength(255)]
        public string option_d { get; set; }

        [StringLength(255)]
        public string option_e { get; set; }

        [StringLength(255)]
        public string image_url { get; set; }

        [StringLength(255)]
        public string image_a { get; set; }

        [StringLength(255)]
        public string image_b { get; set; }

        [StringLength(255)]
        public string image_c { get; set; }

        [StringLength(255)]
        public string image_d { get; set; }

        [StringLength(255)]
        public string image_e { get; set; }
    }
}
