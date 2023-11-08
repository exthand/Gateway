using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class TppContext
    {
        public string tppId { get; set; }
        public string app { get; set; }
        public string flow { get; set; }
        public string transaction { get; set; }
        public string unit { get; set; }
    }
}
