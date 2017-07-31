using NeverBounce.Models;
using Newtonsoft.Json;
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
            //this.ApiKey = ApiKey;
            this.ServerAddress = _ServerAddress;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<object> GetNeverBounce(String Method,string model)
        {
            try
            {
                var content = new StringContent("", Encoding.UTF8, "application/json");
                var uri = new Uri(Method+model);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<object>(data); ;
                }
                else
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<object>(data); ;
                }
               
            }
            catch (Exception ex)
            {
                Log.WriteLog(": " + ex.Message);
                return null;
            }
        }
      
        public async Task<object> PostNeverBounce(String Method, string model)
        {
            try
            {
               
                var content = new StringContent(model, Encoding.UTF8, "application/json");
                var uri = new Uri(Method);
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<object>(data);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(": " + ex.Message);
                return null;
            }
        }

        public async Task<string> PostNeverBouncedownload(String Method, string model)
        {
            try
            {
                var content = new StringContent(model, Encoding.UTF8, "application/json");
                var uri = new Uri(Method);
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(": " + ex.Message);
                return null;
            }
        }

    }
   
}
