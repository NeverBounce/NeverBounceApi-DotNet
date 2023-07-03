namespace NeverBounceSDKTests;
using NeverBounce.Exceptions;
using NUnit.Framework;
using static TestUtility;

[TestFixture]
public class TestNeverBounceSdk
{
    [Test]
    public void TestNeverBounceSdkSetup()
    {
        var nb = CreateMockClient("");
        Assert.IsNotNull(nb.Account);
        Assert.IsNotNull(nb.Jobs);
        Assert.IsNotNull(nb.Single);
    }
    
    [Test]
    public void TestNeverBounceAccountInfo()
    {
        var nb = CreateMockClient("""
            {
                "status": "auth_failure", 
                "message": "Test Message"
            }
            """);

        var resp = Assert.ThrowsAsync<AuthException>(async () =>
            await nb.Account.Info());

        StringAssert.Contains("We were unable to authenticate your request", resp.Message);
        StringAssert.Contains("(auth_failure)", resp.Message);
    }
}
