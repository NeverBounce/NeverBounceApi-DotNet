using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Models
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string downloadResponse { get; set; }

		public object json { get; set; }
		public string plaintext { get; set;  }
    }
}
