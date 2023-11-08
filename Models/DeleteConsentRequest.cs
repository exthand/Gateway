using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class DeleteConsentRequest
    {
        public string id { get; set; }
        public int connectorId { get; set; }
        public string userContext { get; set; }
        public TppContext tppContext { get; set; }

    }

}
