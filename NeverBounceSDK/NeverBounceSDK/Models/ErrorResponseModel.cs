using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Models
{

    public class ErrorResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int execution_time { get; set; }
    }
}
