using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class AccountsForPaymentRequestInit
    {
        /// <summary>
        /// The id of the connector you want to initiate the payment with.
        /// </summary>
        public int connectorID { get; set; }
        /// <summary>
        /// The user context of the bank user you want to create a payment. 
        /// </summary>
        public string UserContext { get; set; }
        /// <summary>
        /// The request 
        /// </summary>
        public AccountsForPaymentRequest AccountsForPaymentRequest { get; set; }
        /// <summary>
        /// The tpp context. Contains arbitrary data that can be used for later reference in calls reported in our report system.
        /// </summary>
        public TppContext TppContext { get; set; }
    }

    public class AccountsForPaymentRequest 
    {
        [Required]
        public string FlowId { get; set; }
        [Required]
        public string RedirectUrl { get; set; }
        [Required]
        public PaymentPriority PaymentPriority { get; set; }
        [Required]
        public PaymentProduct PaymentProduct { get; set; }
        public string EndToEndId { get; set; }
        public Dictionary<string, string> AdditionalProperties { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Public IP of the PSU
        /// </summary>
        [RegularExpression(@"((^\s*((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))\s*$)|(^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$))", ErrorMessage = "The {0} has invalid format.")]
        public string PsuIp { get; set; }
        /// <summary>
        /// IP Port of the PSU request
        /// </summary>
        public string PsuIpPort { get; set; }
        /// <summary>
        /// Accept header value
        /// </summary>
        public string PsuAccept { get; set; }
        /// <summary>
        /// Accept Charset header value
        /// </summary>
        public string PsuAcceptCharset { get; set; }
        /// <summary>
        /// Accept Encoding header value
        /// </summary>
        public string PsuAcceptEncoding { get; set; }
        /// <summary>
        /// Accept Language header value
        /// </summary>
        public string PsuAcceptLanguage { get; set; }
        /// <summary>
        /// Date at which PSU has made the request
        /// </summary>
        public string PsuDate { get; set; }
        /// <summary>
        /// Device Id header value
        /// </summary>
        public string PsuDeviceId { get; set; }
        /// <summary>
        /// Geo Location header value
        /// </summary>
        public string PsuGeoLocation { get; set; }
        /// <summary>
        /// HTTP method
        /// </summary>
        public string PsuHttpMethod { get; set; }
        public string PsuLastLoggedTime { get; set; }
        public string PsuReferer { get; set; }
        /// <summary>
        /// User Agent header value
        /// </summary>
        public string PsuUserAgent { get; set; }




    }

}
