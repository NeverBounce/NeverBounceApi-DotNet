namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class POEService
{
    readonly INeverBounceHttpClient client;

    public POEService(INeverBounceHttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    ///     Allows you to confirm front end (Javascript Widget) verification results
    ///     See: "https://developers.neverbounce.com/v4.0/reference#widget-poe-confirm"
    /// </summary>
    /// <param name="model"> POEConfirmRequestModel</param>
    /// <returns>POEConfirmResponseModel</returns>
    public async Task<POEConfirmResponseModel?> Confirm(POEConfirmRequestModel model) => 
        await this.client.RequestPost<POEConfirmResponseModel>( "/poe/confirm", model);
}
