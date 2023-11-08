using System.Collections.Generic;

namespace Exthand.GatewayClient.Models
{
    public class DebtorInfoOptions
    {
        public PaymentInitiationRequestOptionsType Name { get; set; } = PaymentInitiationRequestOptionsType.Optional;

        /// <summary>
        /// Constrains (min, max lenght, etc.) on the name.
        /// Added to support v4.0.6
        /// </summary>
        public PropertyConstrains NameConstrains { get; set; }

        public PaymentInitiationRequestOptionsType accountIdentification { get; set; }
        public List<AccountSchemeType> accountSchemes { get; set; } = new List<AccountSchemeType>() { AccountSchemeType.IBAN };
        public PaymentInitiationRequestOptionsType currency { get; set; } = PaymentInitiationRequestOptionsType.Optional;
        public PaymentInitiationRequestOptionsType email { get; set; } = PaymentInitiationRequestOptionsType.Optional;
        public PaymentInitiationRequestOptionsType phoneNumber { get; set; } = PaymentInitiationRequestOptionsType.Optional;
        public TierAddressOptions postalAddress { get; set; } = new TierAddressOptions();
    }
}