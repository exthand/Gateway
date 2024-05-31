using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Exthand.GatewayClient.Models
{
    public class AccountsForPaymentFinalize: IBase
    {
        public AccountsForPayment AccountsForPayment { get; set; }
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }

    public class AccountsForPayment
    {
        [JsonProperty("accounts")] //can be removed when we stop using EB
        public List<PaymentBankAccount> Accounts { get; set; } = new List<PaymentBankAccount>();
    }

    public class PaymentBankAccount : BankAccountBase
    {
        [Obsolete]
        public string accountType { get; set; }
    }


}
