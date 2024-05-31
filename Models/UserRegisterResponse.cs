using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class UserRegisterResponse : IBase
    {

        public string psuid { get; set; }
        public string action { get; set; }
        public string url { get; set; }
        public string userContext { get; set; }
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
    }
}
