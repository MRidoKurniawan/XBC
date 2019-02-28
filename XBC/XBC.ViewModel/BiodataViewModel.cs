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

        [Required, StringLength(255)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [StringLength(5)]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Last_Education")]
        public string last_education { get; set; }

        [Required, StringLength(5)]
        [Display(Name = "Graduation_Year")]
        public string graduation_year { get; set; }

        [Required, StringLength(5)]
        [Display(Name = "Educational_Level")]
        public string educational_level { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Majors")]
        public string majors { get; set; }

        [Required, StringLength(5)]
        [Display(Name = "GPA")]
        public string gpa { get; set; }

        [Display(Name = "Bootcamp_Test_Type")]
        public long? bootcamp_test_type { get; set; }

        [Display(Name = "IQ")]
        public long? iq { get; set; }

        [StringLength(10)]
        [Display(Name = "DU")]
        public string du { get; set; }

        [Display(Name = "Arithmetic")]
        public long? arithmetic { get; set; }

        [Display(Name = "NL")]
        public long? nested_logic { get; set; }

        [Display(Name = "JT")]
        public long? join_table { get; set; }

        [StringLength(50)]
        [Display(Name = "TRO")]
        public string tro { get; set; }

        [StringLength(100)]
        [Display(Name = "Notes")]
        public string notes { get; set; }

        [StringLength(100)]
        [Display(Name = "Interviewer")]
        public string interviewer { get; set; }
        public long UserId { get; set; }
        public bool is_deleted { get; set; }
    }
}
