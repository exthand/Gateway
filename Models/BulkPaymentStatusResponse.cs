using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{


    public class BulkPaymentStatusResponse:IBase
    {
        public ResultStatus resultStatus { get; set; }
        public BulkPaymentStatus BulkPaymentStatus { get; set; }
        public string userContext { get; set; }
        public string flowContext { get; set; }
        public string rawResponse { get; set; }

        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }

    public class BulkPaymentStatus
    {
        public PaymentStatusISO20022 Status { get; set; }
        /// <summary>
        /// Raw status code received from the bank
        /// </summary>
        public string StatusCodeRaw { get; set; }
        public string BulksId { get; set; }
        public string BankReferenceId { get; set; }
        public string BankEndToEndId { get; set; }
        public List<BulkPaymentInstructionInformationStatus> Bulks { get; set; } = new List<BulkPaymentInstructionInformationStatus>();
    }


    public class BulkPaymentInstructionInformationStatus
    {
        /// <summary>
        /// Id of the bulk
        /// </summary>
        public string BulkId { get; set; }
        public List<BulkPayment> Payments { get; set; }
        public PaymentStatusISO20022 Status { get; set; }
        /// <summary>
        /// Raw status code received from the bank
        /// </summary>
        public string StatusCodeRaw { get; set; }
    }

    public class BulkPayment
    {
        public string EndToEndIdentification { get; set; }
        public string PaymentId { get; set; }
        public string InternalBankReference { get; set; }
        public string BankReferenceId { get; set; }
        public PaymentStatusISO20022 Status { get; set; }

        /// <summary>
        /// Raw status code received from the bank
        /// </summary>
        public string StatusCodeRaw { get; set; }
    }

}
