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
               
        public long menu_id { get; set; }
               
        public long role_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        [Display(Name = "ROLE")]
        public string role_name { get; set; }
        [Display(Name = "MENU")]
        public string menu_name { get; set; }
        public long UserId { get; set; }
    }
}
