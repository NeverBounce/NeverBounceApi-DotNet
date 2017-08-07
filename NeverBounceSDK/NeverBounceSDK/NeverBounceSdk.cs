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
        public SingleService Single;

		public  NeverBounceSdk(string ApiKey, string Host = null)
        {
            this.ApiKey = ApiKey;

            // Accept debug host
            if (Host != null)
                this.Host = Host;

            Account = new AccountService(ApiKey, Host);
            Jobs = new JobsService(ApiKey, Host);
			Single = new SingleService(ApiKey, Host);
		}
    }
}
