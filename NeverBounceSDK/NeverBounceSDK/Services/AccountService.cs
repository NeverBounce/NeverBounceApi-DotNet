using NeverBounce.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NeverBounce.Utilities;

namespace NeverBounce.Services
{
    public class AccountService
    {
		protected string ApiKey;

		protected string Host;

		public AccountService(string ApiKey, string Host = null)
		{
			this.ApiKey = ApiKey;

			// Accept debug host
			if (Host != null)
				this.Host = Host;
		}

        /// <summary>
        /// Account Info method allow to programmatically check your account's balance and how many jobs are currently running on your account.
        /// </summary>
        /// <param name="serverAddress">containg api url like https://api.neverbounce.com/v4 </param>
        /// <param name="app_key">this parameter authenticate your requests</param>
        /// <returns>AccountInfoModel</returns>
        public async Task<AccountInfoResponseModel> Info()
        {
            RequestModel model = new RequestModel();
			NeverBounceHttpClient client = new Utilities.NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("POST", "/account/info", model);
            return JsonConvert.DeserializeObject<AccountInfoResponseModel>(result.json.ToString());
        }
    }


}

