﻿using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NeverBounce.Services
{
    public class SingleService
    {
		protected string ApiKey;

		protected string Host;

		public SingleService(string ApiKey, string Host = null)
		{
			this.ApiKey = ApiKey;

			// Accept debug host
			if (Host != null)
				this.Host = Host;
		}

		/// <summary>
		/// Single verification allows you verify individual emails and gather additional information pertaining to the email.
		/// See: "https://api.neverbounce.com/v4/single/check"
		/// </summary>
		/// <param name="model"> SingleRequestModel</param>
		/// <returns>SingleResponseModel</returns>
		public async Task<SingleResponseModel> Check(SingleRequestModel model)
        {
            NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
            var result = await client.MakeRequest("GET", "/single/check", model);
            return JsonConvert.DeserializeObject<SingleResponseModel>(result.json.ToString());
        }
    }
}
