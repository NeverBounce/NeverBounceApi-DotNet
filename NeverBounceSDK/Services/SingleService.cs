using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;

namespace NeverBounce.Services
{
    public class SingleService
    {
        protected string _apiKey;

        protected IHttpClient _client;

        protected string _host;

        public SingleService(IHttpClient Client, string ApiKey, string Host = null)
        {
            this._client = Client;
            this._apiKey = ApiKey;

            // Accept debug host
            if (Host != null)
                this._host = Host;
        }

        /// <summary>
        ///     Single verification allows you verify individual emails and gather additional information pertaining to the email.
        ///     See: "https://developers.neverbounce.com/v4.0/reference#single-check"
        /// </summary>
        /// <param name="model"> SingleRequestModel</param>
        /// <returns>SingleResponseModel</returns>
        public async Task<SingleResponseModel> Check(SingleRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("GET", "/single/check", model);
            return JsonConvert.DeserializeObject<SingleResponseModel>(result);
        }
    }
}