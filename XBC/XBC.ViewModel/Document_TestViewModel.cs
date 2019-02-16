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

        public long test_id { get; set; }

        public long test_type_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal version { get; set; }

        [Required]
        [StringLength(10)]
        public string token { get; set; }

        public string TestType { get; set; }

        public string Test { get; set; }

    }
}
