﻿using System.Net;
using System.Net.Http.Headers;
using Moq;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace NeverBounceSDKTests;

[TestFixture]
public class TestHttpClient
{
    const string fakeKey = "fake_api_key";
    
    [Test]
    public void TestAuthFailureHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content =
            new StringContent(
                "{\"status\": \"auth_failure\", \"message\": \"The key provided is invalid\", \"execution_time\":100}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = Assert.ThrowsAsync<AuthException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("The key provided is invalid", resp.Message);
        StringAssert.Contains("(auth_failure)", resp.Message);
    }

    [Test]
    public void TestBadlyFormattedJsonHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content = new StringContent("{notvalid json}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/500", null));
        StringAssert.Contains("(Internal error)", resp.Message);
    }

    [Test]
    public void TestBadReferrerErrorHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content =
            new StringContent(
                "{\"status\": \"bad_referrer\", \"message\": \"The originator of this request is not trusted\", \"execution_time\":100}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = Assert.ThrowsAsync<BadReferrerException>(async () =>
            await httpClient.RequestGetContent( "/500", null));
    }

    [Test]
    public void TestGenericFailureHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content =
            new StringContent(
                "{\"status\": \"general_failure\", \"message\": \"Something went wrong\", \"execution_time\":100}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGetContent( "/500", null));
        StringAssert.Contains("Something went wrong", resp.Message);
        StringAssert.Contains("(general_failure)", resp.Message);
    }

    [Test]
    public void TestHttpStatusCode400ErrorHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGetContent( "/404", null));
    }

    [Test]
    public void TestHttpStatusCode500ErrorHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGetContent( "/500", null));
    }

    [Test]
    public void TestJsonUnmarshalling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content = new StringContent("{\"status\": \"success\", \"execution_time\":100}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = httpClient.RequestGetContent( "/", null).Result.ReadAsStringAsync().Result;


        Assert.AreEqual("{\"status\": \"success\", \"execution_time\":100}", resp);
        var token = JObject.Parse(resp);
        Assert.AreEqual("success", token.SelectToken("status").ToString());
        Assert.AreEqual(100, (int) token.SelectToken("execution_time"));
    }

    [Test]
    public void TestMatchContentType()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content = new StringContent("{\"status\":\"success\"}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);

        var resp = httpClient.RequestGet<ResponseModel>( "/", null).Result;
        Assert.AreEqual(ResponseStatus.Success, resp.Status);
    }
    
    [Test]
    public void TestMismatchedContentTypeThrowsError()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content = new StringContent("Hello!");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGet<ResponseModel>( "/", null));
    }

    [Test]
    public void TestTempUnavailErrorHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content =
            new StringContent(
                "{\"status\": \"temp_unavail\", \"message\": \"Something went wrong\", \"execution_time\":100}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = Assert.ThrowsAsync<GeneralException>(async () =>
            await httpClient.RequestGetContent( "/500", null));
    }

    [Test]
    public void TestThrottleErrorHandling()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        responseMessage.Content =
            new StringContent(
                "{\"status\": \"throttle_triggered\", \"message\": \"Too many requests in a short amount of time\", \"execution_time\":100}");
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMessage));

        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);
        var resp = Assert.ThrowsAsync<ThrottleException>(async () =>
            await httpClient.RequestGetContent( "/500", null));
    }

    [Test]
    public void TestToQueryStringSimple()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var httpClient = new NeverBounceHttpClient(clientMock.Object, fakeKey, null);

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