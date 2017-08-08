using NeverBounce.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NeverBounce.Utilities;

namespace NeverBounce.Services
{
    public class POEService
    {
		protected string ApiKey;

		protected string Host;

		public POEService(string ApiKey, string Host = null)
		{
			this.ApiKey = ApiKey;

			// Accept debug host
			if (Host != null)
				this.Host = Host;
		}

		/// <summary>
		/// Allows you to confirm front end (Javascript Widget) verification results
		/// See: "https://developers.neverbounce.com/v4.0/reference#widget-poe-confirm"
		/// </summary>
		/// <param name="model"> POEConfirmRequestModel</param>
		/// <returns>POEConfirmResponseModel</returns>
		public async Task<POEConfirmResponseModel> Confirm(POEConfirmRequestModel model)
        {
			NeverBounceHttpClient client = new Utilities.NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("POST", "/poe/confirm", model);
            return JsonConvert.DeserializeObject<POEConfirmResponseModel>(result.json.ToString());
        }
    }


}

