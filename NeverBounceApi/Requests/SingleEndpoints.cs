﻿namespace NeverBounceSdkExamples.Requests;
using NeverBounce;
using NeverBounce.Models;

public class SingleEndpoints
{
    public static async Task<SingleResponseModel> Check(NeverBounceService sdk)
    {
        var model = new SingleRequestModel("support@neverbounce.com");
        model.CreditsInfo = true;
        model.AddressInfo = true;
        return await sdk.Single.Check(model);
    }
}