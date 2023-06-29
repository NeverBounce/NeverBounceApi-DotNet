namespace NeverBounce.Models;

public class RequestMetaDataModel
{
    /// <summary>Control historical data usage, set to false to use real-time only verification</summary>
    public bool LeverageHistoricalData { get; set; } = true;
}
