using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;

namespace NeverBounce.Services;

public class AccountService
{
    protected string _apiKey;

    protected IHttpClient _client;

    protected string _host;

    public AccountService(IHttpClient Client, string ApiKey, string Host = null)
    {
        this._client = Client;
        this._apiKey = ApiKey;

        // Accept debug host
        if (Host != null)
            this._host = Host;
    }

    /// <summary>
    ///     Account Info method allow to programmatically check your account's balance and how many jobs are currently running
    ///     on your account.
    ///     See: "https://developers.neverbounce.com/v4.0/reference#account-info"
    /// </summary>
    /// <returns>AccountInfoResponseModel</returns>
    public async Task<AccountInfoResponseModel> Info()
    {
        var model = new RequestModel();
        var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
        var result = await client.MakeRequest("GET", "/account/info", model);
        return JsonConvert.DeserializeObject<AccountInfoResponseModel>(result);
    }
}