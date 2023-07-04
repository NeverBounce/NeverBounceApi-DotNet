using NeverBounce;
using NeverBounce.Models;

static class AccountEndpoint
{
    public static async Task Info(NeverBounceService neverBounceService)
    {
        // Call the account info endpoint
        var info = await neverBounceService.Account();

        WriteCreditsInfo(info.CreditsInfo);

        var j = info.JobCounts;
        Console.WriteLine($"Jobs: pending {j?.Queued ?? 0}, processing {j?.Processing ?? 0}, completed {j?.Completed ?? 0}, under review {j?.UnderReview ?? 0}");
    }

    public static void WriteCreditsInfo(CreditsInfo? info) {
        if (info is null) return;

        Console.WriteLine("Credits remaining:");
        Console.WriteLine($"\tFree {info.FreeCreditsRemaining}, used {info.FreeCreditsUsed}");
        Console.WriteLine($"\tPaid {info.PaidCreditsRemaining}, used {info.PaidCreditsUsed}");
    }
}