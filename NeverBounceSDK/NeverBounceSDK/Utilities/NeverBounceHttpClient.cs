using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NeverBounce.Models;
using NeverBounce.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NeverBounce.Utilities
{
    public class NeverBounceHttpClient
    {

		private HttpClient client;

		private String host = "https://api.neverbounce.com/v4/";

        private String apiKey;

        /// <summary>
        /// Creates the HttpClient instance as well as sets up the hostname to use
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="host"></param>
        public NeverBounceHttpClient(String apiKey, String host = null)
        {
            this.apiKey = apiKey;
            if(host != null)
                this.host = host;
            
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

		/// <summary>
		/// This method makes the actual request to the API
        /// It currently only support GET and POST requests
		/// </summary>
		/// <param name="method">The HTTP method to use, either GET or POST</param>
		/// <param name="endpoint">The endpoint to request</param>
		/// <param name="model">The parameters to include with the request</param>
		public async Task<RawResponseModel> MakeRequest(String method, String endpoint, RequestModel model)
		{
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            model.key = apiKey;
			HttpResponseMessage response;

            // Handle GET && POST methods differently
			if (method.ToUpper() == "GET")
			{
                Uri uri = new Uri(host + endpoint + "?" + ToQueryString(model));
				response = await client.GetAsync(uri);
			}
			else
			{
                Uri uri = new Uri(host + endpoint);
				StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
				response = await client.PostAsync(uri, content);
			}

            return await ParseResponse(response);
		}

        protected async Task<RawResponseModel> ParseResponse(HttpResponseMessage response)
        {
            String contentType = response.Content.Headers.ContentType.ToString();
			String data = await response.Content.ReadAsStringAsync();

            // Handle 5xx HTTP errors
            if(response.StatusCode.GetHashCode() > 500) {
				throw new HttpClientException(String.Format(
					"We were unable to complete your request. "
					+ "The following information was supplied: {0}"
					+ "\n\n(Internal error[status {1}])", response.StatusCode.ToString(), response.StatusCode.GetHashCode()
				));
            }

            // Handle 4xx HTTP errors
            if(response.StatusCode.GetHashCode() > 400) {
                throw new HttpClientException(String.Format(
					"We were unable to complete your request. "
                    + "The following information was supplied: {0}"
                    + "\n\n(Request error[status {1}])", response.StatusCode.ToString(), response.StatusCode.GetHashCode()
                ));
            }

            // Handle application/json responses
			if(contentType == "application/json") {
                JObject token;

                try
                {
                    token = JObject.Parse(data);
                } catch (Exception) {
                    throw new HttpClientException(String.Format(
                        "The response from NeverBounce was unable "
						+ "to be parsed as json. Try the request "
						+ "again, if this error persists "
                        + "let us know at support@neverbounce.com. "
						+ "The following information was supplied: {0} "
						+ "\n\n(Internal error)", data));
                }

                // Handle non 'success' statuses
				String status = (string) token.SelectToken("status");
                if(status != "success") {
                    switch(status) {
						case "auth_failure":
                            throw new AuthException(String.Format(
                                "We were unable to authenticate your request. "
                                + "The following information was supplied: {0} "
                                + "\n\n(auth_failure)", token.SelectToken("message")));
                            
						case "temp_unavail":
                            throw new GeneralException(String.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0} "
                                + "\n\n(temp_unavail)", token.SelectToken("message")));
                            
						case "throttle_triggered":
                            throw new ThrottleException(String.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0} "
                                + "\n\n(throttle_triggered)", token.SelectToken("message")));
                            
						case "bad_referrer":
                            throw new BadReferrerException(String.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0}"
                                + "\n\n(bad_referrer)", token.SelectToken("message")));
                            
						default:
                            throw new GeneralException(String.Format(
                                "We were unable to complete your request. "
                                + "The following information was supplied: {0}"
                                + "\n\n({1})", token.SelectToken("message"), token.SelectToken("status")));
                    }
                }

                // Return good json responses in ResponseModel
				return new RawResponseModel { json = JsonConvert.DeserializeObject<object>(data) };
            }

            // Handle plain text/stream type responses
            return new RawResponseModel { plaintext = data };
        }

		/// <summary>
		/// Creates a urlencoded query string of the parameters
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
										? Convert.ToInt32(x.GetValue(request, null)) : x.GetValue(request, null)
							   );

			// Get names for all IEnumerable properties (excl. string)
			var propertyNames = properties
				.Where(x => !(x.Value is string) && x.Value is IEnumerable)
				.Select(x => x.Key)
				.ToList();

			// Concat all IEnumerable properties into a comma separated string
			foreach (var key in propertyNames)
			{
				var valueType = properties[key].GetType();
				var valueElemType = valueType.IsGenericType
										? valueType.GetGenericArguments()[0]
										: valueType.GetElementType();
				if (valueElemType.IsPrimitive || valueElemType == typeof(string))
				{
					var enumerable = properties[key] as IEnumerable;
					properties[key] = string.Join(separator, enumerable.Cast<object>());
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
