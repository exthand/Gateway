using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{

    public class BulkPaymentFinalizeResponse: IBase
    {
        public ResultStatus resultStatus { get; set; }
        public int statusInfo { get; set; }
        public string dataString { get; set; }
        public Dictionary<string, string> options { get; set; }
        public string flowContext { get; set; }
        public string userContext { get; set; }

        public string paymentId { get; set; }

        public BulkPaymentStatus bulkPaymentStatus { get; set; }

        public string rawResponse { get; set; }
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }



}
