namespace NeverBounceTests;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using NUnit.Framework;
using static TestUtility;

[TestFixture]
public class TestService
{
    [Test]
    public void TestNeverBounceSdkSetup()
    {
        var nb = CreateMockClient("");
        Assert.IsNotNull(nb.Jobs);
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

        var resp = Assert.ThrowsAsync<NeverBounceServiceException>(async () =>
            await nb.Account());

        Assert.IsNotNull(resp);
        StringAssert.Contains("We were unable to authenticate your request", resp.Message);
        StringAssert.Contains("(auth_failure)", resp!.Message);
        Assert.AreEqual(ResponseStatus.AuthFailure, resp!.Reason);
    }
}
