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
using NeverBounce.Utilities;

namespace NeverBounce
{
    public class NeverBounceSdk
    {
        private const string _host = "https://api.neverbounce.com";
        private readonly string _apiKey;
        private readonly IHttpClient _client;
        private readonly string _version;
        
        public readonly AccountService Account;
        public readonly JobsService Jobs;
        public readonly POEService POE;
        public readonly SingleService Single;

        /// <summary>
        ///     This method initializes the NeverBounceSDK
        /// </summary>
        /// <param name="ApiKey">The api key to use to make the requests</param>
        /// <param name="Version">The api version to make this request on</param>
        /// <param name="Host">Specify a different host to make the request to. Leave null to use 'https://api.neverbounce.com'</param>
        /// <param name="Client">An instance of IHttpClient to use; useful for mocking HTTP requests</param>
        public NeverBounceSdk(string ApiKey, string Version = "v4.2", string Host = null, IHttpClient Client = null)
        {
            _apiKey = ApiKey;
            _version = Version;
            
            // Check for mocked IHttpClient, if none exists create default
            _client = Client ?? new HttpClientWrapper();
            
            var url = $"{Host ?? _host}/{_version}";
            
            Account = new AccountService(_client, _apiKey, url);
            Jobs = new JobsService(_client, _apiKey, url);
            POE = new POEService(_client, _apiKey, url);
            Single = new SingleService(_client, _apiKey,  url);
        }
    }
}