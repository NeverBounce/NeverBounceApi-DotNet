namespace NeverBounce.Models;

public enum ResponseStatus
{
    /// <summary>The request was successful</summary>
    Success,

    /// <summary>Something went wrong with the request; check the message property for further details</summary>
    GeneralFailure,

    /// <summary>The request couldn't be authenticated; ensure your API key has been typed correctly and that you're using a V4 API key </summary>
    AuthFailure,

    /// <summary>An internal error has occurred; typically this indicates a partial service interruption</summary>
    TempUnavail,

    /// <summary>The request was rejected due to rate limiting; try again shortly</summary>
    ThrottleTriggered,

    /// <summary>The referrer for this request is not trusted</summary>
    BadReferrer,
}
