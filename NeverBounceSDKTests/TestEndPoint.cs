namespace NeverBounceTests;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using static TestUtility;

[TestFixture]
public class TestEndPoint
{
    
    [Test]
    public void TestAuthFailureHandling()
    {
        var httpClient = CreateMockEndpoint("""
            {
                "status": "auth_failure", 
                "message": "The key provided is invalid", 
                "execution_time": 100
            }
            """);
        var resp = Assert.ThrowsAsync<NeverBounceServiceException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("The key provided is invalid", resp.Message);
        StringAssert.Contains("(auth_failure)", resp.Message);
        Assert.AreEqual(ResponseStatus.AuthFailure, resp.Reason);
    }

    [Test]
    public void TestBadlyFormattedJsonHandling()
    {
        var httpClient = CreateMockEndpoint("{notvalid json}");
        var resp = Assert.ThrowsAsync<NeverBounceParseException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("{notvalid json}", resp.Message);
    }

    [Test]
    public void TestBadReferrerErrorHandling()
    {
        var httpClient = CreateMockEndpoint("""
            {
                "status": "bad_referrer", 
                "message": "The originator of this request is not trusted", 
                "execution_time": 100
            }
            """);
        var resp = Assert.ThrowsAsync<NeverBounceServiceException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        Assert.AreEqual(ResponseStatus.BadReferrer, resp.Reason);
    }

    [Test]
    public void TestGenericFailureHandling()
    {
        var httpClient = CreateMockEndpoint("""
            {
                "status": "general_failure", 
                "message": "Something went wrong", 
                "execution_time": 100
            }
            """);
        var resp = Assert.ThrowsAsync<NeverBounceServiceException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("Something went wrong", resp.Message);
        StringAssert.Contains("(general_failure)", resp.Message);
        Assert.AreEqual(ResponseStatus.GeneralFailure, resp.Reason);
    }

    [Test]
    public void TestHttpStatusCode400ErrorHandling()
    {
        var httpClient = CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.NotFound));
        var resp = Assert.ThrowsAsync<NeverBounceResponseException>(async () =>
            await httpClient.RequestGetContent( "/404", null));

        Assert.AreEqual(HttpStatusCode.NotFound, resp.Status);
    }

    [Test]
    public void TestHttpStatusCode500ErrorHandling()
    {
        var httpClient = CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
        var resp = Assert.ThrowsAsync<NeverBounceResponseException>(async () =>
            await httpClient.RequestGetContent( "/500", null));

        Assert.AreEqual(HttpStatusCode.ServiceUnavailable, resp.Status);
    }

    [Test]
    public void TestJsonUnmarshalling()
    {
        var content = """
            {
                "status": "success", 
                "execution_time": 123
            }
            """;
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
        var httpClient = CreateMockEndpoint("""
            { "status": "success" }
            """);
        var resp = httpClient.RequestGet<ResponseModel>( "/", null).Result;
        Assert.AreEqual(ResponseStatus.Success, resp.Status);
    }
    
    [Test]
    public void TestMismatchedContentTypeThrowsError()
    {
        var httpClient = CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent("Hello!", new MediaTypeHeaderValue("text/csv"))
        });
        var resp = Assert.ThrowsAsync<NeverBounceResponseException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/", null));
    }

    [Test]
    public void TestTempUnavailErrorHandling()
    {
        var httpClient = CreateMockEndpoint("""
            {
                "status": "temp_unavail", 
                "message": "Something went wrong", 
                "execution_time": 100
            }
            """);
        var resp = Assert.ThrowsAsync<NeverBounceServiceException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        Assert.AreEqual(ResponseStatus.TempUnavail, resp.Reason);
    }

    [Test]
    public void TestThrottleErrorHandling()
    {
        var httpClient = CreateMockEndpoint("""
            {
                "status": "throttle_triggered", 
                "message": "Too many requests in a short amount of time", 
                "execution_time": 100
            }
            """);
        var resp = Assert.ThrowsAsync<NeverBounceServiceException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        Assert.AreEqual(ResponseStatus.ThrottleTriggered, resp.Reason);
    }
}
