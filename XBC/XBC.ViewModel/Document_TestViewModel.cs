using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class Document_TestViewModel
    {
        public long id { get; set; }

        [Display(Name = "Test")]
        public long test_id { get; set; }

        [Display(Name = "TestType")]
        public long test_type_id { get; set; }

        [Column(TypeName = "numeric")]
        [Display(Name = "Version")]
        public decimal version { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Token")]
        public string token { get; set; }

        public string TestType { get; set; }

        public string Test { get; set; }

        public List<Document_Test_DetailViewModel> Details { get; set; }

        public int noUrut { get; set; }

        public long UserId { get; set; }

    }
}
