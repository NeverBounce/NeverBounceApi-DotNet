﻿using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NeverBounce.Services
{
    public class SingleService
    {
		protected string _apiKey;

		protected string _host;

        protected IHttpClient _client;

        public SingleService(IHttpClient Client, string ApiKey, string Host = null)
		{
			_client = Client;
			_apiKey = ApiKey;

			// Accept debug host
			if (Host != null)
				_host = Host;
		}

		/// <summary>
		/// Single verification allows you verify individual emails and gather additional information pertaining to the email.
		/// See: "https://developers.neverbounce.com/v4.0/reference#single-check"
		/// </summary>
		/// <param name="model"> SingleRequestModel</param>
		/// <returns>SingleResponseModel</returns>
		public async Task<SingleResponseModel> Check(SingleRequestModel model)
        {
            NeverBounceHttpClient client = new NeverBounceHttpClient(_client, _apiKey, _host);
            var result = await client.MakeRequest("GET", "/single/check", model);
            return JsonConvert.DeserializeObject<SingleResponseModel>(result.json.ToString());
        }
    }
}
