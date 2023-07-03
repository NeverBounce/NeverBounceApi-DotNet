using Moq;
using NeverBounce;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using NeverBounce.Utilities;
using NUnit.Framework;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace NeverBounceSDKTests;

[TestFixture]
public class TestNeverBounceSdk
{
    const string fakeKey = "fake_api_key";

    [Test]
    public void TestNeverBounceSdkSetup()
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var nb = new NeverBounceService(clientMock.Object, fakeKey, null);
        Assert.IsNotNull(nb.Account);
        Assert.IsNotNull(nb.Jobs);
        Assert.IsNotNull(nb.Single);
    }
    
    [Test]
    public void TestNeverBounceAccountInfo()
    {
        var nb = CreateMockClient("{\"status\": \"auth_failure\", \"message\": \"Test Message\"}");

        var resp = Assert.ThrowsAsync<AuthException>(async () =>
            await nb.Account.Info());
        StringAssert.Contains("We were unable to authenticate your request", resp.Message);
        StringAssert.Contains("(auth_failure)", resp.Message);
    }

    static NeverBounceService CreateMockClient(string strContent) {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(strContent, Encoding.UTF8, "application/json")
        };

        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(response));
        return new NeverBounceService(clientMock.Object, fakeKey, null);
    }
}