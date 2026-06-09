using System.ComponentModel.DataAnnotations;

namespace Exthand.GatewayClient.Models
{
    public class VopRequest
    {
        public TppContext tppContext { get; set; } = new TppContext();

        [Required]
        public VopDetails vop { get; set; } = new VopDetails();
    }

    public class VopDetails
    {
        [Required]
        public string iban { get; set; }

        [Required]
        public string name { get; set; }

        public VopIdentifier identifier { get; set; }
    }

    public class VopIdentifier
    {
        public string type { get; set; }
        public string value { get; set; }
    }
}