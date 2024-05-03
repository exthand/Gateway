using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{

    public class BulkPaymentInitRequest
    {
        public int connectorId { get; set; }
        public BankSettings bankSettings { get; set; } = null;
        public string userContext { get; set; }
        public string flow { get; set; }
        public BulkPaymentInitiationRequest bulkPaymentInitiationRequest { get; set; } = new();
        public TppContext tppContext { get; set; } = new TppContext();
    }

    public class BulkPaymentInitiationRequest
    {
        [Required]
        public RecipientInfo recipient { get; set; }
        [Required]
        public DebtorInfo debtor { get; set; }
        [RegularExpression(@"((^\s*((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))\s*$)|(^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$))", ErrorMessage = "The {0} has invalid format.")]
        public string psuIp { get; set; }
        public string psuIpPort { get; set; }
        public string psuAccept { get; set; }
        public string psuAcceptCharset { get; set; }
        public string psuAcceptEncoding { get; set; }
        public string psuAcceptLanguage { get; set; }
        public string psuDate { get; set; }
        public string psuDeviceId { get; set; }
        public string psuGeoLocation { get; set; }
        public string psuHttpMethod { get; set; }
        public string psuLastLoggedTime { get; set; }
        public string psuReferer { get; set; }
        public string psuUserAgent { get; set; }

        public List<Bulk> bulks { get; set; } = new();


        [Required]
        public string redirectUrl { get; set; }
        [Required]
        public string flowId { get; set; }
        [Required]
        public string bulksId { get; set; }
    }

    public class Bulk
    {
        [Required]
        public string bulkId { get; set; }

        public bool batchBooking { get; set; }

        public List<Payment> payments { get; set; }

        public DebtorInfo debtor { get; set; }

        public PaymentPriority paymentPriority { get; set; }

        public PaymentProduct paymentProduct { get; set; }

        public ExecutionType executionType { get; set; }

        public DateTime? requestedExecutionDate { get; set; }

        public Dictionary<String, String> additionalProperties { get; set; } = new Dictionary<string, string>();
    }

    public class Payment
    {
        public string paymentId { get; set; }

        public RecipientInfo recipient { get; set; }

        [Range(0.01, int.MaxValue, ErrorMessage = "The {0} can not be lower than {1}")]
        public decimal amount { get; set; }

        [Required]
        public string currency { get; set; }

        [Required]
        [StringLength(35)]
        public string endToEndId { get; set; }

        public string remittanceInformationUnstructured { get; set; } = "";

        /// <summary>
        /// ISO 20022 Regulatory Reporting Code list. Check in options if required with regulatoryReportingCodeRequired field and, in case it is required, the list of available codes are found in options too, field regulatoryReportingCodes. Add here the value field.
        /// </summary>
        public List<RegulatoryReportingCode> RegulatoryReportingCodes { get; set; }
    }

    public class RegulatoryReportingCode
    {
        public string Value { get; set; }
    }

}