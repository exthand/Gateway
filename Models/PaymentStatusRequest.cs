using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exthand.GatewayClient.Models
{


    public class PaymentStatusRequest
    {
        [Required]
        public int connectorId { get; set; }

        public string paymentId { get; set; }

        [Required]
        public string flow { get; set; }

        public string userContext { get; set; }
        public BankSettings bankSettings { get; set; }
        public TppContext tppContext { get; set; }
    }

}
