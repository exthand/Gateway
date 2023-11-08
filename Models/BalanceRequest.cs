using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BalanceRequest: BankAccessRequest
    {
        public string psuIp { get; set; }

    }

}
