namespace NeverBounce.Models
{
    public enum ResultCodes
    {
        valid,
        invalid,
        disposable,
        catchall,
        unknown
    }

    public class SingleResponseModel : ResponseModel
    {
        public string result { get; set; }
        public List<string> flags { get; set; }
        public string suggested_correction { get; set; }
        public string retry_token { get; set; }
        public CreditsInfo credits_info { get; set; }
        public AddressInfo address_info { get; set; }

        public bool ResultIs(string resultCode)
        {
            return this.result.ToLower() == resultCode.ToLower();
        }

        public bool ResultIs(IEnumerable<string> resultCodes)
        {
            resultCodes = resultCodes.Select(c => c.ToLower());
            return resultCodes.Contains(this.result.ToLower());
        }

        public bool ResultIsNot(string resultCode)
        {
            return this.result.ToLower() != resultCode.ToLower();
        }

        public bool ResultIsNot(IEnumerable<string> resultCodes)
        {
            resultCodes = resultCodes.Select(c => c.ToLower());
            return !resultCodes.Contains(this.result.ToLower());
        }
    }

    public class SingleRequestModel : RequestModel
    {
        public string email { get; set; }
        public bool? address_info { get; set; } = false;
        public bool? credits_info { get; set; } = false;
        public int? timeout { get; set; }
        public RequestMetaDataModel request_meta_data { get; set; } = new RequestMetaDataModel();
    }
}