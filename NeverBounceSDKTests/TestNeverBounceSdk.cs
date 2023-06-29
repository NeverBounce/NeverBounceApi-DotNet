using NeverBounce;
using NeverBounce.Exceptions;
using NUnit.Framework;

namespace NeverBounceSDKTests;

[TestFixture]
public class TestNeverBounceSdk
{
    [Test]
    public void TestNeverBounceSdkSetup()
    {
        var nb = new NeverBounceService("fake_api_key");
        Assert.IsNotNull(nb.Account);
        Assert.IsNotNull(nb.Jobs);
        Assert.IsNotNull(nb.POE);
        Assert.IsNotNull(nb.Single);
    }
    
    [Test]
    public void TestNeverBounceAccountInfo()
    {
        var nb = new NeverBounceService("fake_api_key");            
        var resp = Assert.ThrowsAsync<AuthException>(async () =>
            await nb.Account.Info());
        StringAssert.Contains("We were unable to authenticate your request", resp.Message);
        StringAssert.Contains("(auth_failure)", resp.Message);
    }
}