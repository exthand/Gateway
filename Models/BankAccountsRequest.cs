using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BankAccountsRequest
    {
        public int connectorId { get; set; }
        public string userContext { get; set; }
        public BankSettings bankSettings {get;}=null;
        public TppContext tppContext { get; set; }

    }
}
