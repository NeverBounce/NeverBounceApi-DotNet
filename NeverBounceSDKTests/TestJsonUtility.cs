namespace NeverBounceTests;
using NeverBounce.Models;
using NeverBounce.Utilities;
using NUnit.Framework;

[TestFixture]
public class TestJsonUtility
{
    class Thing { 
        public int AbcID { get; set; }
        public bool DefACR { get; set; }
        public string? StrTest { get; set; }
        public string? JsonTest{ get; set; }
        public ResponseStatus? EnumTest { get; set; }
    }

    [Test]
    public void TestRoundTrip()
    {
        var thing = new Thing
        {
            AbcID = 1,
            DefACR = true,
            StrTest = "text",
            JsonTest = "{ \"e\": \"JSON embed\" }",
            EnumTest = ResponseStatus.AuthFailure
        };

        // Serialise and check matches format expected by NeverBounce API
        string serilalised = JsonUtility.Serialise(thing, "fake-api-key");
        StringAssert.Contains("\"key\":\"fake-api-key\"", serilalised, "Failed to embed key");
        StringAssert.Contains("\"abc_id\":1", serilalised);
        StringAssert.Contains("\"def_acr\":true", serilalised);
        StringAssert.Contains("\"str_test\":\"text\"", serilalised);
        StringAssert.Contains("\"enum_test\":\"auth_failure\"", serilalised, "Failed to apply correct casing to enums");

        // Deserialise and check values match after round trip
        var deserilalised = JsonUtility.Deserialise<Thing>(serilalised);
        Assert.IsNotNull(deserilalised);
        Assert.AreEqual(thing.AbcID, deserilalised.AbcID);
        Assert.AreEqual(thing.DefACR, deserilalised.DefACR);
        Assert.AreEqual(thing.StrTest, deserilalised.StrTest);
        Assert.AreEqual(thing.JsonTest, deserilalised.JsonTest);
        Assert.AreEqual(thing.EnumTest, deserilalised.EnumTest);
    }
}
