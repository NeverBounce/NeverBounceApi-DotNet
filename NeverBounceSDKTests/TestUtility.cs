namespace NeverBounceTests;
using Moq;
using NeverBounce;
using NeverBounce.Utilities;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

    public static INeverBounceEndpoint CreateMockEndpoint(string strContent) =>
        CreateMockEndpoint(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(strContent, Encoding.UTF8, "application/json")
        });

    public static INeverBounceEndpoint CreateMockEndpoint(HttpResponseMessage response)
    {
        var clientMock = new Mock<IHttpServiceEndpoint>();
        clientMock.Setup(http => http.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(response));
        return new NeverBounceEndpoint(clientMock.Object, "fake_api_key", null);
    }
}
