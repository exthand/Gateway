using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class UserRegisterResponse
    {

        public string psuid { get; set; }
        public string action { get; set; }
        public string url { get; set; }
        public string userContext { get; set; }
    }
}
