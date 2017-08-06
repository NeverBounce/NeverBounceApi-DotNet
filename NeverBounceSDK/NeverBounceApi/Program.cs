using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeverBounce;
using NeverBounce.Models;
using NeverBounceSdkExamples.Requests;
using NLog;

namespace NeverBounceSdkExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            NeverBounceSdk sdk = new NeverBounceSdk("https://api.neverbounce.com/v4", "secret_nvrbnc_dotnet");

			var response = Account.Info(sdk);
			//var response = Single.Check(sdk);

            //var response = neverBounceSdk.SearchJob(SearchJobtestMethod()).Result;
            //var response = neverBounceSdk.CreateJob(CreateJobtestMethod()).Result;
            //var response = neverBounceSdk.ParseJob(ParseJobtestMethod()).Result;
            //var response = neverBounceSdk.StartJob(StartJobtestMethod()).Result;
            //var response = neverBounceSdk.JobStatus(JobStatustestMethod()).Result;
            //var response = neverBounceSdk.JobResults(ResultJobtestMethod()).Result;
            //var response = neverBounceSdk.DeleteJobs(DeleteJobtestMethod()).Result;
            //var response = neverBounceSdk.DownloadJob(DownloadTestMethod()).Result;
            var_dump(response);
            Console.ReadLine();
        }

		public static void var_dump(object obj)
		{
			Console.WriteLine("{0,-18} {1}", "Name", "Value");
			string ln = @"-------------------------------------   
               ----------------------------";
			Console.WriteLine(ln);

			Type t = obj.GetType();
			PropertyInfo[] props = t.GetProperties();

			for (int i = 0; i < props.Length; i++)
			{
				try
				{
					Console.WriteLine("{0,-18} {1}",
						  props[i].Name, props[i].GetValue(obj, null));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);   
				}
			}
			Console.WriteLine();
		}

        static JobSearchRequestModel SearchJobtestMethod()
        {
            JobSearchRequestModel model = new JobSearchRequestModel();
            model.job_id = 288025;
            model.filename = "";
            model.page = 0;
            model.items_per_page = 0;
            return model;
        }
        static JobCreateRequestModel CreateJobtestMethod()
        {
            JobCreateRequestModel model = new JobCreateRequestModel();
            model.input_location = "supplied";
            model.filename = "SampleNeverBounceAPI.csv";
            model.auto_parse = 0;
            model.auto_run = 0;
            model.auto_start = 0;
            List<object> s = new List<object>();
            s.Add(new  { id = "3", email = "support@neverbounce.com", name = "Fred McValid" });
            model.input = s;
            return model;
        }
        static JobParseRequestModel ParseJobtestMethod()
        {
            JobParseRequestModel model = new JobParseRequestModel();
            model.auto_start = 1;
            model.job_id = 288024;
            return model;
        }
        static JobStartRequestModel StartJobtestMethod()
        {
            JobStartRequestModel model = new JobStartRequestModel();
            model.job_id = 288025;
            model.run_sample = "";
            return model;
        }
        static JobStatusRequestModel JobStatustestMethod()
        {

            JobStatusRequestModel model = new JobStatusRequestModel();
            model.job_id = 279047;
            return model;
        }
        static JobResultsRequestModel ResultJobtestMethod()
        {
            JobResultsRequestModel model = new JobResultsRequestModel();
            model.job_id = 280580;
            return model;
        }
        static JobDeleteRequestModel DeleteJobtestMethod()
        {
            JobDeleteRequestModel model = new JobDeleteRequestModel();
            model.job_id= 280350;
            return model;
        }

        static JobDownloadRequestModel DownloadTestMethod()
        {

            JobDownloadRequestModel model = new JobDownloadRequestModel();
            model.job_id = 280580;
            model.valids = true;
            model.invalids = true;
            model.catchalls = true;
            model.unknowns = true;
            model.disposables = true;
            model.include_duplicates = false;
            model.email_status = true;
            return model;
        }

    }
}
