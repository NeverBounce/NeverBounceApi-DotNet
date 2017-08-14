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

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Moq;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace NeverBounceSDKTests
{
    [TestFixture]
    public class TestHttpClient
    {
        [Test]
        public void TestAuthFailureHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content =
                new StringContent(
                    "{\"status\": \"auth_failure\", \"message\": \"The key provided is invalid\", \"execution_time\":100}");
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = Assert.ThrowsAsync<AuthException>(async () =>
                await httpClient.MakeRequest("GET", "/500", new RequestModel()));
            StringAssert.Contains("The key provided is invalid", resp.Message);
            StringAssert.Contains("(auth_failure)", resp.Message);
        }

        [Test]
        public void TestBadlyFormattedJsonHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent("{notvalid json}");
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = Assert.ThrowsAsync<HttpClientException>(async () =>
                await httpClient.MakeRequest("GET", "/500", new RequestModel()));
            StringAssert.Contains("(Internal error)", resp.Message);
        }

        [Test]
        public void TestBadReferrerErrorHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content =
                new StringContent(
                    "{\"status\": \"bad_referrer\", \"message\": \"The originator of this request is not trusted\", \"execution_time\":100}");
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = Assert.ThrowsAsync<BadReferrerException>(async () =>
                await httpClient.MakeRequest("GET", "/500", new RequestModel()));
        }

        [Test]
        public void TestGenericFailureHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content =
                new StringContent(
                    "{\"status\": \"general_failure\", \"message\": \"Something went wrong\", \"execution_time\":100}");
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = Assert.ThrowsAsync<GeneralException>(async () =>
                await httpClient.MakeRequest("GET", "/500", new RequestModel()));
            StringAssert.Contains("Something went wrong", resp.Message);
            StringAssert.Contains("(general_failure)", resp.Message);
        }

        [Test]
        public void TestHttpStatusCode400ErrorHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            Assert.ThrowsAsync<HttpClientException>(async () =>
                await httpClient.MakeRequest("GET", "/404", new RequestModel()));
        }

        [Test]
        public void TestHttpStatusCode500ErrorHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            Assert.ThrowsAsync<HttpClientException>(async () =>
                await httpClient.MakeRequest("GET", "/500", new RequestModel()));
        }

        [Test]
        public void TestJsonUnmarshalling()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent("{\"status\": \"success\", \"execution_time\":100}");
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = httpClient.MakeRequest("GET", "/", new RequestModel()).Result;


            Assert.AreEqual("{\"status\": \"success\", \"execution_time\":100}", resp);
            var token = JObject.Parse(resp);
            Assert.AreEqual("success", token.SelectToken("status").ToString());
            Assert.AreEqual(100, (int) token.SelectToken("execution_time"));
        }

        [Test]
        public void TestPlainTextPassthrough()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent("Hello!");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = httpClient.MakeRequest("GET", "/", new RequestModel()).Result;
            Assert.AreEqual("Hello!", resp);
        }

        [Test]
        public void TestTempUnavailErrorHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content =
                new StringContent(
                    "{\"status\": \"temp_unavail\", \"message\": \"Something went wrong\", \"execution_time\":100}");
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = Assert.ThrowsAsync<GeneralException>(async () =>
                await httpClient.MakeRequest("GET", "/500", new RequestModel()));
        }

        [Test]
        public void TestThrottleErrorHandling()
        {
            var clientMock = new Mock<IHttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content =
                new StringContent(
                    "{\"status\": \"throttle_triggered\", \"message\": \"Too many requests in a short amount of time\", \"execution_time\":100}");
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            clientMock.Setup(http => http.GetAsync(It.IsAny<Uri>())).Returns(Task.FromResult(responseMessage));

            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");
            var resp = Assert.ThrowsAsync<ThrottleException>(async () =>
                await httpClient.MakeRequest("GET", "/500", new RequestModel()));
        }

        [Test]
        public void TestToQueryStringSimple()
        {
            var clientMock = new Mock<IHttpClient>();
            clientMock.Setup(http => http.GetRequestHeaders()).Returns(new HttpClient().DefaultRequestHeaders);
            var httpClient = new NeverBounceHttpClient(clientMock.Object, "fake_api_key");

            var query = new SingleRequestModel();
            query.key = "fake_api_key";
            query.email = "support@neverbounce.com";
            query.timeout = 3000;
            query.address_info = true;
            query.credits_info = false;

            var resp = httpClient.ToQueryString(query);
            Assert.AreEqual(
                "email=support%40neverbounce.com&address_info=1&credits_info=0&timeout=3000&key=fake_api_key", resp);
        }
    }
}