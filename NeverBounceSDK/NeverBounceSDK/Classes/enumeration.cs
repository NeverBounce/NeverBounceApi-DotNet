using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Classes
{
    public class enumeration
    {
        public static string StatusCodeDescription(String StatusCode)
        {
            try
            {
                switch (StatusCode)
                {
                    case "success":
                        return "The request was successful";
                    case "general_failure":
                        return "Something went wrong with the request; check the message property for further details";
                    case "auth_failure":
                        return "The request couldn't be authenticated; check the API key and make sure it's being sent correctly";
                    case "temp_unavail":
                        return "An internal error has occurred; typically this indicates a service interruption";
                    case "throttle_triggered":
                        return "The request was rejected due to rate limiting; try again shortly";
                    case "bad_referrer":
                        return "The referrer for this request is not trusted";
                    default:
                        return "Exception";

                }
            }
            catch (Exception ex)
            {
                return "Exception : " + ex.Message;
            }

        }

        public static string ResultCodeDescription(String ResultCode)
        { 
            try
            {
                switch (ResultCode)
                {
                    case "valid":
                        return "Verified as real address";
                    case "invalid":
                        return "Verified as not valid";
                    case "disposable":
                        return "A temporary, disposable address";
                    case "catchall":
                        return " A domain-wide setting";
                    case "unknown":
                        return "The server cannot be reached";
                    default:
                        return "Exception";

                }
            }
            catch (Exception ex)
            {
                return "Exception : " + ex.Message;
            }

        }

        public static string FlagDescription(String ResultCode)
        {
            try
            {
                switch (ResultCode)
                {
                    case "has_dns":
                        return "NA";
                    case "has_dns_mx":
                        return "NA";
                    case "bad_syntax":
                        return "The input given doesn't appear to be an email.";
                    case "free_email_host":
                        return "This email is registered on a free-mail host. (e.g: yahoo.com, hotmail.com)"; 
                    default:
                        return "Exception";

                }
            }
            catch (Exception ex)
            {
                return "Exception : " + ex.Message;
            }

        }
    }
}
