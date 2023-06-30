namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class SingleService
{
    readonly INeverBounceHttpClient client;

    internal SingleService(INeverBounceHttpClient client)
    {
        this.client = client;
    }

    /// <summary>Single verification allows you verify individual emails and gather additional information pertaining to the email.
    /// <para>Each verification performed over the Single endpoints cost 1 credit. This includes duplicate verifications requests and bad syntax data.</para></summary>
    /// <param name="model">The model details to request</param>
    /// <returns>Result of checking the email</returns>
    public async Task<SingleResponseModel?> Check(SingleRequestModel model) =>
        await this.client.RequestGet<SingleResponseModel>("/single/check", model);

    /// <summary>Single verification allows you verify individual emails and gather additional information pertaining to the email.
    /// <para>Each verification performed over the Single endpoints cost 1 credit. This includes duplicate verifications requests and bad syntax data.</para></summary>
    /// <param name="email">The email to check</param>
    /// <returns>Result of checking the email</returns>
    public async Task<SingleResponseModel?> Check(string email) =>
        await this.Check(new SingleRequestModel(email));
}
