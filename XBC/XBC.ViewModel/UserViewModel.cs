using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class UserViewModel
    {
        public long id { get; set; }

        [Display(Name = "username")]
        public string username { get; set; }

        [Display(Name = "password")]
        public string password { get; set; }

        [Display(Name = "email")]
        public string email { get; set; }
        [Display(Name = "roleId")]
        public long role_id { get; set; }

        [Display(Name = "Role")]
        public string role_name { get; set; }
        [Display(Name = "mobileFlag")]
        public bool mobile_flag { get; set; }
        [Display(Name = "mobileToken")]
        public long? mobile_token { get; set; }
    }
}
