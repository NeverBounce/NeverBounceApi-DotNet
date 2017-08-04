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
        /// <summary>
        /// Account Info method allow to programmatically check your account's balance and how many jobs are currently running on your account.
        /// </summary>
        /// <param name="serverAddress">containg api url like https://api.neverbounce.com/v4 </param>
        /// <param name="app_key">this parameter authenticate your requests</param>
        /// <returns>AccountInfoModel</returns>
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

