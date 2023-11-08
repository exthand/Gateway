using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BankAccessResponseFinalize
    {

        public ResultStatus resultStatus { get; set; }

        public string dataString { get; set; }

        public string[]  options { get; set; }

        public string userContext { get; set; }

        public string flowContext { get; set; }

        public string rawResponse { get; set; }
    }
}

