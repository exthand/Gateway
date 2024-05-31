using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{

    public enum ConsentStatus : int
    {
        OK = 0,
        INVALID_GRANT = 100
    }

    public class BankAccountsResponse:IBase
    {
        public string userContext { get; set; }
        public BankAccountResponse[] accounts {get;set;}

        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }

    /// <summary>
    /// Account identification, when not IBAN and if other scheme than IBAN are supported (check [recipient | debtor].accountSchemes option), you can use this structure to provide the account identification. E.g. BBAN
    /// </summary>
    public class GenericAccountIdentification
    {
        /// <summary>
        /// The identification, e.g. BBAN
        /// </summary>
        public string identification { get; set; }
        public AccountSchemeType schemeName { get; set; }
        public string issuer { get; set; }
    }

    public class BankAccountBase
    {
        public string id { get; set; }
        public string currency { get; set; }
        public string iban { get; set; }
        public string description { get; set; }
        /// <summary>
        /// Name of the owner of the account.
        /// </summary>
        public string ownerName { get; set; }
        /// <summary>
        /// Different account identifiers used to specify something other than IBAN. E.g. BBAN.
        /// </summary>
        public List<GenericAccountIdentification> genericAccountIdentifications { get; set; }
        /// <summary>
        /// BIC associated to the account.
        /// </summary>
        public string bic { get; set; }
        /// <summary>
        /// ExternalCashAccountType1Code from ISO 20022.
        /// </summary>
        public string cashAccountType { get; set; }
        /// <summary>
        /// Cash Account Type, received directly from the bank.
        /// </summary>
        public string cashAccountTypeRaw { get; set; }
        /// <summary>
        /// Product Name of the Bank for this account.
        /// </summary>
        public string product { get; set; }
    }

    public class BankAccountResponse : BankAccountBase
    {
        public TransactionsConsent transactionsConsent { get; set; }
        public BalancesConsent balancesConsent { get; set; }
    }


    public class TransactionsConsent
    {
        public string consentId { get; set; }
        public DateTime? validUntil { get; set; }

        public ConsentStatus status { get; set; }
        public DateTime? statusAt { get; set; }
    }

    public class BalancesConsent
    {
        public string consentId { get; set; }
        public DateTime? validUntil { get; set; }

        public ConsentStatus status { get; set; }
        public DateTime? statusAt { get; set; }
    }
}
