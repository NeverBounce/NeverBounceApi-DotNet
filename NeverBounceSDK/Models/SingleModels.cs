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

using System.Collections.Generic;
using System.Linq;

namespace NeverBounce.Models
{
    public enum ResultCodes
    {
        valid,
        invalid,
        disposable,
        catchall,
        unknown
    }

    public class SingleResponseModel : ResponseModel
    {
        public string result { get; set; }
        public List<string> flags { get; set; }
        public string suggested_correction { get; set; }
        public string retry_token { get; set; }
        public CreditsInfo credits_info { get; set; }
        public AddressInfo address_info { get; set; }

        public bool ResultIs(string resultCode)
        {
            return result.ToLower() == resultCode.ToLower();
        }

        public bool ResultIs(IEnumerable<string> resultCodes)
        {
            resultCodes = resultCodes.Select(c => c.ToLower());
            return resultCodes.Contains(result.ToLower());
        }

        public bool ResultIsNot(string resultCode)
        {
            return result.ToLower() != resultCode.ToLower();
        }

        public bool ResultIsNot(IEnumerable<string> resultCodes)
        {
            resultCodes = resultCodes.Select(c => c.ToLower());
            return !resultCodes.Contains(result.ToLower());
        }
    }

    public class SingleRequestModel : RequestModel
    {
        public string email { get; set; }
        public bool? address_info { get; set; } = false;
        public bool? credits_info { get; set; } = false;
        public int? timeout { get; set; }
        public RequestMetaDataModel request_meta_data { get; set; } = new RequestMetaDataModel();
    }
}