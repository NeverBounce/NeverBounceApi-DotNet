using NeverBounce.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NeverBounce.Utilities;

namespace NeverBounce.Services
{
    public class AccountService
    {
	    protected string _apiKey;

	    protected string _host;

	    protected IHttpClient _client;

	    public AccountService(IHttpClient Client, string ApiKey, string Host = null)
	    {
		    _client = Client;
		    _apiKey = ApiKey;

		    // Accept debug host
		    if (Host != null)
			    _host = Host;
	    }

		/// <summary>
		/// Account Info method allow to programmatically check your account's balance and how many jobs are currently running on your account.
		/// See: "https://developers.neverbounce.com/v4.0/reference#account-info"
		/// </summary>
		/// <returns>AccountInfoResponseModel</returns>
		public async Task<AccountInfoResponseModel> Info()
        {
            RequestModel model = new RequestModel();
	        NeverBounceHttpClient client = new NeverBounceHttpClient(_client, _apiKey, _host);
			var result = await client.MakeRequest("POST", "/account/info", model);
            return JsonConvert.DeserializeObject<AccountInfoResponseModel>(result.json.ToString());
        }
    }


}

