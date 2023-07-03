namespace NeverBounceSDKTests;
using System.Net;
using System.Net.Http.Headers;
using Moq;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using static TestUtility;

[TestFixture]
public class TestHttpClient
{
    
    [Test]
    public void TestAuthFailureHandling()
    {
        var httpClient = CreateMockEndpoint("{\"status\": \"auth_failure\", \"message\": \"The key provided is invalid\", \"execution_time\":100}");
        var resp = Assert.ThrowsAsync<AuthException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("The key provided is invalid", resp.Message);
        StringAssert.Contains("(auth_failure)", resp.Message);
    }

    [Test]
    public void TestBadlyFormattedJsonHandling()
    {
        var httpClient = CreateMockEndpoint("{notvalid json}");
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("{notvalid json}", resp.Message);
    }

    [Test]
    public void TestBadReferrerErrorHandling()
    {
        var httpClient = CreateMockEndpoint("{\"status\": \"bad_referrer\", \"message\": \"The originator of this request is not trusted\", \"execution_time\":100}");
        var resp = Assert.ThrowsAsync<BadReferrerException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
    }

    [Test]
    public void TestGenericFailureHandling()
    {
        var httpClient = CreateMockEndpoint("{\"status\": \"general_failure\", \"message\": \"Something went wrong\", \"execution_time\":100}");
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("Something went wrong", resp.Message);
        StringAssert.Contains("(general_failure)", resp.Message);
    }

    [Test]
    public void TestHttpStatusCode400ErrorHandling()
    {
        var httpClient = CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.NotFound));
        Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGetContent( "/404", null));
    }

    [Test]
    public void TestHttpStatusCode500ErrorHandling()
    {
        var httpClient = CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
        Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGetContent( "/500", null));
    }

    [Test]
    public void TestJsonUnmarshalling()
    {
        var content = "{\"status\": \"success\", \"execution_time\":123}";
        var httpClient = CreateMockEndpoint(content);
        var resp = httpClient.RequestGetContent( "/", null).Result.ReadAsStringAsync().Result;

        Assert.AreEqual(content, resp);
        var token = JObject.Parse(resp);
        Assert.AreEqual("success", token.SelectToken("status").ToString());
        Assert.AreEqual(123, (int) token.SelectToken("execution_time"));
    }

    [Test]
    public void TestMatchContentType()
    {
        var httpClient = CreateMockEndpoint("{\"status\": \"success\"}");
        var resp = httpClient.RequestGet<ResponseModel>( "/", null).Result;
        Assert.AreEqual(ResponseStatus.Success, resp.Status);
    }
    
    [Test]
    public void TestMismatchedContentTypeThrowsError()
    {
        var httpClient = CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent("Hello!", new MediaTypeHeaderValue("text/csv"))
        });
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/", null));
    }

    [Test]
    public void TestTempUnavailErrorHandling()
    {
        var httpClient = CreateMockEndpoint("{\"status\": \"temp_unavail\", \"message\": \"Something went wrong\", \"execution_time\":100}");
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
    }

    [Test]
    public void TestThrottleErrorHandling()
    {
        var httpClient = CreateMockEndpoint("{\"status\": \"throttle_triggered\", \"message\": \"Too many requests in a short amount of time\", \"execution_time\":100}");
        var resp = Assert.ThrowsAsync<ThrottleException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
    }

    [Test]
    public void TestToQueryStringSimple()
    {
        var query = new SingleRequestModel("support@neverbounce.com");
        query.Key = "fake_api_key";
        query.Timeout = 3000;
        query.AddressInfo = true;
        query.CreditsInfo = false;

        var resp = QueryStringUtility.ToQueryString(query);
        Assert.AreEqual(
            "email=support%40neverbounce.com&address_info=1&credits_info=0&timeout=3000&request_meta_data[leverage_historical_data]=1&key=fake_api_key", resp);
    }
}