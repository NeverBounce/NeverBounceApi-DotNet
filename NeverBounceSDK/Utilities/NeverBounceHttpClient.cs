﻿// Author: Mike Mollick <mike@neverbounce.com>
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

using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NeverBounce.Utilities
{
    public class NeverBounceHttpClient
    {
        private readonly string _apiKey;

        private readonly IHttpClient _client;

        private readonly string _host = "https://api.neverbounce.com/v4/";

        private string acceptedType = "application/json";

        /// <summary>
        ///     Creates the HttpClient instance as well as sets up the hostname to use
        /// </summary>
        /// <param name="Client">The instance of IHttpClient to use to make requests</param>
        /// <param name="ApiKey">The api key to use to authenticate requests</param>
        /// <param name="Host">The url to make the API request to</param>
        public NeverBounceHttpClient(IHttpClient Client, string ApiKey, string Host = null)
        {
            _client = Client;
            _client.GetRequestHeaders().Add("User-Agent", GenerateUserAgent());
            _apiKey = ApiKey;
            if (Host != null)
                _host = Host;
        }

        /// <summary>
        ///     This will set the expected data type for the response. If a different
        ///     data type is return an error will be thrown.
        /// </summary>
        /// <param name="type">The expected request data type</param>
        public void SetAcceptedType(string type)
        {
            acceptedType = type;
        }

        /// <summary>
        ///     Generates the useragent string
        /// </summary>
        private string GenerateUserAgent()
        {
            string loc = Assembly.GetExecutingAssembly().Location;
            string productVersion = FileVersionInfo.GetVersionInfo(loc).ProductVersion;
            return "NeverBounceApi-DotNet/" + productVersion;
        }

        /// <summary>
        ///     This method makes the actual request to the API
        ///     It currently only support GET and POST requests
        /// </summary>
        /// <param name="method">The HTTP method to use, either GET or POST</param>
        /// <param name="endpoint">The endpoint to request</param>
        /// <param name="model">The parameters to include with the request</param>
        public async Task<string> MakeRequest(string method, string endpoint, RequestModel model)
        {
            model.key = _apiKey;
            HttpResponseMessage response;

            // Handle GET && POST methods differently
            if (method.ToUpper() == "GET")
            {
                var uri = new Uri(_host + endpoint + "?" + ToQueryString(model));
                response = await _client.GetAsync(uri);
            }
            else
            {
                var uri = new Uri(_host + endpoint);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                response = await _client.PostAsync(uri, content);
            }

            return await ParseResponse(response);
        }

        protected async Task<string> ParseResponse(HttpResponseMessage response)
        {
            // Handle 5xx HTTP errors
            if (response.StatusCode.GetHashCode() > 500)
                throw new GeneralException(string.Format(
                    "We were unable to complete your request. "
                    + "The following information was supplied: {0}"
                    + "\n\n(Internal error[status {1}])", response.StatusCode, response.StatusCode.GetHashCode()
                ));

            // Handle 4xx HTTP errors
            if (response.StatusCode.GetHashCode() > 400)
                throw new GeneralException(string.Format(
                    "We were unable to complete your request. "
                    + "The following information was supplied: {0}"
                    + "\n\n(Request error[status {1}])", response.StatusCode, response.StatusCode.GetHashCode()
                ));

            var contentType = response.Content.Headers.ContentType.ToString();
            var data = await response.Content.ReadAsStringAsync();

            // Handle application/json responses
            if (contentType == "application/json")
            {
                JObject token;

                try
                {
                    token = JObject.Parse(data);
                }
                catch (Exception)
                {
                    throw new GeneralException(string.Format(
                        "The response from NeverBounce was unable "
                        + "to be parsed as json. Try the request "
                        + "again, if this error persists "
                        + "let us know at support@neverbounce.com. "
                        + "The following information was supplied: {0} "
                        + "\n\n(Internal error)", data));
                }

                // Handle non 'success' statuses
                var status = (string) token.SelectToken("status");
                if (status != "success")
                    switch (status)
                    {
                        case "auth_failure":
                            throw new AuthException(string.Format(
                                "We were unable to authenticate your request. "
                                + "The following information was supplied: {0} "
                                + "\n\n(auth_failure)", token.SelectToken("message")));

                        case "temp_unavail":
                            throw new GeneralException(string.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0} "
                                + "\n\n(temp_unavail)", token.SelectToken("message")));

                        case "throttle_triggered":
                            throw new ThrottleException(string.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0} "
                                + "\n\n(throttle_triggered)", token.SelectToken("message")));

                        case "bad_referrer":
                            throw new BadReferrerException(string.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0}"
                                + "\n\n(bad_referrer)", token.SelectToken("message")));

                        default:
                            throw new GeneralException(string.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0}"
                                + "\n\n({1})", token.SelectToken("message"), token.SelectToken("status")));
                    }
            }
            
            // Handle unexpected response types
            if (contentType != acceptedType)
            {
                throw new GeneralException(string.Format(
                    "The response from NeverBounce was has a data type of \"{0}\", but \"{1}\" was expected."
                    + "The following information was supplied: {2}"
                    + "\n\n(Internal error[status {3}])", contentType, acceptedType, response.StatusCode, response.StatusCode.GetHashCode()
                ));
            }

            // Return response data for passthrough/marshalling
            return data;
        }

        /// <summary>
        ///     Creates a urlencoded query string of the parameters
        /// </summary>
        /// <param name="request">The request parameters to encode</param>
        /// <param name="separator">The seperator to use for enumerated properties</param>
        public string ToQueryString(object request, string separator = ",")
        {
            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(
                    x => x.Name,
                    x => x.PropertyType.ToString().Contains("System.Boolean")
                        ? Convert.ToInt32(x.GetValue(request, null))
                        : x.GetValue(request, null)
                );

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var value = properties[key];
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                    ? valueType.GetGenericArguments()[0]
                    : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(",", enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}