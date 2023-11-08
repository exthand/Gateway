using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BalanceResponse
    {

        public IEnumerable<Balance> balances { get; set; }
        public string userContext { get; set; }

    }


    public class Balance
    {

        public string balanceType { get; set; }
        public string rawBalanceType { get; set; }
        public DateTime? referenceDate { get; set; }
        public DateTime? lastChangeDateTime { get; set; }
        public BalanceAmount balanceAmount { get; set; }
    }

    public class BalanceAmount
    {

        public string currency { get; set; }
        public decimal amount { get; set; }
    }
}
