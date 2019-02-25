using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class MenuAccessViewModel
    {
        public long id { get; set; }

        [Display(Name = "Menu ID")]
        public long menu_id { get; set; }

        [Display(Name = "Role ID")]
        public long role_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public string role_name { get; set; }
        public string menu_name { get; set; }
    }
}
