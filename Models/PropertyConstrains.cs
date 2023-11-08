using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Schema;

namespace Exthand.GatewayClient.Models
{
    public class PropertyConstrains
    {
        public int minLength { get; set; }
        public int maxLength { get; set; }
        public string acceptedChars { get; set; }
        public string regex { get; set; }
    }
}
