using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BankAccessOption : IBase
    {
        public string? XRequestID { get; set; }
        public string? XCorrelationID { get; set; }
        public string? XOperationID { get; set; }
        
        public int transactionsSavingDateField { get; set; }
        public int accessOption { get; set; }
        public int currencyOption { get; set; }
        public int linkingOption { get; set; }

        public List<AccountSchemeType> AccountSchemes { get; set; } = new List<AccountSchemeType>() { AccountSchemeType.IBAN };
        public List<AdditionalPropertyRequested> AdditionalPropertiesRequested { get; set; } = new List<AdditionalPropertyRequested>();
    }
}
