using System;
using NeverBounce.Services;

namespace NeverBounce
{
   public class NeverBounceSdk
    {
        private string ApiKey;
        private String Host = "https://api.neverbounce.com/v4";

        public AccountService Account;
        public JobsService Jobs;
        public POEService POE;
        public SingleService Single;

		/// <summary>
		/// This method initializes the NeverBounceSDK
		/// </summary>
		/// <param name="ApiKey">The api key to use to make the requests</param>
        /// <param name="Host">Specify a different host to make the request to. Leave null to use 'https://api.neverbounce.com'</param>
		public  NeverBounceSdk(string ApiKey, string Host = null)
        {
            this.ApiKey = ApiKey;

            // Accept debug host
            if (Host != null)
                this.Host = Host;

            Account = new AccountService(ApiKey, Host);
            Jobs = new JobsService(ApiKey, Host);
            POE = new POEService(ApiKey, Host);
			Single = new SingleService(ApiKey, Host);
		}
    }
}
