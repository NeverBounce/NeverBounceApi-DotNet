using System;
using System.Reflection;
using NeverBounce;
using NeverBounceSdkExamples.Requests;

namespace NeverBounceSdkExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            NeverBounceSdk sdk = new NeverBounceSdk("api_key");

            var response = AccountEndpoint.Info(sdk);
            //var response = SingleEndpoints.Check(sdk);
            //var response = JobsEndpoint.Search(sdk);
            //var response = JobsEndpoint.Create(sdk);
            //var response = JobsEndpoint.Parse(sdk);
            //var response = JobsEndpoint.Start(sdk);
            //var response = JobsEndpoint.Status(sdk);
            //var response = JobsEndpoint.Results(sdk);
            //var response = JobsEndpoint.Download(sdk);
            //var response = JobsEndpoint.Delete(sdk);

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
    }
}
