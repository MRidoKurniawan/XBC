using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public object Entity { get; set; }
    }
}
