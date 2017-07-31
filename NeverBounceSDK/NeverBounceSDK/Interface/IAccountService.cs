using NeverBounce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Interface
{
   public interface IAccountService
    {
        AccountInfoModel AccountInfo(string serverAddress, string app_key);
    }
}
