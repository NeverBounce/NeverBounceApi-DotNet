namespace NeverBounce.Services; 
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class SingleService
{
    readonly INeverBounceHttpClient client;

    public SingleService(INeverBounceHttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    ///     Single verification allows you verify individual emails and gather additional information pertaining to the email.
    ///     See: "https://developers.neverbounce.com/v4.0/reference#single-check"
    /// </summary>
    /// <param name="model"> SingleRequestModel</param>
    /// <returns>SingleResponseModel</returns>
    public async Task<SingleResponseModel?> Check(SingleRequestModel model) =>
        await this.client.RequestGet<SingleResponseModel>( "/single/check", model);
}