using NeverBounce;
using NeverBounceSdkExamples.Requests;

namespace NeverBounceSdkExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sdk = new NeverBounceSdk("api_key");
            
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/await
            
            var response = AccountEndpoint.Info(sdk).Result;
            //var response = POEEndpoints.Confirm(sdk).Result;
            //var response = SingleEndpoints.Check(sdk).Result;
            //var response = JobsEndpoint.Search(sdk).Result;
            //var response = JobsEndpoint.CreateSuppliedData(sdk).Result;
            //var response = JobsEndpoint.CreateRemoteUrl(sdk).Result;
            //var response = JobsEndpoint.Parse(sdk).Result;
            //var response = JobsEndpoint.Start(sdk).Result;
            //var response = JobsEndpoint.Status(sdk).Result;
            //var response = JobsEndpoint.Results(sdk).Result;
            //var response = JobsEndpoint.Download(sdk).Result;
            //var response = JobsEndpoint.Delete(sdk).Result;

            var_dump(response);
            Console.ReadLine();
        }

        public static void var_dump(object obj)
        {
            Console.WriteLine("{0,-18} {1}", "Name", "Value");
            var ln = @"-------------------------------------   
               ----------------------------";
            Console.WriteLine(ln);

            var t = obj.GetType();
            var props = t.GetProperties();

            for (var i = 0; i < props.Length; i++)
                try
                {
                    Console.WriteLine("{0,-18} {1}",
                        props[i].Name, props[i].GetValue(obj, null));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            Console.WriteLine();
        }
    }
}