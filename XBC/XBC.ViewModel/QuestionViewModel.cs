using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XBC.ViewModel
{
    public partial class QuestionViewModel 
    {
        public long id { get; set; }

        [Required, StringLength(5)]
        public string questionType { get; set; }

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

        public HttpPostedFileBase imgurl { get; set; }
        public HttpPostedFileBase imga { get; set; }
        public HttpPostedFileBase imgb { get; set; }
        public HttpPostedFileBase imgc { get; set; }
        public HttpPostedFileBase imgd { get; set; }
        public HttpPostedFileBase imge { get; set; }

    }
}
