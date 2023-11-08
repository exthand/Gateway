using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
 
    /// <summary>
    /// Contains the parameters to request consent of a bank account.
    /// </summary>
    public class BankAccessRequest
    {

        public int connectorId { get; set; }
        public string userContext { get; set; }
        public BankSettings bankSettings { get; } = null;
        public TppContext tppContext { get; set; }
        public AccountsAccessRequest accountsAccessRequest { get; set; }

    }

}
