using System.Collections.Generic;

namespace Exthand.GatewayClient.Models
{
    public class PaymentInitiationRequestOptions
    {
        public RecipientInfoOptions recipient { get; set; } = new RecipientInfoOptions();
        public DebtorInfoOptions debtor { get; set; } = new DebtorInfoOptions();
        public PaymentInitiationRequestOptionsType endToEndId { get; set; } = PaymentInitiationRequestOptionsType.Required;
        public PaymentInitiationRequestOptionsType requestedExecutionDate { get; set; } = PaymentInitiationRequestOptionsType.Optional;
        public PaymentInitiationRequestOptionsType regulatoryReportingCodeRequired { get; set; } = PaymentInitiationRequestOptionsType.Unused;
        public List<RegulatoryReportingCodeOptions> regulatoryReportingCodes { get; set; } = new List<RegulatoryReportingCodeOptions>();


        // Added to support v4.0.6
        public PaymentInitiationRequestOptionsType remittanceInformationUnstructured { get; set; } = PaymentInitiationRequestOptionsType.Optional;
        public PropertyConstrains remittanceInformationUnstructuredConstrains { get; set; } = new PropertyConstrains { maxLength = 140, minLength = 0, regex = "^[a-zA-Z0-9-/:().,? '+]{0,140}$", acceptedChars = "a-zA-Z0-9-/:().,? '+" };

    }
}