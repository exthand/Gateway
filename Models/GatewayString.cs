using Exthand.GatewayClient.Models;

namespace Exthand.GatewayClient.Models
{
    public class GatewayString:IBase
    {
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
        public string Content { get; set; }
    }
}