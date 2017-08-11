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

namespace NeverBounce.Models
{
    public class ResponseModel
    {
        public string status { get; set; }
        public int execution_time { get; set; }
    }

    public class RawResponseModel
    {
        public object json { get; set; }
        public string plaintext { get; set; }
    }

    public class CreditsInfo
    {
        public int paid_credits_used { get; set; }
        public int free_credits_used { get; set; }
        public int paid_credits_remaining { get; set; }
        public int free_credits_remaining { get; set; }
    }

    public class AddressInfo
    {
        public string original_email { get; set; }
        public string normalized_email { get; set; }
        public string addr { get; set; }
        public string alias { get; set; }
        public string host { get; set; }
        public string fqdn { get; set; }
        public string domain { get; set; }
        public string subdomain { get; set; }
        public string tld { get; set; }
    }
}