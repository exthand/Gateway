using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{

    public class BulkPaymentFinalizeResponse
    {
        public ResultStatus resultStatus { get; set; }
        public int statusInfo { get; set; }
        public string dataString { get; set; }
        public Dictionary<string, string> options { get; set; }
        public string flowContext { get; set; }
        public string userContext { get; set; }

        public string paymentId { get; set; }

        public PaymentStatus paymentStatus { get; set; }

        public string rawResponse { get; set; }
    }



}
