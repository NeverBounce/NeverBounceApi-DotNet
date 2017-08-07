﻿using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

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
        ///Single verification allows you verify individual emails and gather additional information pertaining to the email.
        /// </summary>
        /// <param name="parameter">emailid required </param>
        ///   <param name="parameter">address_info </param>
        ///   <param name="parameter">credits_info </param>
        ///   <param name="parameter">timeout </param>
        /// <returns>SingleModel</returns>
        public async Task<SingleModel> Check(SingleRequestModel model)
        {
            NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
            var result = await client.MakeRequest("GET", "/single/check", model);
            return JsonConvert.DeserializeObject<SingleModel>(result.json.ToString());
        }
    }
}
