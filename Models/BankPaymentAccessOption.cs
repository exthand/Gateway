using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public enum SpecificPaymentDateOption
    {
        NOT_ALLOWED,
        ALLOWED
    }

    public class BankPaymentAccessOption
    {
        public TransferOptions sepaCreditTransfers { get; set; } = new ();
        public TransferOptions instantSepaCreditTransfers { get; set; } = new ();
        public TransferOptions domesticTransfers { get; set; } = new();
        public TransferOptions crossborderSepaCreditTransfers { get; set; } = new ();
        public TransferOptions target2Payment { get; set; } = new ();
    }

    public class TransferOptions
    {
        public SupportOptions singlePayments { get; set; } = new ();
        public SupportOptions periodicPayments { get; set; } = new ();
        public SupportOptions bulkPayments { get; set; } = new ();
        public PaymentInitiationRequestOptions paymentInitiationRequestOptions { get; set; } = new ();
        public List<AdditionalPropertyRequested> additionalPropertiesRequested { get; set; } = new List<AdditionalPropertyRequested>();
    }

    public class SupportOptions
    {
        public bool supported { get; set; }
        public bool cancelSupported { get; set; }
        public SpecificPaymentDateOption specificPaymentDate { get; set; }
    }

    public class AdditionalPropertyRequested
    {
        public String name { get; set; }
        public String title { get; set; }
        public bool required { get; set; }
        public String description { get; set; }
        public String template { get; set; }
    }

}

