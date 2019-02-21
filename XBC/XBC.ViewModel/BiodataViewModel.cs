using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BiodataViewModel
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [StringLength(5)]
        public string gender { get; set; }

        [Required]
        [StringLength(100)]
        public string last_education { get; set; }

        [Required]
        [StringLength(5)]
        public string graduation_year { get; set; }

        [Required]
        [StringLength(5)]
        public string educational_level { get; set; }

        [Required]
        [StringLength(100)]
        public string majors { get; set; }

        [Required]
        [StringLength(5)]
        public string gpa { get; set; }

        public long? bootcamp_test_type { get; set; }

        public long? iq { get; set; }

        [StringLength(10)]
        public string du { get; set; }

        public long? arithmetic { get; set; }

        public long? nested_logic { get; set; }

        public long? join_table { get; set; }

        [StringLength(50)]
        public string tro { get; set; }

        [StringLength(100)]
        public string notes { get; set; }

        [StringLength(100)]
        public string interviewer { get; set; }
        public bool is_deleted { get; set; }
    }
}
