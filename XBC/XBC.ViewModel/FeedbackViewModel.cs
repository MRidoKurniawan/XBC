using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XBC.ViewModel
{
    public class FeedbackViewModel
    {
        public long id { get; set; }

        public long test_id { get; set; }

        public long document_test_id { get; set; }

        [StringLength(5000)]
        public string json_feedback { get; set; }

        public string Nama { get; set; }

        public List<JsonFeedbackViewModel> Feedback { get; set; }

    }
}
