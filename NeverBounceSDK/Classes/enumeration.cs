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

using System;

namespace NeverBounce.Classes
{
    public class enumeration
    {
        public static string StatusCodeDescription(string StatusCode)
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
                        return
                            "The request couldn't be authenticated; check the API key and make sure it's being sent correctly";
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

        public static string ResultCodeDescription(string ResultCode)
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

        public static string FlagDescription(string ResultCode)
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