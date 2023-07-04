using NeverBounce;
using NeverBounce.Models;

static class SingleEndpoint
{
    public static async Task Check(NeverBounceService neverBounceService, string email)
    {
        Console.WriteLine($"Checking bounce status of: {email}");
        var result = await neverBounceService.CheckSingle(email, true, true);

        Console.WriteLine($"\tResult: {ResultCodeDescription(result.Result)}");

        if(result.SuggestedCorrection is not null)
            Console.WriteLine($"\tSuggested correction: {result.SuggestedCorrection}");

        if(result.Flags?.Any() ?? false)
            Console.WriteLine($"\tFlags: {string.Join(", ", result.Flags)}");

        AccountEndpoint.WriteCreditsInfo(result.CreditsInfo);
    }

    public static string ResultCodeDescription(ResultCode result)
    {
        return result switch
        {
            ResultCode.Valid => "Verified as real address", // ✅ 
            ResultCode.Invalid => "Verified as not valid", // ❌ 
            ResultCode.Disposable => "A temporary, disposable address", // 🗑️ 
            ResultCode.Catchall => "A domain-wide setting",
            ResultCode.Unknown => "The server cannot be reached",
            _ => result.ToString(),
        };
    }
}