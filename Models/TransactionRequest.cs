using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class TransactionRequest
    {
        public int connectorId { get; set; }
        public string userContext { get; set; }
        public BankSettings bankSettings { get; } = null;
        public TppContext tppContext { get; set; }
        public string psuIp { get; set; }

    }

    public class TransactionPagingRequest
    {
        public int connectorId { get; set; }
        public string userContext { get; set; }
        public BankSettings bankSettings { get; } = new();
        public TppContext tppContext { get; set; }
        public string psuIp { get; set; }
        public string pagerContext { get; set; }

    }

}
