namespace Exthand.GatewayClient.Models
{
    public class AgentOptions
    {
        public PaymentInitiationRequestOptionsType country = PaymentInitiationRequestOptionsType.Unused;
        public PaymentInitiationRequestOptionsType bicFi = PaymentInitiationRequestOptionsType.Optional;
    }
}