using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class UserDTO
    {

        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string cellPhone { get; set; }
        public string vatid{ get; set; }
        public string externalRef { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public int validatedVersionTC { get; set; }

    }
}
