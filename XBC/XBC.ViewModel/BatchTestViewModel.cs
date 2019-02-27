using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BatchTestViewModel
    {
        public long id { get; set; }

        public long batch_id { get; set; }

        public long test_id { get; set; }

        public long UserId { get; set; }
    }
}
