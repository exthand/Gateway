using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BalanceResponse: IBase
    {

        public IEnumerable<Balance> balances { get; set; }
        public string userContext { get; set; }

        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }


    public class Balance
    {

        public string balanceType { get; set; }
        public string rawBalanceType { get; set; }
        public DateTimeOffset? referenceDate { get; set; }
        public DateTimeOffset? lastChangeDateTime { get; set; }
        public BalanceAmount balanceAmount { get; set; }
    }

    public class BalanceAmount
    {

        public string currency { get; set; }
        public decimal amount { get; set; }
    }
}
