using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class Document_Test_DetailViewModel
    {
        public long id { get; set; }

        public long question_id { get; set; }

        [Required, StringLength(5)]
        public string questionType { get; set; }

        public long document_test_id { get; set; }

        public long test_id { get; set; }

        [Required, StringLength(255)]
        public string question { get; set; }

        [StringLength(255)]
        public string optionA { get; set; }

        [StringLength(255)]
        public string optionB { get; set; }

        [StringLength(255)]
        public string optionC { get; set; }

        [StringLength(255)]
        public string optionD { get; set; }

        [StringLength(255)]
        public string optionE { get; set; }

        [StringLength(255)]
        public string imageUrl { get; set; }

        [StringLength(255)]
        public string imageA { get; set; }

        [StringLength(255)]
        public string imageB { get; set; }

        [StringLength(255)]
        public string imageC { get; set; }

        [StringLength(255)]
        public string imageD { get; set; }

        [StringLength(255)]
        public string imageE { get; set; }

        public long UserId { get; set; }

    }
}
