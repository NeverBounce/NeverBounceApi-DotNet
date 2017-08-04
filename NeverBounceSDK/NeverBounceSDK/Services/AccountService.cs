using NeverBounce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using NeverBounce.Utilities;
using NeverBounce.Interface;


namespace NeverBounce.Services
{
    class AccountService
    {
        private static string api = "/account/info?key=";
        static WebClient client = new WebClient();
        public static async Task<AccountInfoModel> AccountInfo(string serverAddress, string app_key)
        {
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            AccountInfoModel accountInfo = null;
            var result = await uitylity.GetNeverBounce(serverAddress + api, app_key);
            if (result.Status == "success")
            {
                accountInfo = JsonConvert.DeserializeObject<AccountInfoModel>(result.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return accountInfo;
        }
    }


}

