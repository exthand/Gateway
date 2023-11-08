using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BankAccessRequestFinalize
    {
        public string flow { get; set; }
        public string dataString { get; set; }
        public string userContext { get; set; }
        public BankSettings bankSettings { get;  } = null;
        public TppContext tppContext { get; set; }

    }

}
