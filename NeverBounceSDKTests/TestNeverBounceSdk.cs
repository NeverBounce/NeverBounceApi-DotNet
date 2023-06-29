using Moq;
using NeverBounce;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using NeverBounce.Utilities;
using NUnit.Framework;

namespace NeverBounceSDKTests;

[TestFixture]
public class TestNeverBounceSdk
{
    readonly static NeverBounceConfigurationSettings fakeSettings = new("fake_api_key");

    [Test]
    public void TestNeverBounceSdkSetup()
    {
        var clientMock = new Mock<IHttpClient>();
        var nb = new NeverBounceService(clientMock.Object, fakeSettings);
        Assert.IsNotNull(nb.Account);
        Assert.IsNotNull(nb.Jobs);
        Assert.IsNotNull(nb.POE);
        Assert.IsNotNull(nb.Single);
    }
    
    [Test]
    public void TestNeverBounceAccountInfo()
    {
        var clientMock = new Mock<IHttpClient>();
        var nb = new NeverBounceService(clientMock.Object, fakeSettings);            
        var resp = Assert.ThrowsAsync<AuthException>(async () =>
            await nb.Account.Info());
        StringAssert.Contains("We were unable to authenticate your request", resp.Message);
        StringAssert.Contains("(auth_failure)", resp.Message);
    }
}