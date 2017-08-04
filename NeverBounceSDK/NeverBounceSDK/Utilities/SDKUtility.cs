using NeverBounce.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Utilities
{

    public class SDKUtility
    {
        private string ApiKey;
        private HttpClient client;
        private String ServerAddress = "https://api.neverbounce.com/v4/";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ServerAddress"></param>
        /// <param name="ApiKey"></param>
        public SDKUtility(String _ServerAddress)
        {
            this.ServerAddress = _ServerAddress;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<ResponseModel> GetNeverBounce(String Method, string model)
        {
            try
            {
                var content = new StringContent("", Encoding.UTF8, "application/json");
                var uri = new Uri(Method + model);
                var response = await client.GetAsync(uri);
                var data = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(data);
                var status = (string)token.SelectToken("status");
                return new ResponseModel { Data = JsonConvert.DeserializeObject<object>(data), Status = status };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Message = ex.Message, Status = "Fail" };
            }
        }

        public async Task<ResponseModel> PostNeverBounce(String Method, string model)
        {
            try
            {

                var content = new StringContent(model, Encoding.UTF8, "application/json");
                var uri = new Uri(Method);
                var response = await client.PostAsync(uri, content);
                var data = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(data);
                var status = (string)token.SelectToken("status");
                return new ResponseModel { Data = JsonConvert.DeserializeObject<object>(data), Status = status };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Message = ex.Message, Status = "Fail" };
            }
        }

        public async Task<ResponseModel> PostNeverBouncedownload(String Method, string model)
        {
            try
            {
                var content = new StringContent(model, Encoding.UTF8, "application/json");
                var uri = new Uri(Method);
                var response = await client.PostAsync(uri, content);
                var data = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(data);
                var status = (string)token.SelectToken("status");
                return new ResponseModel { downloadResponse = JsonConvert.DeserializeObject<string>(data), Status = status };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Message = ex.Message, Status = "Fail" };
            }
        }

    }

}
