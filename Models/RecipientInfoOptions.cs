using System.Collections.Generic;

namespace Exthand.GatewayClient.Models
{
    public class RecipientInfoOptions
    {
        public PaymentInitiationRequestOptionsType Name { get; set; } = PaymentInitiationRequestOptionsType.Required;

        /// <summary>
        /// Constrains (min, max lenght, etc.) on the name.
        /// Added to support v4.0.6
        /// </summary>
        public PropertyConstrains NameConstrains { get; set; }

        public PaymentInitiationRequestOptionsType accountIdentification { get; set; } = PaymentInitiationRequestOptionsType.Required;
        public List<AccountSchemeType> accountSchemes { get; set; } = new List<AccountSchemeType>() { AccountSchemeType.IBAN };
        public PaymentInitiationRequestOptionsType currency { get; set; } = PaymentInitiationRequestOptionsType.Unused;
        public TierAddressOptions postalAddress { get; set; } = new TierAddressOptions();
        public AgentOptions agent { get; set; } = new AgentOptions();
    }
}