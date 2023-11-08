
namespace Exthand.GatewayClient.Models
{
    public class TierAddressOptions
    {
        public PaymentInitiationRequestOptionsType addressType { get; set; }
        public PaymentInitiationRequestOptionsType department { get; set; }
        public PaymentInitiationRequestOptionsType subDepartment { get; set; }
        public PaymentInitiationRequestOptionsType streetName { get; set; }
        public PaymentInitiationRequestOptionsType buildingNumber { get; set; }
        public PaymentInitiationRequestOptionsType postCode { get; set; }
        public PaymentInitiationRequestOptionsType townName { get; set; }
        public PaymentInitiationRequestOptionsType countrySubDivision { get; set; }
        public PaymentInitiationRequestOptionsType country { get; set; }
        public PaymentInitiationRequestOptionsType addressLine { get; set; }

        public TierAddressOptions() : this(PaymentInitiationRequestOptionsType.Unused)
        {
        }

        public TierAddressOptions(PaymentInitiationRequestOptionsType option)
        {
            addressType = option;
            department = option;
            subDepartment = option;
            streetName = option;
            buildingNumber = option;
            postCode = option;
            townName = option;
            countrySubDivision = option;
            country = option;
            addressLine = option;
        }
    }
}