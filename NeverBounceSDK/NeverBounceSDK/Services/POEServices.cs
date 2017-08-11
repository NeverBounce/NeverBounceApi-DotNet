using NeverBounce.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NeverBounce.Utilities;

namespace NeverBounce.Services
{
    public class POEService
    {
	    protected string _apiKey;

	    protected string _host;

	    protected IHttpClient _client;

	    public POEService(IHttpClient Client, string ApiKey, string Host = null)
	    {
		    _client = Client;
		    _apiKey = ApiKey;

		    // Accept debug host
		    if (Host != null)
			    _host = Host;
	    }

		/// <summary>
		/// Allows you to confirm front end (Javascript Widget) verification results
		/// See: "https://developers.neverbounce.com/v4.0/reference#widget-poe-confirm"
		/// </summary>
		/// <param name="model"> POEConfirmRequestModel</param>
		/// <returns>POEConfirmResponseModel</returns>
		public async Task<POEConfirmResponseModel> Confirm(POEConfirmRequestModel model)
        {
	        NeverBounceHttpClient client = new NeverBounceHttpClient(_client, _apiKey, _host);
			var result = await client.MakeRequest("POST", "/poe/confirm", model);
            return JsonConvert.DeserializeObject<POEConfirmResponseModel>(result);
        }
    }


}

