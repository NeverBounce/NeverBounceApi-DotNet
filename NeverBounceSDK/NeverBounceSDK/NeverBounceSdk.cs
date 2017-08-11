// Author: Mike Mollick <mike@neverbounce.com>
//
// Copyright (c) 2017 NeverBounce
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using NeverBounce.Services;

namespace NeverBounce
{
    public class NeverBounceSdk
    {
        public AccountService Account;
        private string ApiKey;
        private string Host = "https://api.neverbounce.com/v4";
        public JobsService Jobs;
        public POEService POE;
        public SingleService Single;

	    /// <summary>
	    ///     This method initializes the NeverBounceSDK
	    /// </summary>
	    /// <param name="ApiKey">The api key to use to make the requests</param>
	    /// <param name="Host">Specify a different host to make the request to. Leave null to use 'https://api.neverbounce.com'</param>
	    public NeverBounceSdk(string ApiKey, string Host = null)
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