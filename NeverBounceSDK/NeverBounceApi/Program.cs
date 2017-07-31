using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeverBounce;
using NeverBounce.Models;
using NLog;

namespace NeverBounceApi
{
    class Program
    {
        static void Main(string[] args)
        {
            NeverBounceSdk neverBounceSdk = new NeverBounceSdk("https://api.neverbounce.com/v4", "secret_nvrbnc_dotnet");

            var AccountInfo = neverBounceSdk.AccountInfo();
           var SingleCheck = neverBounceSdk.SingleCheck(SingleChecktestMethod()).Result;
            var SearchJob = neverBounceSdk.SearchJob(SearchJobtestMethod()).Result;
           //var CreateJob = neverBounceSdk.CreateJob(CreateJobtestMethod()).Result;
            // var ParseJob = neverBounceSdk.ParseJob(ParseJobtestMethod()).Result;
            //  var StartJob = neverBounceSdk.StartJob(StartJobtestMethod()).Result;
            //var JobStatus = neverBounceSdk.JobStatus(JobStatustestMethod()).Result;
            //var JobResultss = neverBounceSdk.JobResults(ResultJobtestMethod()).Result;
            // var DeleteJobs = neverBounceSdk.DeleteJobs(DeleteJobtestMethod()).Result;
            var DownloadJobs = neverBounceSdk.DownloadJob(DownloadTestMethod()).Result;
            Console.WriteLine(DownloadJobs);
            Console.ReadLine();
        }
        static JobSearchRequestModel SearchJobtestMethod()
        {
            JobSearchRequestModel model = new JobSearchRequestModel();
            model.job_id = 0;
            model.filename = "";
            model.completed = 0;
            model.processing = 0;
            model.indexing = 0;
            model.failed = 0;
            model.manual_review = 0;
            model.unpurchased = 0;
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
          //  List<List<Input>> sub = new List<List<Input>>();
            List<Input> s = new List<Input>();
            s.Add(new Input { id = "3", email = "support@neverbounce.com", name = "Fred McValid" });
           // sub.Add(s);
            model.input = s;
            return model;
        }
        static JobParseRequestModel ParseJobtestMethod()
        {
            JobParseRequestModel model = new JobParseRequestModel();
            model.auto_start = 1;
            model.job_id = 280350;
            return model;
        }
        static JobStartRequestModel StartJobtestMethod()
        {
            JobStartRequestModel model = new JobStartRequestModel();
            model.job_id = 279047;
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
           // model.key = "secret_nvrbnc_dotnet";
            model.job_id = 279047;
            return model;
        }
        static JobDeleteRequestModel DeleteJobtestMethod()
        {
            JobDeleteRequestModel model = new JobDeleteRequestModel();
            model.job_id= 280350;
            return model;
        }
        static SingleRequestModel SingleChecktestMethod()
        {

            SingleRequestModel model = new SingleRequestModel();
            //model.key = "secret_nvrbnc_dotnet";
            //modelS.job_id = 279047;
            model.email = "support@neverbounce.com";
            return model;
        }
        static JobDownloadRequestModel DownloadTestMethod()
        {

            JobDownloadRequestModel model = new JobDownloadRequestModel();
            //model.key = "secret_nvrbnc_dotnet";
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
