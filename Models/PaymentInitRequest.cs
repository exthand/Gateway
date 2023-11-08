using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{

    public class PaymentInitRequest
    {
        public int connectorId { get; set; }
        public BankSettings bankSettings { get; set; } = null;
        public string userContext { get; set; }
        public PaymentInitiationRequest paymentInitiationRequest { get; set; } = new PaymentInitiationRequest();
        public TppContext tppContext { get; set; } = new TppContext();
    }
}