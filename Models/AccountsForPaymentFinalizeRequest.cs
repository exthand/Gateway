using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class AccountsForPaymentFinalizeRequest
    {
        /// <summary>
        /// Flow context received in the previous call of the flow
        /// </summary>
        public string Flow { get; set; }
        /// <summary>
        /// Data needed to continue teh flow. Depends on the flow type (e.g. for Redirect flow the DataString should be a query string form bank's callback url)
        /// </summary>
        public string DataString { get; set; }
        public string UserContext { get; set; }
        public BankSettings BankSettings { get; set; }
        public TppContext TppContext { get; set; }
    }
}
