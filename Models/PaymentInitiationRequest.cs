using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{

    public enum TierAddressType
    {
        BUSINESS,
        CORRESPONDENCE,
        DELIVERYTO,
        MAILTO,
        POBOX,
        POSTAL,
        RESIDENTIAL,
        STATEMENT
    }


    public enum PaymentProduct
    {
        Domestic,
        SEPA,
        CrossBorder
    }

    public enum PaymentPriority
    {
        Normal,
        Instant
    }

    /// <summary>
    /// Used to specify a structured information to the debtor.
    /// https://docs.exthand.com/docs/structured-remittance#bba
    /// </summary>
    public class StructuredRemittance
    {
        public string? code { get; set; }
        public string? issuer { get; set; }
        public string? reference { get; set; }
    }
    
    
    public class PaymentInitiationRequest
    {
        [Required]
        public RecipientInfo recipient { get; set; }
        [Required]
        public DebtorInfo debtor { get; set; }
        [RegularExpression(@"((^\s*((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))\s*$)|(^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$))", ErrorMessage ="The {0} has invalid format.")]
        public string psuIp { get; set; }
        public string psuIpPort {get;set;}
        public string psuAccept {get;set;}
        public string psuAcceptCharset {get;set;}
        public string psuAcceptEncoding {get;set;}
        public string psuAcceptLanguage {get;set;}
        public string psuDate {get;set;}
        public string psuDeviceId {get;set;}
        public string psuGeoLocation {get;set;}
        public string psuHttpMethod {get;set;}
        public string psuLastLoggedTime {get;set;}
        public string psuReferer {get;set;}
        public string psuUserAgent {get;set;}

        [Required]
        public PaymentProduct paymentProduct { get; set; }

        public PaymentPriority paymentPriority { get; set; }

        [Required]
        public string redirectUrl { get; set; }
        [Required]
        [StringLength(35)]
        public string endToEndId { get; set; }
        public DateTime? requestedExecutionDate { get; set; }
        [Range(0.01, int.MaxValue, ErrorMessage = "The {0} can not be lower than {1}")]
        public decimal amount { get; set; }
        [Required]
        public string currency { get; set; }
        [Required]
        public string flowId { get; set; }
        public string remittanceInformationUnstructured { get; set; } = "";
        public StructuredRemittance? remittanceInformationStructured { get; set; }
        public Dictionary<String, String> additionalProperties { get; set; } = new Dictionary<string, string>();

    }


    public class RecipientInfo
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string iban { get; set; }
        public TierAddress postalAddress { get; set; }
        public Agent agent { get; set; }

    }

    public class DebtorInfo
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string iban { get; set; }
        public string currency { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        public TierAddress postalAddress { get; set; }
    }

    public class TierAddress
    {
        public TierAddressType addressType { get; set; }
        public String department { get; set; }
        public String subDepartment { get; set; }
        public String streetName { get; set; }
        public String buildingNumber { get; set; }
        public String postCode { get; set; }
        public String townName { get; set; }
        public String countrySubDivision { get; set; }
        public String country { get; set; }
        public List<String> addressLine;
    }

    public class Agent
    {
        public String bicFi { get; set; }

    }

}
