using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Schema;

namespace Exthand.GatewayClient.Models
{
    public class Error
    {
        
        public string exceptionType { get; set; }

        public string message { get; set; }


        public string innerException { get; set; }


        public int? statusCode { get; set; }


        public string userContext { get; set; }

        public string log { get; set; }

        public string xCorrelationId { get; set; }

        public string xOperationId { get; set; }
    }
}
