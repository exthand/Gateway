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
        public PropertyConstrains BulksIdConstrains { get; set; } 
        public int MaxNumberOfBulk { get; set; } 
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
        public BulkSupportOptions bulkPayments { get; set; } = new ();
        public PaymentInitiationRequestOptions paymentInitiationRequestOptions { get; set; } = new ();
        public List<AdditionalPropertyRequested> additionalPropertiesRequested { get; set; } = new List<AdditionalPropertyRequested>();
    }

    public class SupportOptions
    {
        public bool supported { get; set; }
        public bool cancelSupported { get; set; }
        public SpecificPaymentDateOption specificPaymentDate { get; set; }
    }

    public class BulkSupportOptions : SupportOptions
    {
        public List<BulkExecutionTypeOptions> ExecutionTypeOptions { get; set; } = new List<BulkExecutionTypeOptions>();
        [Obsolete]
        public int MaxNumberOfBulk { get; set; } 
        public PropertyConstrains BulkIdConstrains { get; set; } 
        public PaymentInitiationRequestOptionsType BatchBooking { get; set; } 
        public bool PrivateAccountsSupported { get; set; }

        public PaymentInitiationRequestOptions PaymentInitiationRequestOptions { get; set; } 
        public List<AdditionalPropertyRequested> AdditionalPropertiesRequested { get; set; }
    }

    public class BulkExecutionTypeOptions
    {
        public PropertyConstrains PaymentIdConstrains { get; set; } 
        public int MaxNumberOfPayments { get; set; } 
        public ExecutionType ExecutionType { get; set; }
    }

    /// <summary>
    /// - **Individual (0)** – Each payment of the bulk payment is debited individually - batchBookingPreferred field = false
    /// - **Batch (1)** – The bulk payment is debited from the payment account for the sum of all payments in one go - batchBookingPreferred field = true
    /// </summary>
    public enum ExecutionType
    {
        Individual,
        Batch
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

