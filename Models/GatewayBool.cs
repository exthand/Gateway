using Exthand.GatewayClient.Models;

namespace Exthand.GatewayClient.Models
{
    public class GatewayBool:IBase
    {
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
        public bool value { get; set; }
    }
}