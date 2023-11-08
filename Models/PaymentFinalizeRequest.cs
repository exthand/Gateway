using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{


    public class PaymentFinalizeRequest
    {
        public string flow { get; set; }
        public string dataString { get; set; }
        public string userContext { get; set; }
        public BankSettings bankSettings { get; set; }
        public TppContext tppContext { get; set; }
    }

}
