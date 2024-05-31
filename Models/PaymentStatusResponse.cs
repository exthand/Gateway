using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{


    public class PaymentStatusResponse:IBase
    {
        public ResultStatus resultStatus { get; set; }
        public PaymentStatus paymentStatus { get; set; }
        public string userContext { get; set; }
        public string flowContext { get; set; }
        public string rawResponse { get; set; }

        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }

}
