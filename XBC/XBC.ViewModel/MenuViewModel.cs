using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class MenuViewModel
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string code { get; set; }

        
        public string title { get; set; }

        [StringLength(255)]
        public string descriptoin { get; set; }

        [Display(Name = "imageUrl")]
        public string image_url { get; set; }
        [Display(Name = "menuOrder")]
        public long menu_order { get; set; }

        [Display(Name = "menuParent")]
        public long? menu_parent { get; set; }

        [Display(Name = "menuUrl")]
        public string menu_url { get; set; }
    }
}
