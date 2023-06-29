using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;

namespace NeverBounce.Services;

public class POEService
{
    protected string _apiKey;

    protected IHttpClient _client;

    protected string _host;

    public POEService(IHttpClient Client, string ApiKey, string Host = null)
    {
        this._client = Client;
        this._apiKey = ApiKey;

        // Accept debug host
        if (Host != null)
            this._host = Host;
    }

    /// <summary>
    ///     Allows you to confirm front end (Javascript Widget) verification results
    ///     See: "https://developers.neverbounce.com/v4.0/reference#widget-poe-confirm"
    /// </summary>
    /// <param name="model"> POEConfirmRequestModel</param>
    /// <returns>POEConfirmResponseModel</returns>
    public async Task<POEConfirmResponseModel> Confirm(POEConfirmRequestModel model)
    {
        var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
        var result = await client.MakeRequest("POST", "/poe/confirm", model);
        return JsonConvert.DeserializeObject<POEConfirmResponseModel>(result);
    }
}