namespace Exthand.GatewayClient.Models
{
    public class VopResponse : IBase
    {
        public string id { get; set; }
        public string remoteId { get; set; }
        public VopResult vopResult { get; set; }
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }

    public class VopResult
    {
        public VopMatchResult vopMatchResult { get; set; }
        public BankAccountHolder bankAccountHolder { get; set; }
        public VopBank bank { get; set; }
    }

    public class VopMatchResult
    {
        public string result { get; set; }
    }

    public class BankAccountHolder
    {
        public string name { get; set; }
        public VopIdentifier identifier { get; set; }
    }

    public class VopBank
    {
        public string bic { get; set; }
        public string name { get; set; }
    }
}