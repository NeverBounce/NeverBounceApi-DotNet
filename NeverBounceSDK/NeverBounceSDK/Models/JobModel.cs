using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Models
{
    public class JobSearchRequestModel : RequestModel
    {
        public int job_id { get; set; }
        public string filename { get; set; }
        public string job_status { get; set; }
        public int page { get; set; } = 1;
        public int items_per_page { get; set; } = 10;
    }

    public class JobSearchResponseModel : ResponseModel
    {
        public string status { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public JobSearchQuery query { get; set; }
        public List<JobSearchResult> results { get; set; }
        public int execution_time { get; set; }
    }

    public class JobSearchQuery
    {
        public int page { get; set; }
        public int items_per_page { get; set; }
        public int job_id { get; set; }

    }

    public class Total
    {
        public Nullable<int> records { get; set; }
        public Nullable<int> billable { get; set; }
        public Nullable<int> processed { get; set; }
        public Nullable<int> valid { get; set; }
        public Nullable<int> invalid { get; set; }
        public Nullable<int> catchall { get; set; }
        public Nullable<int> disposable { get; set; }
        public Nullable<int> unknown { get; set; }
        public Nullable<int> duplicates { get; set; }
        public Nullable<int> bad_syntax { get; set; }
    }

    public class JobSearchResult : ResponseModel
    {
        public int id { get; set; }
        public string job_status { get; set; }
        public string filename { get; set; }
        public string created_at { get; set; }
        public object started_at { get; set; }
        public object finished_at { get; set; }
        public Total total { get; set; }
        public int bounce_estimate { get; set; }
        public int percent_complete { get; set; }
    }

    public class JobCreateRequestModel : RequestModel
    {
        public string input_location { get; set; }
        public string filename { get; set; }
        public bool auto_start { get; set; } = false;
        public bool auto_parse { get; set; } = false;
		public bool run_sample { get; set; } = false;
 		public List<object> input { get; set; }

    }
    public class JobCreateResponseModel : ResponseModel
    {
        public string status { get; set; }
        public int job_id { get; set; }
        
        public int execution_time { get; set; }
    }

    public class JobParseRequestModel : RequestModel
    {
        public int job_id { get; set; }
        public bool auto_start { get; set; } = false;
    }

    public class JobParseResponseModel : ResponseModel
    {
        public string status { get; set; }
        public string queue_id { get; set; }
        public int execution_time { get; set; }
        
    }

    public class JobStartRequestModel : RequestModel
    {
        public int job_id { get; set; }
        public string run_sample { get; set; }
    }

    public class JobStartResponseModel : ResponseModel
    {

        public string status { get; set; }
        public string queue_id { get; set; }
        public int execution_time { get; set; }
    }

    public class JobStatusRequestModel : RequestModel
    {
        public int job_id { get; set; }
    }

    public class JobStatusResponseModel : ResponseModel
    {
        public string status { get; set; }
        public int id { get; set; }
        public string job_status { get; set; }
        public string filename { get; set; }
        public string created_at { get; set; }
        public string started_at { get; set; }
        public string finished_at { get; set; }
        public Total total { get; set; }
        public int bounce_estimate { get; set; }
        public int percent_complete { get; set; }
        public int execution_time { get; set; }
    }

    public class JobResultsRequestModel : RequestModel
    {
        public int job_id { get; set; }
        public int page { get; set; } = 1;
        public int items_per_page { get; set; } = 10;
    }

    public class JobResultsResponseModel : ResponseModel 
    {
        public string status { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public Query query { get; set; }
        public List<Result> results { get; set; }
        public int execution_time { get; set; }
       
    }

    public class Query
    {
        public int job_id { get; set; }
        public int valids { get; set; }
        public int invalids { get; set; }
        public int disposables { get; set; }
        public int catchalls { get; set; }
        public int unknowns { get; set; }
        public int page { get; set; }
        public int items_per_page { get; set; }
    }

    public class Data
    {
        public string email { get; set; }
        public string id { get; set; }
        public string name { get; set; }
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

    public class Verification
    {
        public string result { get; set; }
        public List<object> flags { get; set; }
        public string suggested_correction { get; set; }
        public AddressInfo address_info { get; set; }
    }

    public class Result
    {
        public Data data { get; set; }
        public Verification verification { get; set; }
    }

	public class JobDownloadRequestModel : RequestModel
	{
		public int job_id { get; set; }
		public bool valids { get; set; } = true;
		public bool invalids { get; set; } = true;
		public bool catchalls { get; set; } = true;
		public bool unknowns { get; set; } = true;
		public bool disposables { get; set; } = true;
	}

    public class JobDeleteRequestModel : RequestModel
    {
        public int job_id { get; set; }
    }

	public class JobDeleteResponseModel : ResponseModel
	{
		public string status { get; set; }
		public int execution_time { get; set; }

	}

}
