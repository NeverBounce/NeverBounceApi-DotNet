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
using System.Threading.Tasks;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class JobsEndpoint
    {
        public static async Task<JobSearchResponseModel> Search(NeverBounceSdk sdk)
        {
            var model = new JobSearchRequestModel();
            model.job_id = 288025;
            return await sdk.Jobs.Search(model);
        }

        public static async Task<JobCreateResponseModel> CreateSuppliedData(NeverBounceSdk sdk)
        {
            var model = new JobCreateSuppliedDataRequestModel();
            model.filename = "Created From dotNET.csv";
            model.auto_parse = true;
            model.auto_start = false;
            var data = new List<object>();
            data.Add(new {id = "3", email = "support@neverbounce.com", name = "Fred McValid"});
            data.Add(new {id = "4", email = "invalid@neverbounce.com", name = "Bob McInvalid"});
            model.input = data;
            return await sdk.Jobs.CreateFromSuppliedData(model);
        }

        public static async Task<JobCreateResponseModel> CreateRemoteUrl(NeverBounceSdk sdk)
        {
            var model = new JobCreateRemoteUrlRequestModel();
            model.filename = "Created From dotNET.csv";
            model.auto_parse = true;
            model.auto_start = false;
            model.input = "https://example.com/file.csv";
            return await sdk.Jobs.CreateFromRemoteUrl(model);
        }

        public static async Task<JobParseResponseModel> Parse(NeverBounceSdk sdk)
        {
            var model = new JobParseRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Parse(model);
        }

        public static async Task<JobStartResponseModel> Start(NeverBounceSdk sdk)
        {
            var model = new JobStartRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Start(model);
        }

        public static async Task<JobStatusResponseModel> Status(NeverBounceSdk sdk)
        {
            var model = new JobStatusRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Status(model);
        }

        public static async Task<JobResultsResponseModel> Results(NeverBounceSdk sdk)
        {
            var model = new JobResultsRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Results(model);
        }

        public static async Task<string> Download(NeverBounceSdk sdk)
        {
            var model = new JobDownloadRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Download(model);
        }

        public static async Task<JobDeleteResponseModel> Delete(NeverBounceSdk sdk)
        {
            var model = new JobDeleteRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Delete(model);
        }
    }
}
