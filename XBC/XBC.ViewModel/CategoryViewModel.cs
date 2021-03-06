﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class CategoryViewModel
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Code")]
        public string code { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description")]
        public string description { get; set; }

        public long UserId { get; set; }


    }
}
