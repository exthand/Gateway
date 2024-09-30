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

        public StructuredRemittanceOptions RemittanceInformationStructured { get; set; } = new StructuredRemittanceOptions();
    }
    
    public class StructuredRemittanceOptions
    {
        public bool Supported { get; set; }
        public PaymentInitiationRequestOptionsType Reference { get; set; } = PaymentInitiationRequestOptionsType.Required;
        public PaymentInitiationRequestOptionsType Code { get; set; } = PaymentInitiationRequestOptionsType.Unused;
        public PaymentInitiationRequestOptionsType Issuer { get; set; } = PaymentInitiationRequestOptionsType.Required;
        public List<RemittanceIssuer> AllowedIssuers { get; set; }
        public List<string> AllowedCodes { get; set; }
        public PropertyConstrains ReferenceConstraints { get; set; } = new PropertyConstrains();
        public PropertyConstrains CodeConstraints { get; set; } 
        public PropertyConstrains IssuerConstraints { get; set; } 
    }
    
    public enum RemittanceIssuer
    {
        /// <summary>
        /// Belgium
        /// </summary>
        BBA,
        /// <summary>
        /// ISO 11649
        /// </summary>
        ISO,
        /// <summary>
        /// International Reference Number, same as ISO 11649
        /// </summary>
        INTL,
        /// <summary>
        /// Swedish Bankgiro OCR
        /// </summary>
        SEBG,
        /// <summary>
        /// Norwegian KID (OCR)
        /// </summary>
        NORF,
        /// <summary>
        /// Finnish reference number
        /// </summary>
        FIRF
    }
}