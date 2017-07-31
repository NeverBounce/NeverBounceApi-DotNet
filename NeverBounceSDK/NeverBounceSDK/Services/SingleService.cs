using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Services
{
    class SingleService
    {
        private static string api = "/single/check";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static async Task<SingleModel> Check(string serverAddress, SingleRequestModel model)
        {
            try
            {
            SingleModel singleCheck = null;
                if (Validation.SinglecheckValidation(model.email))
                {
                    var json = ConvertDictionary(model);
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                    var result = await uitylity.GetNeverBounce(serverAddress + api, GenerateQuerstring(json));
                    singleCheck = JsonConvert.DeserializeObject<SingleModel>(result.ToString());
                }
                else
                {
                    singleCheck = new SingleModel();
                    singleCheck.status = "general_failure";
                    singleCheck.message = "Missing required parameters or In valid email address";
                }
                return singleCheck;
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }
        private static string ConvertDictionary(SingleRequestModel model)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (model.key!=null)
            {
                data["key"] = model.key;
            }
            if (model.email != null)
            {
                data["email"] = model.email;
            }
            if (model.address_info<=0 || model.address_info != null)
            {
                data["address_info"] = model.address_info;
            }
            if (model.credits_info<= 0|| model.credits_info!=null)
            {
                data["credits_info"] = model.credits_info;
            }

            if (model.timeout <= 0|| model.timeout!=null)
            {
                data["timeout"] = model.timeout;
            }
            
            
            //var json = JsonConvert.SerializeObject(model);
            return JsonConvert.SerializeObject(data);
        }

        private static string GenerateQuerstring(string parameters)
        {
            //var json = JsonConvert.SerializeObject(model);
            string str = "?";
            str += parameters.Replace(":", "=").Replace("{", "").
                        Replace("}", "").Replace(",", "&").
                            Replace("\"", "");

            return str;
        }
    }
}
