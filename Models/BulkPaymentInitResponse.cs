using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{

    public class BulkPaymentInitResponse
    {
        public ResultStatus resultStatus { get; set; }

        public int statusInfo { get; set; }

        public string dataString { get; set; }
        public Dictionary<string, string> options { get; set; }
        public string flowContext { get; set; }
        public string userContext { get; set; }
        public string rawResponse { get; set; }
    }
}

