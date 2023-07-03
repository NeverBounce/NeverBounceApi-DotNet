using Moq;
using NeverBounce.Utilities;
using NeverBounce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounceSDKTests;

static class TestUtility
{
    public static NeverBounceService CreateMockClient(string strContent)
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(strContent, Encoding.UTF8, "application/json")
        };

        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(response));
        return new NeverBounceService(clientMock.Object, "fake_api_key", null);
    }

    public static NeverBounceHttpClient CreateMockEndpoint(string strContent) =>
        CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(strContent, Encoding.UTF8, "application/json")
        });

    public static NeverBounceHttpClient CreateMockEndpoint(HttpResponseMessage response)
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(response));
        return new NeverBounceHttpClient(clientMock.Object, "fake_api_key", null);
    }
}
