namespace NeverBounceTests;
using NeverBounce.Models;
using NeverBounce.Utilities;
using NUnit.Framework;

[TestFixture]
public class TestQueryString
{
    [Test]
    public void TestToQueryStringSimple()
    {
        var query = new SingleRequestModel("support@neverbounce.com");
        query.Timeout = 3000;
        query.AddressInfo = true;
        query.CreditsInfo = false;

        var resp = QueryStringUtility.ToQueryString(query);
        Assert.AreEqual(
            "email=support%40neverbounce.com&address_info=1&credits_info=0&timeout=3000&request_meta_data[leverage_historical_data]=1", resp);
    }
}
