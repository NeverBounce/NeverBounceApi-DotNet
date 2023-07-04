namespace NeverBounce.Models;

/// <summary>Request object to serialise and send to the /single/check endpoint</summary>
public class SingleRequestModel
{
    public SingleRequestModel(string email) { this.Email = email; }

    /// <summary>The email to verify, required</summary>
    public string Email { get; }

    /// <summary>Include additional address info in response (default: false)</summary>
    public bool AddressInfo { get; set; } = false;

    /// <summary>Include account credit info in response (default: false)</summary>
    public bool CreditsInfo { get; set; } = false;

    /// <summary>The maximum time in seconds we should try to verify the address</summary>
    public int? Timeout { get; set; }

    /// <summary>Miscellanious request meta data</summary>
    public RequestMetaDataModel RequestMetaData { get; set; } = new RequestMetaDataModel();
}
