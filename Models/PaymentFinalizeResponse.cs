﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{

    public class PaymentFinalizeResponse: IBase
    {
        public ResultStatus resultStatus { get; set; }
        public string dataString { get; set; }
        public Dictionary<string, string> options { get; set; }
        public string flowContext { get; set; }
        public string userContext { get; set; }
        public string paymentId {get;set;}
        public PaymentStatus paymentStatus { get; set; }
        public int statusInfo { get; set; }
        public string rawResponse { get; set; }
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }


    public class PaymentStatus
    {
        public string paymentId { get; set; }
        public string debtorName { get; set; }
        public BankingAccount debtor { get; set; }
        public string creditorName { get; set; }
        public BankingAccount creditor { get; set; }
        public BankingAccountInstructedAmount amount { get; set; }
        public PaymentStatusISO20022 status { get; set; }
        public string statusCodeRaw { get; set; }
        public string endToEndIdentification { get; set; }
        public TransactionRemittanceInformation RemittanceInformation { get; set; }
    }

    public class BankingAccount
    {
        public string iban { get; set; }
        public string currency { get; set; }
    }

    public class BankingAccountInstructedAmount
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
}
