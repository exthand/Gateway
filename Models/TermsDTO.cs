using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class TermsDTO
    {

        public string tc { get; set; }
        public string privacy { get; set; }
        public int version { get; set; }
        public string language { get; set; }
        public List<string> availableLanguages { get; set; }
    }
}
