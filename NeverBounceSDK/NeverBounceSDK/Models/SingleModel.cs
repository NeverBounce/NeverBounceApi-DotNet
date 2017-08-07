using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Models
{
      
    public class SingleModel : ResponseModel
    {
        public string status { get; set; }
        public string result { get; set; }
        public List<string> flags { get; set; }
        public string suggested_correction { get; set; }
        public string retry_token { get; set; }
        public CreditsInfo credits_info { get; set; }
        public int execution_time { get; set; }
        public string message { get; set; }

    }
    public class SingleRequestModel : RequestModel
    {       
        public string email { get; set; }
        public Nullable<bool> address_info { get; set; } = false;
        public Nullable<bool> credits_info { get; set; } = false;
        public Nullable<int> timeout { get; set; }
    }
  
}
