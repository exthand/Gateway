using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public interface IBase
    {
        public string? XRequestID { get; set; }
        public string? XCorrelationID { get; set; }
        public string? XOperationID { get; set; }
    }
}
