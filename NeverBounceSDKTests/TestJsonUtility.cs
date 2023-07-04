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

        string serilalised = JsonUtility.Serialise(thing, "fake-api-key");
        var deserilalised = JsonUtility.Deserialise<Thing>(serilalised);

        Assert.IsNotNull(deserilalised);
        Assert.AreEqual(thing.AbcID, deserilalised.AbcID);
        Assert.AreEqual(thing.DefACR, deserilalised.DefACR);
        Assert.AreEqual(thing.StrTest, deserilalised.StrTest);
        Assert.AreEqual(thing.JsonTest, deserilalised.JsonTest);
        Assert.AreEqual(thing.EnumTest, deserilalised.EnumTest);

    }
}
