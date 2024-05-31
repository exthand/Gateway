using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class TransactionResponse: IBase
    {
        public IEnumerable<Transaction> transactions { get; set; }
        public string userContext { get; set; }
        public string pagerContext { get; set; }
        public bool isFirstPage { get; set; }
        public bool isLastPage { get; set; }
        public string rawResponse { get; set; }


        /// <summary>
        /// True indicates that the consent to retrieve banking transactions or balances has been lost or expired. Renew the consent through an SCA process with your end user.
        /// </summary>
        public bool isConsentLost { get; set; } = false;

        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }


    public class Transaction
    {

        public string id { get; set; }
        public decimal amount { get; set; }
        public string counterpartReference { get; set; }
        public string counterpartName { get; set; }
        public string counterpartBic { get; set; }
        public string currency { get; set; }
        public string description { get; set; }

        public DateTimeOffset? executionDate { get; set; }
        public DateTimeOffset? valueDate { get; set; }
        public bool possibleHole { get; set; }

        public TransactionRemittanceInformation remittanceInformation { get; set; }
        public TransactionBankTransactionCode bankTransactionCode { get; set; }
        
        public string gwAccountId { get; set; }
        public string gwSequence { get; set; }
        public long gwSequenceNumber { get
            {
                if (string.IsNullOrEmpty(gwSequence))
                    return 0;
                else
                {
                    return long.Parse(gwSequence.Remove(8, 1));
                }
            }
        }

        /// <summary>
        /// If true, means the transaction is a SUM of a batch of transactions, provided by the bank.
        /// </summary>
        public bool? globalization { get; set; }

    }

    public class TransactionRemittanceInformation
    {
        public TransactionRemittanceInformationStructured structured { get; set; }
        public string unstructured { get; set; }
    }
    public class TransactionRemittanceInformationStructured
    {
        public TransactionCreditorReferenceInformation creditorReferenceInformation { get; set; }
    }
    public class TransactionCreditorReferenceInformation
    {
        public TransactionCreditorReferenceInformationType type { get; set; }
        public string reference { get; set; }
    }

    public class TransactionCreditorReferenceInformationType
    {
        public string code { get; set; }
        public string issuer { get; set; }
    }



    public class TransactionBankTransactionCode
    {
        public string domain { get; set; }
        public string family { get; set; }
        public string subFamily { get; set; }
    }

}
